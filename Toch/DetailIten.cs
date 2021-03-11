using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Toch
{
    public partial class DetailIten : Button
    {
        public int ValueInt { get; set; }
        public int ValueInt2 { get; set; }
        public int ValueInt3 { get; set; }
        public decimal ValueDecimal { get; set; }
        public decimal ValueDecimal2 { get; set; }
        public decimal ValueDecimal3 { get; set; }
        public string ValueString { get; set; }
        public string ValueString2 { get; set; }
        public string ValueString3 { get; set; }
        public DateTime ValueDateTime { get; set; }
        public DateTime ValueDateTime2 { get; set; }
        

        public DetailIten()
        {
            InitializeComponent();
        }

        public DetailIten(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
