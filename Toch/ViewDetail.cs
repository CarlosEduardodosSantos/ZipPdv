using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Toch
{
    public delegate void OnClickIten(object sender, EventArgs e);
    
    public class ViewDetail
    {
        public string ItenCodigo { get; set; }
        public string ItenText { get; set; }
        public string ItenValue1 { get; set; }
        public string ItenValue2 { get; set; }
        public event OnClickIten OnClickIten;

        public void PanelViewDetail<T>(List<T> DataSource, ref Panel pnlViewDetail)
        {
            int Linhas;

            if (DataSource.Count > 0)
            {
                Linhas = DataSource.Count;
                Linhas++;

                if (DataSource.Count == 0) return;
                bool FimProd = false;
                int i = 0; //espaço left
                int x = pnlViewDetail.Width / 95;
                int j = 0;
                int LinhaAtual = 1;
                int y = Linhas / x; //Verifica quantas linha tera

                //Verifica se o resultado é fracionado e adiciona mais uma linha
                double Valor = ((double)Linhas / (double)x);
                if (Valor - Math.Truncate(Valor) > 0) y++;

                while (j < y)
                {
                    i = 0;
                    while (i < x)
                    {
                        try
                        {
                            TextBoxProd BtnProd = new TextBoxProd();
                            BtnProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
                            BtnProd.ForeColor = System.Drawing.Color.Black;
                            BtnProd.Left = 95;
                            BtnProd.Parent = pnlViewDetail;
                            BtnProd.Text = DataSource[LinhaAtual - 1].GetType().InvokeMember(ItenText, BindingFlags.GetProperty, null, DataSource[LinhaAtual - 1], null).ToString();
                            BtnProd.Left = BtnProd.Left * i + 5;
                            BtnProd.Top = 4;
                            BtnProd.Top = BtnProd.Top + (53 * j);
                            BtnProd.Height = 53;
                            BtnProd.Width = 95;
                            BtnProd.CodigoProduto = (int)DataSource[LinhaAtual - 1].GetType().InvokeMember(ItenCodigo, BindingFlags.GetProperty, null, DataSource[LinhaAtual - 1], null);
                            BtnProd.ValorUnitario = decimal.Parse(DataSource[LinhaAtual - 1].GetType().InvokeMember(ItenValue1, BindingFlags.GetProperty, null, DataSource[LinhaAtual - 1], null).ToString());
                            BtnProd.TipoProduto = (string)DataSource[LinhaAtual - 1].GetType().InvokeMember(ItenValue2, BindingFlags.GetProperty, null, DataSource[LinhaAtual - 1], null);
                            BtnProd.Click += ClickIten;
                        }
                        catch (Exception) { }

                        LinhaAtual++;
                        if (Linhas == LinhaAtual) { FimProd = true; break; };

                        i++;
                    }

                    j++;
                    if (FimProd)
                        break;
                }
                
            }
        }
        protected void ClickIten(object sender, EventArgs e)
        {
            if (OnClickIten != null)
                OnClickIten(sender, e);
        }
    }
}
