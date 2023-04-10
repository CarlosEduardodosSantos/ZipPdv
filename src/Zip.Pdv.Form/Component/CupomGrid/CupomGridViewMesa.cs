using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Eventos;

namespace Zip.Pdv.Component.CupomGrid
{
    public partial class CupomGridViewMesa : UserControl
    {
        public event EventHandler<EventArgs> TaskItem;
        public event EventHandler<EventArgs> AdicionarItem;
        public event EventHandler<EventArgs> RemoverItem;
        void taskItem(object sender, EventArgs e)
        {
            var completedEvent = TaskItem;
            if (completedEvent != null)
            {
                var item = (CupomItemBase)sender;
                completedEvent(item, e);
            }
        }

        public CupomGridViewMesa()
        {
            InitializeComponent();



        }

        void adicionarItem(object sender, EventArgs e)
        {
            var completedEvent = AdicionarItem;
            if (completedEvent != null)
            {
                var item = (CupomItemBase)sender;
                completedEvent(item, e);
            }
        }
        void removerItem(object sender, EventArgs e)
        {
            var completedEvent = RemoverItem;
            if (completedEvent != null)
            {
                var item = (CupomItemBase)sender;
                completedEvent(item, e);
            }
        }
        public List<VendaItemViewModel> DataSource { get; set; }
        public CupomItemBase SelectedItem { get; set; }
        [DefaultValue("dbe2f5")]
        public Color ColorHeader { get; set; }
        [DefaultValue(false)]
        public bool HideHeader { get; set; }
        public CupomGridViewMesa(bool habCabecalho = true)
        {
            InitializeComponent();
            HeraderPanel.Visible = habCabecalho;

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
            VendaItem.HorizontalScroll.Visible = false;

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

        public void AddItem(VendaItemViewModel item, bool disableExcluir = false)
        {

            pictureBox1.Visible = !disableExcluir;
            var largura = VendaItem.Width - 24;
            
            /*
            if (fLayoutVendaItem.VerticalScroll.Visible)
                largura -= 18;
              */

            var index = VendaItem.Controls.Count > 0 ? (VendaItem.Controls.Count) : 0;
            CupomItemBase cupomItem = null;

            if (Program.TipoPdv == ModoPdvEnumView.TotemMenu)
                cupomItem = new CupomItemTotem
                {
                    Name = item.ProdutoId.ToString(),
                    Index = index,
                    DataSource = item
                };
            else
            {
                cupomItem = new CupomItem
                {
                    Name = item.ProdutoId.ToString(),
                    Index = index,
                    DataSource = item
                };
            }
            VendaItem.Controls.Add(cupomItem);


            cupomItem.CarregaItem(disableExcluir);
            cupomItem.Width = largura;
            cupomItem.Dock = DockStyle.Top;


            cupomItem.TaskItem += taskItem;
            cupomItem.AddItem += adicionarItem;
            cupomItem.RemoveItem += removerItem;
            cupomItem.SelectItem += Item_SelectItem;

            var selected = VendaItem.Controls[VendaItem.Controls.Count - 1];
            Item_SelectItem(selected, EventArgs.Empty);

            VendaItem.VerticalScroll.Value = VendaItem.VerticalScroll.Maximum - 1;
            VendaItem.PerformLayout();
            VendaItem.Refresh();

        }

        public void AddItemPagamento(VendaItemViewModel item, bool disableExcluir = false)
        {

            pictureBox1.Visible = !disableExcluir;
            var largura = VendaItem.Width - 24;

            /*
            if (fLayoutVendaItem.VerticalScroll.Visible)
                largura -= 18;
              */

            var index = VendaItem.Controls.Count > 0 ? (VendaItem.Controls.Count) : 0;
            CupomItemBase cupomItem = null;

            cupomItem = new CupomItem
            {
                Name = item.ProdutoId.ToString(),
                Index = index,
                DataSource = item
            };

            VendaItem.Controls.Add(cupomItem);


            cupomItem.CarregaItem(disableExcluir);
            cupomItem.Width = largura;
            cupomItem.Dock = DockStyle.Top;


            cupomItem.TaskItem += taskItem;
            cupomItem.AddItem += adicionarItem;
            cupomItem.RemoveItem += removerItem;
            cupomItem.SelectItem += Item_SelectItem;

            var selected = VendaItem.Controls[VendaItem.Controls.Count - 1];
            Item_SelectItem(selected, EventArgs.Empty);

            VendaItem.VerticalScroll.Value = VendaItem.VerticalScroll.Maximum - 1;
            VendaItem.PerformLayout();
            VendaItem.Refresh();

        }

        public void Atualizar(List<VendaItemViewModel> itens, bool disableExcluir = false)
        {
            var controls = new Control[itens.Count];
            VendaItem.Controls.Clear();
            if (itens.Count == 0) return;

            for (int i = 0; i < itens.Count; i++)
            {
                var vendaItemViewModel = itens[i];

                var largura = VendaItem.Width - 24;
                /*if (fLayoutVendaItem.HorizontalScroll.Visible)
                    largura -= 18;
                    */



                CupomItemBase cupomItem = null;

                if (Program.TipoPdv == ModoPdvEnumView.TotemMenu)
                    cupomItem = new CupomItemTotem
                    {
                        Name = vendaItemViewModel.ProdutoId.ToString(),
                        Index = i,
                        DataSource = vendaItemViewModel
                    };
                else
                {
                    cupomItem = new CupomItem
                    {
                        Name = vendaItemViewModel.ProdutoId.ToString(),
                        Index = i,
                        DataSource = vendaItemViewModel
                    };
                }

                cupomItem.CarregaItem(disableExcluir);
                cupomItem.Width = largura;
                cupomItem.Dock = DockStyle.Top;

                cupomItem.TaskItem += taskItem;
                cupomItem.AddItem += AdicionarItem;
                cupomItem.RemoveItem += RemoverItem;
                cupomItem.SelectItem += Item_SelectItem;

                controls[i] = cupomItem;

            }

            VendaItem.Controls.AddRange(controls);
            var selected = VendaItem.Controls[VendaItem.Controls.Count - 1];
            Item_SelectItem(selected, EventArgs.Empty);

            VendaItem.VerticalScroll.Value = VendaItem.VerticalScroll.Maximum - 1;
            VendaItem.PerformLayout();
        }

        public void Atualizar(VendaItemViewModel item, int index, bool disableExcluir = false)
        {
            var cupomItem = (CupomItemBase)VendaItem.Controls[index];
            cupomItem.DataSource = item;
            cupomItem.CarregaItem(disableExcluir);
           

            VendaItem.PerformLayout();
        }
        private void Item_SelectItem(object sender, EventArgs e)
        {
            foreach (Control control in VendaItem.Controls)
            {
                if(control.GetType() != typeof(CupomItemBase))continue;
                
                ((CupomItemBase)control).Selected = false;
                ((CupomItemBase) control).BackColor = Color.White;

            }
            var selectedItem = (CupomItemBase) sender;

            selectedItem.Selected = !selectedItem.Selected;
            selectedItem.BackColor = selectedItem.Selected ? ColorTranslator.FromHtml("#dbe2f5") : Color.White;

            SelectedItem = selectedItem;


        }
    }
}
