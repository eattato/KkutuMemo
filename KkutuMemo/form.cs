using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
//using Newtonsoft.Json;

namespace KkutuMemo
{
    public partial class form : Form
    {
        private bool dragging = false;
        private Point dragOffset;
        private int currentPage = 0;
        private int wordsPerPage = 7;

        // Init
        public form()
        {
            InitializeComponent();
            this.ActiveControl = null; // 텍스트 박스 선택 해제

            // 투명화 키로 창을 투명하게 보이도록
            this.BackColor = Color.Fuchsia;
            this.TransparencyKey = this.BackColor;

            // 이벤트 바인드
            this.bg.MouseDown += bg_MouseDown;
            this.bg.MouseMove += bg_MouseMove;
            this.bg.MouseUp += bg_MouseUp;

            // 버튼 바인드
            this.close.MouseClick += ExitApp;
            this.minimum.MouseClick += MinimizeApp;
            this.pin.MouseClick += PinOnTop;
            this.prev.MouseClick += prev_button;
            this.next.MouseClick += next_button;

            // 메인 로직
            words = loadWords("../../Resources/words.txt");
            words = words.Concat(loadWords("../../Resources/long.txt", new string[] { "[긴단어]" })).ToList();
            //this.search.TextChanged += updateSearch;
            this.submit.MouseClick += updateSearch;
            this.search.KeyPress += (sender, e) => { if (e.KeyChar == (char)13) { updateSearch(null, null); } };

            this.injungWord.MouseClick += updateSearch;
            this.deathWord.MouseClick += updateSearch;
            this.sortFrom.MouseClick += updateSearch;
            this.sortLength.MouseClick += updateSearch;
        }

        // Main
        private List<Word> words = new List<Word>(); // 단어 목록
        private List<Word> wordsLoaded = new List<Word>(); // 현재 검색된 단어 목록
        private List<string> history = new List<string>(); // 비공개적으로 이전에 봤던 단어를 기록, 이전으로 가기에 사용
        private List<string> lateUses = new List<string>(); // 공개적으로 이전에 봤던 단어를 기록, 검색 기록처럼 쓰임
        private List<Button> wordButtons = new List<Button>();
         
        private List<Word> loadWords(string path, string[] optionalTags = null)
        {
            List<Word> result = new List<Word>();
            using (StreamReader f = new StreamReader(path))
            {
                //string data = f.ReadToEnd();
                //return JsonConvert.DeserializeObject<List<Word>>(data);

                string line;
                while ((line = f.ReadLine()) != null)
                {
                    Word word = new Word();
                    string[] split = line.Split(' ');
                    word.word = split[0];

                    List<string> tags = split.ToList();
                    tags.RemoveAt(0);
                    if (optionalTags != null)
                    {
                        tags = tags.Concat(optionalTags).ToList();
                    }
                    word.tags = tags;

                    result.Add(word);
                }
            }
            return result;
        }

        private List<Word> sortWords(List<Word> targets)
        {
            List<Word> result = new List<Word>();
            int loopCount = targets.Count;
            while (targets.Count > 0)
            {
                int target = -1;
                for (int ind = 0; ind < targets.Count; ind++)
                {
                    Word word = targets[ind];
                    if (target == -1 || targets[target].priority > word.priority)
                    {
                        target = ind;
                    }
                }
                result.Add(targets[target]);
                targets.RemoveAt(target);
            }
            return result;
        }

        private Button createButtonFromWord(Word word)
        {
            string text = word.word + " " + String.Join(" ", word.tags.ToArray());
            Button button = new Button()
            {
                Text = text,
                Size = new Size(570, 30),
                Font = new Font("한컴 고딕", 12),
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 0, 0, 0)
            };
            button.MouseClick += (sender, e) => { this.search.Text = word.word; this.current.Text = text; };
            button.Parent = this.targets;
            wordButtons.Add(button);

