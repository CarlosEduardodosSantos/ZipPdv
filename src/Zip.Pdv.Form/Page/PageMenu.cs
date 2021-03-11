using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zip.Pdv.Page
{
    public partial class PageMenu : UserControl
    {
        public UserControl Page;
        public event EventHandler<EventArgs> SelectItem;
        void selectItem(object sender, EventArgs e)
        {
            var completedEvent = SelectItem;
            if (completedEvent != null)
            {
                var item = (Button)sender;
                completedEvent(item, e);
            }
        }

        private static PageMenu _instance;
        public static PageMenu Instance => _instance ?? (_instance = new PageMenu());

        public PageMenu()
        {
            InitializeComponent();
        }
    }
}
