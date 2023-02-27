namespace KkutuMemo
{
    partial class form
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form));
            this.bg = new System.Windows.Forms.Panel();
            this.page = new System.Windows.Forms.Label();
            this.targets = new System.Windows.Forms.FlowLayoutPanel();
            this.current = new System.Windows.Forms.Label();
            this.glassPanel1 = new KkutuMemo.GlassPanel();
            this.pin = new KkutuMemo.MoremiButton();
            this.titleFrame = new KkutuMemo.MoremiPanel();
            this.title = new System.Windows.Forms.Label();
            this.minimum = new KkutuMemo.MoremiButton();
            this.close = new KkutuMemo.MoremiButton();
            this.next = new KkutuMemo.MoremiButton();
            this.prev = new KkutuMemo.MoremiButton();
            this.sortLength = new KkutuMemo.MoremiRadioButton();
            this.sortFrom = new KkutuMemo.MoremiRadioButton();
            this.deathWord = new KkutuMemo.MoremiRadioButton();
            this.injungWord = new KkutuMemo.MoremiRadioButton();
            this.submit = new KkutuMemo.MoremiButton();
            this.search = new KkutuMemo.BetterTextbox();
            this.bg.SuspendLayout();
            this.glassPanel1.SuspendLayout();
            this.titleFrame.SuspendLayout();
            this.SuspendLayout();
            // 
            // bg
            // 
            this.bg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bg.Controls.Add(this.page);
            this.bg.Controls.Add(this.next);
            this.bg.Controls.Add(this.prev);
            this.bg.Controls.Add(this.targets);
            this.bg.Controls.Add(this.sortLength);
            this.bg.Controls.Add(this.sortFrom);
            this.bg.Controls.Add(this.current);
            this.bg.Controls.Add(this.deathWord);
            this.bg.Controls.Add(this.injungWord);
            this.bg.Controls.Add(this.submit);
            this.bg.Controls.Add(this.search);
            this.bg.Location = new System.Drawing.Point(0, 35);
            this.bg.Name = "bg";
            this.bg.Size = new System.Drawing.Size(600, 365);
            this.bg.TabIndex = 1;
            // 
            // page
            // 
            this.page.AutoSize = true;
            this.page.Font = new System.Drawing.Font("한컴 고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.page.Location = new System.Drawing.Point(531, 16);
            this.page.Name = "page";
            this.page.Size = new System.Drawing.Size(14, 16);
            this.page.TabIndex = 13;
            this.page.Text = "0";
            // 
            // targets
            // 
            this.targets.Location = new System.Drawing.Point(15, 120);
            this.targets.Margin = new System.Windows.Forms.Padding(0);
            this.targets.Name = "targets";
            this.targets.Size = new System.Drawing.Size(570, 230);
            this.targets.TabIndex = 10;
            this.targets.Paint += new System.Windows.Forms.PaintEventHandler(this.targets_Paint);
            // 
            // current
            // 
            this.current.AutoSize = true;
            this.current.Font = new System.Drawing.Font("한컴 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.current.Location = new System.Drawing.Point(15, 75);
            this.current.Name = "current";
            this.current.Size = new System.Drawing.Size(0, 31);
            this.current.TabIndex = 6;
            // 
            // glassPanel1
            // 
            this.glassPanel1.Controls.Add(this.pin);
            this.glassPanel1.Controls.Add(this.titleFrame);
            this.glassPanel1.Controls.Add(this.minimum);
            this.glassPanel1.Controls.Add(this.close);
            this.glassPanel1.Location = new System.Drawing.Point(0, 0);
            this.glassPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.glassPanel1.Name = "glassPanel1";
            this.glassPanel1.Size = new System.Drawing.Size(600, 35);
            this.glassPanel1.TabIndex = 2;
            // 
            // pin
            // 
            this.pin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pin.CornerRadius = 10;
            this.pin.CornerUnder = false;
            this.pin.FlatAppearance.BorderSize = 0;
            this.pin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pin.Font = new System.Drawing.Font("한컴 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.pin.ForeColor = System.Drawing.Color.Black;
            this.pin.Location = new System.Drawing.Point(90, 5);
            this.pin.Margin = new System.Windows.Forms.Padding(0);
            this.pin.Name = "pin";
            this.pin.Size = new System.Drawing.Size(55, 30);
            this.pin.TabIndex = 7;
            this.pin.Text = "고정";
            this.pin.UseVisualStyleBackColor = false;
            // 
            // titleFrame
            // 
            this.titleFrame.BackColor = System.Drawing.Color.LimeGreen;
            this.titleFrame.Controls.Add(this.title);
            this.titleFrame.Location = new System.Drawing.Point(0, 5);
            this.titleFrame.Margin = new System.Windows.Forms.Padding(0);
            this.titleFrame.Name = "titleFrame";
            this.titleFrame.Size = new System.Drawing.Size(90, 30);
            this.titleFrame.TabIndex = 6;
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("한컴 고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.title.Location = new System.Drawing.Point(11, 8);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(65, 16);
            this.title.TabIndex = 0;
            this.title.Text = "끄투 단어장";
            // 
            // minimum
            // 
            this.minimum.BackColor = System.Drawing.Color.Aqua;
            this.minimum.CornerRadius = 10;
            this.minimum.CornerUnder = false;
            this.minimum.FlatAppearance.BorderSize = 0;
            this.minimum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimum.Font = new System.Drawing.Font("한컴 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.minimum.ForeColor = System.Drawing.Color.Black;
            this.minimum.Location = new System.Drawing.Point(490, 5);
            this.minimum.Margin = new System.Windows.Forms.Padding(0);
            this.minimum.Name = "minimum";
            this.minimum.Size = new System.Drawing.Size(55, 30);
            this.minimum.TabIndex = 5;
            this.minimum.Text = "최소화";
            this.minimum.UseVisualStyleBackColor = false;
            // 
            // close
            // 
            this.close.BackColor = System.Drawing.Color.Red;
            this.close.CornerRadius = 10;
            this.close.CornerUnder = false;
            this.close.FlatAppearance.BorderSize = 0;
            this.close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close.Font = new System.Drawing.Font("한컴 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.close.ForeColor = System.Drawing.Color.Black;
            this.close.Location = new System.Drawing.Point(545, 5);
            this.close.Margin = new System.Windows.Forms.Padding(0);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(55, 30);
            this.close.TabIndex = 4;
            this.close.Text = "닫기";
            this.close.UseVisualStyleBackColor = false;
            // 
            // next
            // 
            this.next.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.next.CornerRadius = 10;
            this.next.CornerUnder = true;
            this.next.FlatAppearance.BorderSize = 0;
            this.next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.next.Font = new System.Drawing.Font("한컴 고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.next.ForeColor = System.Drawing.Color.Black;
            this.next.Location = new System.Drawing.Point(560, 10);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(28, 28);
            this.next.TabIndex = 12;
            this.next.Text = ">";
            this.next.UseVisualStyleBackColor = false;
            // 
            // prev
            // 
            this.prev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.prev.CornerRadius = 10;
            this.prev.CornerUnder = true;
            this.prev.FlatAppearance.BorderSize = 0;
            this.prev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.prev.Font = new System.Drawing.Font("한컴 고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.prev.ForeColor = System.Drawing.Color.Black;
            this.prev.Location = new System.Drawing.Point(490, 10);
            this.prev.Name = "prev";
            this.prev.Size = new System.Drawing.Size(28, 28);
            this.prev.TabIndex = 11;
            this.prev.Text = "<";
            this.prev.UseVisualStyleBackColor = false;
            // 
            // sortLength
            // 
            this.sortLength.Active = true;
            this.sortLength.BackColor = System.Drawing.Color.Cyan;
            this.sortLength.CornerRadius = 10;
            this.sortLength.CornerUnder = true;
            this.sortLength.FlatAppearance.BorderSize = 0;
            this.sortLength.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sortLength.Font = new System.Drawing.Font("한컴 고딕", 7F);
            this.sortLength.ForeColor = System.Drawing.Color.Black;
            this.sortLength.Location = new System.Drawing.Point(269, 41);
            this.sortLength.Margin = new System.Windows.Forms.Padding(0);
            this.sortLength.Name = "sortLength";
            this.sortLength.OffColor = System.Drawing.Color.MediumAquamarine;
            this.sortLength.OffString = "짧은 단어부터";
            this.sortLength.OnColor = System.Drawing.Color.Cyan;
            this.sortLength.OnString = "긴 단어부터";
            this.sortLength.Size = new System.Drawing.Size(75, 25);
            this.sortLength.TabIndex = 9;
            this.sortLength.Text = "긴 단어부터";
            this.sortLength.UseVisualStyleBackColor = false;
            // 
            // sortFrom
            // 
            this.sortFrom.Active = true;
            this.sortFrom.BackColor = System.Drawing.Color.Cyan;
            this.sortFrom.CornerRadius = 10;
            this.sortFrom.CornerUnder = true;
            this.sortFrom.FlatAppearance.BorderSize = 0;
            this.sortFrom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sortFrom.Font = new System.Drawing.Font("한컴 고딕", 7F);
            this.sortFrom.ForeColor = System.Drawing.Color.Black;
            this.sortFrom.Location = new System.Drawing.Point(185, 41);
            this.sortFrom.Margin = new System.Windows.Forms.Padding(0);
            this.sortFrom.Name = "sortFrom";
            this.sortFrom.OffColor = System.Drawing.Color.MediumAquamarine;
            this.sortFrom.OffString = "끝 단어부터";
            this.sortFrom.OnColor = System.Drawing.Color.Cyan;
            this.sortFrom.OnString = "시작 단어부터";
            this.sortFrom.Size = new System.Drawing.Size(75, 25);
            this.sortFrom.TabIndex = 8;
            this.sortFrom.Text = "시작 단어부터";
            this.sortFrom.UseVisualStyleBackColor = false;
            // 
            // deathWord
            // 
            this.deathWord.Active = true;
            this.deathWord.BackColor = System.Drawing.Color.Lime;
            this.deathWord.CornerRadius = 10;
            this.deathWord.CornerUnder = true;
            this.deathWord.FlatAppearance.BorderSize = 0;
            this.deathWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deathWord.Font = new System.Drawing.Font("한컴 고딕", 7F);
            this.deathWord.ForeColor = System.Drawing.Color.Black;
            this.deathWord.Location = new System.Drawing.Point(100, 40);
            this.deathWord.Margin = new System.Windows.Forms.Padding(0);
            this.deathWord.Name = "deathWord";
            this.deathWord.OffColor = System.Drawing.Color.Red;
            this.deathWord.OffString = "한방 단어 표시";
            this.deathWord.OnColor = System.Drawing.Color.Lime;
            this.deathWord.OnString = "한방 단어 표시";
            this.deathWord.Size = new System.Drawing.Size(75, 25);
            this.deathWord.TabIndex = 5;
            this.deathWord.Text = "한방 단어 표시";
            this.deathWord.UseVisualStyleBackColor = false;
            // 
            // injungWord
            // 
            this.injungWord.Active = true;
            this.injungWord.BackColor = System.Drawing.Color.Lime;
            this.injungWord.CornerRadius = 10;
            this.injungWord.CornerUnder = true;
            this.injungWord.FlatAppearance.BorderSize = 0;
            this.injungWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.injungWord.Font = new System.Drawing.Font("한컴 고딕", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.injungWord.ForeColor = System.Drawing.Color.Black;
            this.injungWord.Location = new System.Drawing.Point(15, 40);
            this.injungWord.Margin = new System.Windows.Forms.Padding(0);
            this.injungWord.Name = "injungWord";
            this.injungWord.OffColor = System.Drawing.Color.Red;
            this.injungWord.OffString = "어인정 사용";
            this.injungWord.OnColor = System.Drawing.Color.Lime;
            this.injungWord.OnString = "어인정 사용";
            this.injungWord.Size = new System.Drawing.Size(75, 25);
            this.injungWord.TabIndex = 4;
            this.injungWord.Text = "어인정 사용";
            this.injungWord.UseVisualStyleBackColor = false;
            // 
            // submit
            // 
            this.submit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.submit.CornerRadius = 10;
            this.submit.CornerUnder = true;
            this.submit.FlatAppearance.BorderSize = 0;
            this.submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submit.Font = new System.Drawing.Font("한컴 고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.submit.ForeColor = System.Drawing.Color.Black;
            this.submit.Location = new System.Drawing.Point(318, 10);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(75, 28);
            this.submit.TabIndex = 2;
            this.submit.Text = "검색";
            this.submit.UseVisualStyleBackColor = false;
            // 
            // search
            // 
            this.search.Font = new System.Drawing.Font("한컴 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.search.ForeColor = System.Drawing.Color.Black;
            this.search.Location = new System.Drawing.Point(15, 10);
            this.search.Margin = new System.Windows.Forms.Padding(0);
            this.search.Name = "search";
            this.search.placeholder = "검색할 단어를 입력하세요..";
            this.search.placeholderColor = System.Drawing.Color.LightGray;
            this.search.Size = new System.Drawing.Size(300, 28);
            this.search.TabIndex = 1;
            this.search.Text = "여기에 텍스트를 입력하세요..";
            // 
            // form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.glassPanel1);
            this.Controls.Add(this.bg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "form";
            this.Text = "W";
            this.bg.ResumeLayout(false);
            this.bg.PerformLayout();
            this.glassPanel1.ResumeLayout(false);
            this.titleFrame.ResumeLayout(false);
            this.titleFrame.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel bg;
        private MoremiButton close;
        private MoremiButton minimum;
        private GlassPanel glassPanel1;
        private MoremiPanel titleFrame;
        private System.Windows.Forms.Label title;
        private MoremiButton pin;
        private BetterTextbox search;
        private MoremiButton submit;
        private MoremiRadioButton injungWord;
        private System.Windows.Forms.Label current;
        private MoremiRadioButton sortFrom;
        private MoremiRadioButton sortLength;
        private System.Windows.Forms.FlowLayoutPanel targets;
        private System.Windows.Forms.Label page;
        private MoremiButton next;
        private MoremiButton prev;
        private MoremiRadioButton deathWord;
    }
}

