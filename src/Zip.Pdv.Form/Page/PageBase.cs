﻿using System;
using System.Windows.Forms;

namespace Zip.Pdv.Page
{
    public class PageBase : UserControl
    {
        public event EventHandler<EventArgs> CloseForm;
        protected void closeForm(object sender, EventArgs e)
        {
            var completedEvent = CloseForm;
            if (completedEvent != null)
            {
                var item = sender;
                completedEvent(item, e);
            }
        }

    }
}