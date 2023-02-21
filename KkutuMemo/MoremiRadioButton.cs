using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KkutuMemo
{
    internal class MoremiRadioButton : MoremiButton
    {
        private String onString = "ON";
        private String offString = "OFF";
        private Color onColor = Color.Lime;
        private Color offColor = Color.Red;
        private bool active = true;

        // Event
        private void activateButton(object sender, EventArgs e)
        {
            if (active == true)
            {
                active = false;
            } else
            {
                active = true;
            }
            updateButton();
        }

        // Properties
        private void updateButton()
        {
            if (active == true)
            {
                this.Text = onString;
                this.BackColor = onColor;
            }
            else
            {
                this.Text = offString;
                this.BackColor = offColor;
            }
            //Update();
        }

        public bool Active
        {
            get { return active; }
            set { active = value; updateButton(); }
        }

        public string OnString
        {
            get { return onString; }
            set { onString = value; updateButton(); }
        }

        public string OffString
        {
            get { return offString; }
            set { offString = value; updateButton(); }
        }

        public Color OnColor
        {
            get { return onColor; }
            set { onColor = value; updateButton(); }
        }

        public Color OffColor
        {
            get { return offColor; }
            set { offColor = value; updateButton(); }
        }

        // init
        public MoremiRadioButton()
        {
            updateButton();
            this.MouseClick += activateButton;
        }
    }
}
