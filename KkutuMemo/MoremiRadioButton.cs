﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

    internal class MoremiChangingButton : MoremiButton
    {
        private List<string> strings = new List<string>();
        private List<Color> colors = new List<Color>();
        private int status = 0;
        private int max = 0;

        // Event
        private void activateButton(object sender, EventArgs e)
        {
            status += 1;
            if (status > max)
            {
                status = 0;
            }
            updateButton();
        }

        // Properties
        private void updateButton()
        {
            try
            {
                this.Text = strings[status];
                this.BackColor = colors[status];
            } catch (Exception e) {
                Debug.WriteLine(e.ToString());
            }
            //Update();
        }

        public int Status
        {
            get { return status; }
            set { status = value; updateButton(); }
        }

        public int Max
        {
            get { return max; }
            set { max = value; updateButton(); }
        }

        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            typeof(System.Drawing.Design.UITypeEditor))]
        public List<string> Strings
        {
            get { return strings; }
            set { Debug.WriteLine("텍스트 변경"); strings = value; updateButton(); }
        }

        public List<Color> Colors
        {
            get { return colors; }
            set { Debug.WriteLine("색상 변경"); colors = value; updateButton(); }
        }

        // init
        public MoremiChangingButton()
        {
            if (strings.Count == 0)
            {
                strings.Append("moremi");
            }
            if (colors.Count == 0)
            {
                colors.Append(Color.LightGray);
            }

            updateButton();
            this.MouseClick += activateButton;
        }
    }
}
