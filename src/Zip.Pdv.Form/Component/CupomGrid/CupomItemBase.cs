using System;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

namespace Zip.Pdv.Component.CupomGrid
{
    public abstract class CupomItemBase : UserControl
    {
        public event EventHandler<EventArgs> TaskItem;
        public event EventHandler<EventArgs> SelectItem;
        public event EventHandler<EventArgs> AddItem;
        public event EventHandler<EventArgs> RemoveItem;

        public VendaItemViewModel DataSource { get; set; }
        public bool Selected { get; set; }
        public int Index { get; set; }
        public void taskItem(object sender, EventArgs e)
        {
            var completedEvent = TaskItem;
            if (completedEvent != null)
            {
                var item = (CupomItemBase)this;
                completedEvent(item, e);
            }
        }
        public void addItem(object sender, EventArgs e)
        {
            var completedEvent = AddItem;
            if (completedEvent != null)
            {
                var item = (CupomItemBase)this;
                completedEvent(item, e);
            }
        }
        public void removeItem(object sender, EventArgs e)
        {
            var completedEvent = RemoveItem;
            if (completedEvent != null)
            {
                var item = (CupomItemBase)this;
                completedEvent(item, e);
            }
        }
        public void selectItem(object sender, EventArgs e)
        {
            var completedEvent = SelectItem;
            if (completedEvent != null)
            {
                var item = (CupomItemBase)this;
                completedEvent(item, e);
            }
        }

        public abstract void CarregaItem(bool disableExcluir = false);

    }
}
