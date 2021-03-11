using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

namespace Zip.Pdv.Component.CupomGrid
{
    public partial class CupomGridView : UserControl
    {
        public event EventHandler<EventArgs> TaskItem;
        void taskItem(object sender, EventArgs e)
        {
            var completedEvent = TaskItem;
            if (completedEvent != null)
            {
                var item = (CupomItem)sender;
                completedEvent(item, e);
            }
        }

        public List<VendaItemViewModel> DataSource { get; set; }
        public CupomItem SelectedItem { get; set; }
        [DefaultValue("FFFFFAFA")]
        public Color ColorHeader { get; set; }
        [DefaultValue(false)]
        public bool HideHeader { get; set; }
        public CupomGridView()
        {
            InitializeComponent();
            

        }

        private void CupomGridView_Load(object sender, EventArgs e)
        {
            if (HideHeader)
            {
                HeraderPanel.Visible = false;
            }
            else
                HeraderPanel.BackColor = ColorHeader;

            //HeraderPanel.Visible = false;
            fLayoutVendaItem.HorizontalScroll.Visible = false;

            CarregaGrid();
        }

        public void CarregaGrid()
        {
            //   fLayoutVendaItem.Visible = false;
            /*
               fLayoutVendaItem.Controls.Clear();
               var largura = fLayoutVendaItem.Width - 5;
               if (fLayoutVendaItem.HorizontalScroll.Enabled)
                   largura -= 20;

               if (DataSource?.Count == null || DataSource?.Count == 0)
               {
                   fLayoutVendaItem.Controls.Add(new CupomGridItemEmpty(){Width = largura});
                   return;
               }


               for (int index = 0; index < DataSource.Count; index++)
               {
                   var vendaItem = DataSource[index];
                   var item = new CupomItem
                   {
                       Index = index,
                       DataSource = vendaItem,
                       Width = largura,
                   };

                   item.SelectItem += Item_SelectItem;
                   fLayoutVendaItem.Controls.Add(item);
               }
               fLayoutVendaItem.Visible = true;
               var selected = fLayoutVendaItem.Controls[fLayoutVendaItem.Controls.Count - 1];
               Item_SelectItem(selected, EventArgs.Empty);

               fLayoutVendaItem.VerticalScroll.Value = fLayoutVendaItem.VerticalScroll.Maximum - 1;
               fLayoutVendaItem.PerformLayout();*/
        }

        public void AddItem(VendaItemViewModel item)
        {
            
            var largura = fLayoutVendaItem.Width - 24;
            
            /*
            if (fLayoutVendaItem.VerticalScroll.Visible)
                largura -= 18;
              */

            var index = fLayoutVendaItem.Controls.Count > 0 ? (fLayoutVendaItem.Controls.Count) : 0;
            var cupomItem = new CupomItem
            {
                Name = item.ProdutoId.ToString(),
                Index = index,
                DataSource = item
            };
            fLayoutVendaItem.Controls.Add(cupomItem);


            cupomItem.CarregaItem();
            cupomItem.Width = largura;
            cupomItem.Dock = DockStyle.Top;


            cupomItem.TaskItem += taskItem;
            cupomItem.SelectItem += Item_SelectItem;

            var selected = fLayoutVendaItem.Controls[fLayoutVendaItem.Controls.Count - 1];
            Item_SelectItem(selected, EventArgs.Empty);

            fLayoutVendaItem.VerticalScroll.Value = fLayoutVendaItem.VerticalScroll.Maximum - 1;
            fLayoutVendaItem.PerformLayout();
            fLayoutVendaItem.Refresh();

        }

        public void Atualizar(List<VendaItemViewModel> itens)
        {
            var controls = new Control[itens.Count];
            fLayoutVendaItem.Controls.Clear();
            if (itens.Count == 0) return;

            for (int i = 0; i < itens.Count; i++)
            {
                var vendaItemViewModel = itens[i];

                var largura = fLayoutVendaItem.Width - 24;
                /*if (fLayoutVendaItem.HorizontalScroll.Visible)
                    largura -= 18;
                    */

                var cupomItem = new CupomItem
                {
                    Name = vendaItemViewModel.ProdutoId.ToString(),
                    Index = i,
                    DataSource = vendaItemViewModel
                };

                cupomItem.CarregaItem();
                cupomItem.Width = largura;
                cupomItem.Dock = DockStyle.Top;

                cupomItem.TaskItem += taskItem;
                cupomItem.SelectItem += Item_SelectItem;

                controls[i] = cupomItem;

            }

            fLayoutVendaItem.Controls.AddRange(controls);
            var selected = fLayoutVendaItem.Controls[fLayoutVendaItem.Controls.Count - 1];
            Item_SelectItem(selected, EventArgs.Empty);

            fLayoutVendaItem.VerticalScroll.Value = fLayoutVendaItem.VerticalScroll.Maximum - 1;
            fLayoutVendaItem.PerformLayout();
        }

        public void RemoveItem(VendaItemViewModel item)
        {
            
        }
        private void Item_SelectItem(object sender, EventArgs e)
        {
            foreach (Control control in fLayoutVendaItem.Controls)
            {
                if(control.GetType() != typeof(CupomItem))continue;
                
                ((CupomItem)control).Selected = false;
                ((CupomItem) control).BackColor = Color.White;

            }
            var selectedItem = (CupomItem) sender;

            selectedItem.Selected = !selectedItem.Selected;
            selectedItem.BackColor = selectedItem.Selected ? Color.Cyan : Color.White;

            SelectedItem = selectedItem;


        }
    }
}
