﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using SnagFree.TrayApp.Core;
using System.Threading;

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

            // 내부 단어 로드
            /*try
            {
                string[] wordResources = new string[] { "attack", "defense", "hanbang", "long" };
                string basePath = Directory.GetCurrentDirectory();
                List<string> pathSplit = basePath.Split('\\').ToList();
                pathSplit.RemoveAt(pathSplit.Count - 1);
                pathSplit.RemoveAt(pathSplit.Count - 1);
                basePath = String.Join("/", pathSplit);

                foreach (string path in wordResources)
                {
                    StreamReader reader = new StreamReader($"{basePath}/Resources/{path}.txt");
                    words = words.Concat(loadWords(reader)).ToList();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"단어를 로1드하지 못했습니다.\n{e.ToString()}");
            }*/

            // 외부 단어 로드
            try
            {
                string basePath = Directory.GetCurrentDirectory();
                string[] defaultWordResources = new string[] { "attack", "defense", "hanbang", "long", "mission" };

                foreach (string origin in defaultWordResources)
                {
                    string path = $"{basePath}/Resources/default/{origin}.txt";
                    readWordsFromTxt(path, origin);
                }

                DirectoryInfo extensions = new DirectoryInfo($"{basePath}/Resources/extensions");
                foreach (FileInfo file in extensions.GetFiles("*.txt"))
                {
                    string path = $"{basePath}/Resources/extensions/{file.Name}";
                    readWordsFromTxt(path);
                }
            } catch (Exception e)
            {
                MessageBox.Show($"단어를 로드하지 못했습니다.\n{e.ToString()}");
            }

            //this.search.TextChanged += updateSearch;
            this.submit.MouseClick += sendSearch;
            this.search.KeyPress += (sender, e) => { if (e.KeyChar == (char)13) { updateSearch(this.search.Text); } };

            this.injungWord.MouseClick += sendSearch;
            this.deathWord.MouseClick += sendSearch;
            this.sortFrom.MouseClick += sendSearch;
            this.sortLength.MouseClick += sendSearch;

            keyboardHook = new GlobalKeyboardHook();
            keyboardHook.KeyboardPressed += autoInput;
        }

        // Main
        private List<Word> words = new List<Word>(); // 단어 목록
        private List<Word> wordsLoaded = new List<Word>(); // 현재 검색된 단어 목록
        private string selected = null;

        private List<string> history = new List<string>(); // 비공개적으로 이전에 봤던 단어를 기록, 이전으로 가기에 사용
        private List<string> lateUses = new List<string>(); // 공개적으로 이전에 봤던 단어를 기록, 검색 기록처럼 쓰임
        private List<Button> wordButtons = new List<Button>();
        private GlobalKeyboardHook keyboardHook;

        // 단어 관련 함수
        private List<Word> loadWords(StreamReader reader, string[] optionalTags = null)
        {
            List<Word> result = new List<Word>();
            using (reader)
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        // 전체 태그
                        if (line[0] == '[' && line[line.Length - 1] == ']')
                        {
                            string tag = line.Substring(1, line.Length - 2);
                            List<string> tags;
                            if (optionalTags != null)
                            {
                                tags = optionalTags.ToList();
                            } else
                            {
                                tags = new List<string>();
                            }
                            tags.Add(tag);
                            optionalTags = tags.ToArray();
                        } else // 단어 로드
                        {
                            Word word = new Word();
                            string[] split = line.Split(' ');

                            bool collapsed = false;
                            foreach (Word check in words)
                            {
                                if (check.word == split[0])
                                {
                                    collapsed = true;
                                    break;
                                }
                            }

                            if (collapsed == false)
                            {
                                word.word = split[0];

                                List<string> tags = split.ToList();
                                tags.RemoveAt(0);
                                if (optionalTags != null)
                                {
                                    tags = tags.Concat(optionalTags).ToList();
                                }

                                List<string> filteredTags = new List<string>();
                                foreach (string tag in tags)
                                {
                                    if (tag.Length > 0)
                                    {
                                        filteredTags.Add(tag);
                                    }
                                }
                                word.tags = filteredTags;

                                result.Add(word);
                            }
                        }
                    } finally { }
                }
            }
            return result;
        }

        private void readWordsFromTxt(string path, string origin = null)
        {
            StreamReader reader = new StreamReader(path);
            string[] additionalTags = null;
            if (origin == "attack")
            {
                additionalTags = new string[] { "공격" };
            }
            else if (origin == "defense")
            {
                additionalTags = new string[] { "방어" };
            }
            words = words.Concat(loadWords(reader, additionalTags)).ToList();
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
                if (!(hanbang == false && word.hasTag("한방") || injung == false && word.hasTag("어인정")))
                {
                    // 필터 검사 + 우선도 책정
                    if (word.checkFilter(search, sortFrom, sortLength))
                    {
                        targets.Add(word);
                    }
                }
            }
            return targets;
        }

        // 디스플레이 관련 함수
        private Font fitFontSize(string fontName, float fontSize, string text, int labelWidth)
        {
            Font font = new Font(fontName, fontSize);
            Size textSize = TextRenderer.MeasureText(text, font);
            if (textSize.Width > labelWidth)
            {
                float scale = (float)labelWidth / textSize.Width;
                fontSize = fontSize * scale;
                if (fontSize == 0)
                {
                    fontSize = 1;
                }
                font = new Font(fontName, fontSize);
            }
            return font;
        }

        private Button createButtonFromWord(Word word)
        {
            string tags = "";
            foreach (string tag in word.tags)
            {
                tags += $"[{tag}] ";
            }

            //string text = word.word + " " + String.Join(" ", word.tags.ToArray());
            string text = word.word + " " + tags;
            string lastWord = word.word[word.word.Length - 1] + "~";

            int buttonWidth = 570;
            int textWidth = buttonWidth - 50;
            Font font = fitFontSize("한컴 고딕", 12, text, textWidth);
            Size textSize = TextRenderer.MeasureText(text, font);

            // 버튼 생성
            Button button = new Button()
            {
                Text = text,
                Size = new Size(buttonWidth, 30),
                Font = font,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 0, 0, 0)
            };

            button.MouseClick += (sender, e) => {
                selected = word.word;
                this.current.Text = word.word;
                this.current.Font = fitFontSize("한컴 고딕", 18, word.word, 530);
                updateSearch(lastWord);
            };
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

        // 검색 관련 함수
        private void updateSearch(string search)
        {
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

        private void sendSearch(object sender, MouseEventArgs e)
        {
            string search = this.search.Text;
            updateSearch(search);
        }

        // 페이지 관련 함수
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

        // 자동 입력기
        private bool[] inputPressed = new bool[] { false, false, false }; // 세번째는 디바운드용
        private void autoInput(object sender, GlobalKeyboardHookEventArgs e)
        {
            int inputIndex = -1;
            bool status = false;
            Keys key = (Keys)e.KeyboardData.VirtualCode;

            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown) { status = true; }
            if (key == Keys.LControlKey) { inputIndex = 0; }
            else if (key == Keys.B) { inputIndex = 1; }

            if (inputIndex != -1)
            {
                inputPressed[inputIndex] = status;
                if (status == false)
                {
                    inputPressed[2] = false;
                }
            }

            if (inputPressed[0] == true && inputPressed[1] == true && inputPressed[2] == false)
            {
                //Console.WriteLine("auto input");
                inputPressed[2] = true;

                // 타자 침
                string currentWriting = selected;
                new Thread(() =>
                {
                    foreach (char target in currentWriting) // 한 글자로 쪼갬
                    {
                        SendKeys.SendWait(target.ToString());
                        Thread.Sleep(100);
                        /*char[] split = Hangul.split(target);
                        foreach (char segment in split) // 한 자모로 쪼갬
                        {
                            if (segment != ' ')
                            {
                                foreach (char superSegment in Hangul.superSplit(segment)) // 키보드 최소 단위로 쪼갬
                                {
                                    SendKeys.SendWait($"{{{superSegment.ToString()} 1}}");
                                    Thread.Sleep(25);
                                }
                            }
                        }*/
                    }
                    /*SendKeys.Send(" ");
                    SendKeys.Send("{BACKSPACE}");*/
                }).Start();
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
