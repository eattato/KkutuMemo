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

namespace KkutuMemo
{
    public partial class form : Form
    {
        private bool dragging = false;
        private Point dragOffset;

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
            this.close.MouseDown += ExitApp;
            this.minimum.MouseDown += MinimizeApp;
            this.pin.MouseDown += PinOnTop;


            // 메인 로직
            this.search.TextChanged += updateSearch;
        }

        // Main
        private List<Word> words = new List<Word>(); // 단어 목록
        private List<string> history = new List<string>(); // 비공개적으로 이전에 봤던 단어를 기록, 이전으로 가기에 사용
        private List<string> lateUses = new List<string>(); // 공개적으로 이전에 봤던 단어를 기록, 검색 기록처럼 쓰임

        private List<Word> sortWords(List<Word> targets)
        {
            List<Word> result = new List<Word>();
            int loopCount = targets.Count;
            while (targets.Count > 0)
            {
                int highest = -1;
                for (int ind = 0; ind < targets.Count; ind++)
                {
                    Word word = targets[ind];
                    if (highest == -1 || targets[highest].priority < word.priority)
                    {
                        highest = ind;
                    }
                }
                result.Add(targets[highest]);
                targets.RemoveAt(highest);
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
                Font = new Font("한컴 고딕", 24),
                TextAlign = ContentAlignment.MiddleLeft
            };
            button.MouseClick += (sender, e) => { this.search.Text = word.word; this.current.Text = text; };
            button.Parent = this.targets;

            return button;
        }

        private void updateSearch(object sender, EventArgs e)
        {
            string search = this.search.Text;

            string[] split = search.Split(' ');
            bool hanbang = this.deathWord.Active;
            bool injung = this.injungWord.Active;
            bool sortFrom = this.sortFrom.Active; // t = 앞 시작 부터, f = 뒷 시작 부터
            bool sortLength = this.sortLength.Active; // t = 긴 거 부터, f = 짧은 거 부터

            List<Word> targets = new List<Word>();

            // 해당 조건에 맞는 단어들 가져옴
            foreach (Word word in words)
            {
                if (word.hasTag("[한방]") == hanbang && word.hasTag("[어인정]") == injung)
                {
                    // 필터 검사
                    bool filterDone = true;
                    for (int ind = 1; ind < split.Length; ind++)
                    {
                        string filter = split[ind];
                        if (word.checkFilter(filter) != 1)
                        {
                            filterDone = false;
                            break;
                        }
                    }

                    if (filterDone)
                    {
                        bool start = word.word.StartsWith(split[0]);
                        bool end = word.word.EndsWith(split[0]);
                        bool contains = word.word.Contains(split[0]);

                        if (start || end || contains)
                        {
                            // 시작 단어냐 끝 단어냐에 순서 부여
                            if (start)
                            {
                                if (sortFrom)
                                {
                                    word.priority = 100;
                                }
                                else
                                {
                                    word.priority = 200;
                                }
                            }
                            else if (end)
                            {
                                if (sortFrom)
                                {
                                    word.priority = 200;
                                }
                                else
                                {
                                    word.priority = 100;
                                }
                            }
                            else if (contains)
                            {
                                word.priority = 300;
                            }

                            // 길이에 따라 순서 변화
                            if (sortLength)
                            {
                                word.priority += word.word.Length;
                            } else
                            {
                                word.priority -= word.word.Length;
                            }
                            targets.Add(word);
                        }
                    }
                }
            }

            // 순서에 맞게 정렬
            targets = sortWords(targets);
            foreach (Word word in targets)
            {
                createButtonFromWord(word);
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
    }
}
