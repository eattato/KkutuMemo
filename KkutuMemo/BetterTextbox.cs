using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KkutuMemo
{
    internal class BetterTextbox : TextBox
    {
        private string placeholderText = "여기에 텍스트를 입력하세요..";
        private Color phColor = Color.LightGray;

        public string placeholder {
            get { return placeholderText; }
            set { placeholderText = value; OnChange(null, null); }
        }
        public Color placeholderColor {
            get { return phColor; }
            set { phColor = value; }
        }

        private bool empty = true;
        private Color colorSave;
        private bool antiEvent = false;

        public BetterTextbox()
        {
            colorSave = this.ForeColor;
            if (this.Text.Length > 0)
            {
                empty = false;
            }
            OnChange(null, null);
            OutFocus(null, null);

            // Bind events
            this.TextChanged += OnChange;
            this.GotFocus += OnFocus;
            this.LostFocus += OutFocus;
        }

        private void OnChange(object sender, EventArgs e)
        {
            if (antiEvent == false)
            {
                if (this.Text.Length > 0)
                {
                    empty = false;
                } else
                {
                    empty = true;
                }
            }
        }

        private void OnFocus(object sender, EventArgs e)
        {
            if (empty == true)
            {
                antiEvent = true;
                this.Text = "";
                this.ForeColor = colorSave;
                antiEvent = false;
            }
        }

        private void OutFocus(object sender, EventArgs e)
        {
            if (empty == true)
            {
                antiEvent = true;
                this.Text = placeholder;
                this.ForeColor = phColor;
                antiEvent = false;
            }
        }
    }
}
