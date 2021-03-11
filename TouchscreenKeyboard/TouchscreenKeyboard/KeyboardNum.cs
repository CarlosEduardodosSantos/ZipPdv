using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace KeyboardClassLibrary.Num
{
    public partial class KeyboardNum : UserControl
    {
        public KeyboardNum()
        {
            InitializeComponent();
        }

        private Boolean shiftindicator = false;
        private Boolean capslockindicator = false;
        private string pvtKeyboardKeyPressed = "";

        [Category("Mouse"), Description("Return value of mouseclicked key")]
        public event KeyboardNumDelegate UserKeyPressed;
        protected virtual void OnUserKeyPressed(KeyboardNumEventArgs e)
        {
            if (UserKeyPressed != null)
                UserKeyPressed(this, e);
        }

        private void buttonBoxKeyboard_MouseClick(object sender, MouseEventArgs e)
        {
            var pvtKeyboardKeyPressed = string.Empty;

            if (sender.GetType() == typeof(Label))
            {
                var obj = (Label)sender;
                pvtKeyboardKeyPressed = (string)obj.Tag;
            }
            else
            {
                var obj = (PictureBox)sender;
                pvtKeyboardKeyPressed = (string)obj.Tag;
            }
           
            KeyboardNumEventArgs dea = new KeyboardNumEventArgs(pvtKeyboardKeyPressed);

            OnUserKeyPressed(dea);
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(Label))
            {
                var obj = (Label)sender;
                obj.BackColor = Color.Azure;
            }
            else
            {
                var obj = (PictureBox)sender;
                obj.BackColor = Color.Azure;
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(Label))
            {
                var obj = (Label)sender;
                obj.BackColor = Color.White;
            }
            else
            {
                var obj = (PictureBox)sender;
                obj.BackColor = Color.White;
            }
        }
    }

    public delegate void KeyboardNumDelegate(object sender, KeyboardNumEventArgs e);

    public class KeyboardNumEventArgs : EventArgs
    {
        private readonly string pvtKeyboardKeyPressed;

        public KeyboardNumEventArgs(string KeyboardKeyPressed)
        {
            this.pvtKeyboardKeyPressed = KeyboardKeyPressed;
        }

        public string KeyboardKeyPressed
        {
            get
            {
                return pvtKeyboardKeyPressed;
            }
        }
    }


}
