using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Toch
{
    public partial class ViewDatail : System.Windows.Forms.Button
    {
        public ViewDatail()
        {
            InitializeComponent();
        }

        public ViewDatail(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        // Note that the DoubleClickTime property gets 
        // the maximum number of milliseconds allowed between 
        // mouse clicks for a double-click to be valid.
        int previousClick = System.Windows.Forms.SystemInformation.DoubleClickTime;

        public new event EventHandler DoubleClick;

        protected override void OnClick(EventArgs e)
        {
            int now = System.Environment.TickCount;

            // A double-click is detected if the the time elapsed
            // since the last click is within DoubleClickTime.
            if (now - previousClick <= System.Windows.Forms.SystemInformation.DoubleClickTime)
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

        public int ValueId { get; set; }
        public decimal Value1 { get; set; }
        public decimal Value2 { get; set; }
        public decimal Value3 { get; set; }
        public decimal Value4 { get; set; }
    }
}
