using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Toch
{
    public partial class TextBoxProd : System.Windows.Forms.Button
    {
        public TextBoxProd()
        {
            InitializeComponent();

        }
        public int CodigoProduto { get; set; }

        public decimal ValorUnitario { get; set; }

        public int CodigoMeio1 { get; set; }

        public int CodigoMeio2 { get; set; }

        public string TipoProduto { get; set; }

        public int TopOld { get; set; }
        public int LeftOld { get; set; }
        public DateTime DataUltimoAtendimento { get; set; }

        public TextBoxProd(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        // Note that the DoubleClickTime property gets 
        // the maximum number of milliseconds allowed between 
        // mouse clicks for a double-click to be valid.
        int previousClick = SystemInformation.DoubleClickTime;

        public new event EventHandler DoubleClick;

        protected override void OnClick(EventArgs e)
        {
            int now = System.Environment.TickCount;

            // A double-click is detected if the the time elapsed
            // since the last click is within DoubleClickTime.
            if (now - previousClick <= SystemInformation.DoubleClickTime)
            {
                // Raise the DoubleClick event.
                if (DoubleClick != null)
                    DoubleClick(this, EventArgs.Empty);
            }

            // Set previousClick to now so that 
            // subsequent double-clicks can be detected.
            previousClick = now;
            // Allow the base class to raise the regular Click event.
            base.OnClick(e);
        }

        // Event handling code for the DoubleClick event.
        protected new virtual void OnDoubleClick(EventArgs e)
        {
            if (this.DoubleClick != null)
                this.DoubleClick(this, e);
        }

        protected override void OnLeave(System.EventArgs e)
        {
            this.BackColor = _oldBackColor;
        }

        protected override void OnEnter(System.EventArgs e)
        {
            _oldBackColor = this.BackColor;
            if (!BackColorEnter.IsEmpty)
                this.BackColor = BackColorEnter;
        }

        private Color _oldBackColor;
        public Color BackColorEnter { get; set; }


    }
}