            return button;
        }

        private void displayButtonOfPage()
        {
            foreach (Button button in wordButtons)
            {
                button.Dispose();
            }
            wordButtons = new List<Button>();

            int pageMax = (currentPage + 1) * wordsPerPage;
            if (wordsLoaded.Count < pageMax)
            {
                pageMax = wordsLoaded.Count;
            }
            Debug.WriteLine($"start {currentPage * wordsPerPage}, end {pageMax}");
            for (int ind = currentPage * wordsPerPage; ind < pageMax; ind++)
            {
                Word word = wordsLoaded[ind];
                createButtonFromWord(word);
            }
        }

        private void updateSearch(object sender, EventArgs e)
        {
            string search = this.search.Text;
            if (search.Length >= 1)
            {
                currentPage = 0;
                this.page.Text = (currentPage + 1).ToString();

                Stopwatch stopwatch = Stopwatch.StartNew();
                long start = DateTime.Now.Ticks;
                wordsLoaded = getWords(search);

                stopwatch.Stop();
                Debug.WriteLine($"spend {stopwatch.ElapsedMilliseconds} ms on getting words..");

                // 순서에 맞게 정렬
                stopwatch = Stopwatch.StartNew();
                wordsLoaded = sortWords(wordsLoaded);

                stopwatch.Stop();
                Debug.WriteLine($"spend {stopwatch.ElapsedMilliseconds} ms on sorting words..");

                stopwatch = Stopwatch.StartNew();
                displayButtonOfPage();

                stopwatch.Stop();
                Debug.WriteLine($"spend {stopwatch.ElapsedMilliseconds} ms on placing buttons..");
            }
        }

        private List<Word> getWords(string search)
        {
            string[] split = search.Split(' ');
            bool hanbang = this.deathWord.Active;
            bool injung = this.injungWord.Active;
            bool sortFrom = this.sortFrom.Active; // t = 앞 시작 부터, f = 뒷 시작 부터
            bool sortLength = this.sortLength.Active; // t = 긴 거 부터, f = 짧은 거 부터

            List<Word> targets = new List<Word>();

            // 해당 조건에 맞는 단어들 가져옴
            foreach (Word word in words)
            {
                // 어인정이나 한 방 꺼지면 해당된 게 안 나와야함
                if (!(hanbang == false && word.hasTag("[한방]") || injung == false && word.hasTag("[어인정]")))
                {
                    // 필터 검사 + 우선도 책정
                    if (word.checkFilter(search))
                    {
                        targets.Add(word);
                    }
                }
            }
            return targets;
        }

        private void prev_button(object sender, MouseEventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage -= 1;
                this.page.Text = (currentPage + 1).ToString();
                displayButtonOfPage();
            }
        }

        private void next_button(object sender, MouseEventArgs e)
        {
            int pageCount = (int)Math.Ceiling((float)wordsLoaded.Count / wordsPerPage);
            if (currentPage < pageCount)
            {
                currentPage += 1;
                this.page.Text = (currentPage + 1).ToString();
                displayButtonOfPage();
            }
        }

        // Drag
        private void bg_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            //dragOffset = new Point(this.Location.X - e.X, this.Location.Y - e.Y);
            dragOffset = e.Location;
            this.ActiveControl = null;
        }

        private void bg_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                //this.Location = new Point(e.X - dragOffset.X, e.Y - dragOffset.Y);
                this.Location = new Point(
                    this.Location.X - dragOffset.X + e.X,
                    this.Location.Y - dragOffset.Y + e.Y);
                this.Update();
            }
        }

        private void bg_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        // Buttons
        private void ExitApp(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void MinimizeApp(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PinOnTop(object sender, MouseEventArgs e)
        {
            if (ActiveForm.TopMost == true)
            {
                ActiveForm.TopMost = false;
            } else
            {
                ActiveForm.TopMost = true;
            }
        }

        private void targets_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
