using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Toch
{
    public partial class ListViewDetail : System.Windows.Forms.Panel
    {
        public ListViewDetail()
        {
            InitializeComponent();
        }

        public ListViewDetail(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public object DataSource { get; set; }
        public string ItenCodigo { get; set; }
        public string ItenText { get; set; }
        public string ItenValue1 { get; set; }
        public string ItenValue2 { get; set; }
    }
}
