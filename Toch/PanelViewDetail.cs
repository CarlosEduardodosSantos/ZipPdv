using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Toch
{
    public delegate void OnClickDetailIten(object sender, EventArgs e);

    public partial class PanelViewDetail : Panel
    {
        public string ValueInt { get; set; }
        public string ValueInt2 { get; set; }
        public string ValueInt3 { get; set; }
        public string ValueDecimal { get; set; }
        public string ValueDecimal2 { get; set; }
        public string ValueDecimal3 { get; set; }
        public string ValueString { get; set; }
        public string ValueString2 { get; set; }
        public string ValueString3 { get; set; }
        public string ValueDateTime { get; set; }
        public string ValueDateTime2 { get; set; }
        public string DisplayVaue { get; set; }

        public int WidthDetailIten { get; set; }
        public int HeightDetailIten { get; set; }

        public event OnClickDetailIten OnClickDetailIten;

        public PanelViewDetail()
        {
            InitializeComponent();
        }

        protected void ClickDetailIten(object sender, EventArgs e)
        {
            if (OnClickDetailIten != null)
                OnClickDetailIten(sender, e);
        }

        public PanelViewDetail(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void DataSource<T>(List<T> DataSource)
        {
            int Linhas;

            if (DataSource.Count > 0)
            {
                Linhas = DataSource.Count;
                Linhas++;

                if (DataSource.Count == 0) return;
                bool FimProd = false;
                int i = 0; //espaço left
                int QuantidadeLinha = this.Width / WidthDetailIten;
                int TamanhoEspaco = (QuantidadeLinha * 5);
                int x = (this.Width - TamanhoEspaco) / WidthDetailIten;
                int j = 0;
                int LinhaAtual = 1;
                int y = Linhas / x; //Verifica quantas linha tera
                int LeftEspaco = 5;
                int TopEspaco = 5;
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
                            DetailIten BtnIten = new DetailIten();
                            BtnIten.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
                            BtnIten.FlatStyle = FlatStyle.Flat;
                            BtnIten.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                            BtnIten.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                            BtnIten.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
                            BtnIten.ForeColor = System.Drawing.Color.Black;
                            BtnIten.Left = WidthDetailIten;
                            BtnIten.Parent = this;
                            BtnIten.Left = (WidthDetailIten * i) + LeftEspaco;
                            BtnIten.Top = TopEspaco;
                            BtnIten.Top = BtnIten.Top + (HeightDetailIten * j);
                            BtnIten.Height = HeightDetailIten;
                            BtnIten.Width = WidthDetailIten;
                            BtnIten.Click += ClickDetailIten;

                            if (!string.IsNullOrEmpty(DisplayVaue))
                                BtnIten.Text = DataSource[LinhaAtual - 1].GetType().InvokeMember(DisplayVaue, BindingFlags.GetProperty, null, DataSource[LinhaAtual - 1], null).ToString();
                            if (!string.IsNullOrEmpty(ValueString2))
                                BtnIten.ValueString2 = (string)DataSource[LinhaAtual - 1].GetType().InvokeMember(ValueString2, BindingFlags.GetProperty, null, DataSource[LinhaAtual - 1], null);
                            if (!string.IsNullOrEmpty(ValueString3))
                                BtnIten.ValueString3 = (string)DataSource[LinhaAtual - 1].GetType().InvokeMember(ValueString3, BindingFlags.GetProperty, null, DataSource[LinhaAtual - 1], null);
                            
                            if (!string.IsNullOrEmpty(ValueDecimal))
                                BtnIten.ValueDecimal = (Decimal)DataSource[LinhaAtual - 1].GetType().InvokeMember(ValueDecimal, BindingFlags.GetProperty, null, DataSource[LinhaAtual - 1], null);
                            if (!string.IsNullOrEmpty(ValueDecimal2))
                                BtnIten.ValueDecimal2 = (Decimal)DataSource[LinhaAtual - 1].GetType().InvokeMember(ValueDecimal2, BindingFlags.GetProperty, null, DataSource[LinhaAtual - 1], null);
                            if (!string.IsNullOrEmpty(ValueDecimal3))
                                BtnIten.ValueDecimal3 = (Decimal)DataSource[LinhaAtual - 1].GetType().InvokeMember(ValueDecimal3, BindingFlags.GetProperty, null, DataSource[LinhaAtual - 1], null);

                            if (!string.IsNullOrEmpty(ValueInt))
                                BtnIten.ValueInt = (int)DataSource[LinhaAtual - 1].GetType().InvokeMember(ValueInt, BindingFlags.GetProperty, null, DataSource[LinhaAtual - 1], null);
                            if (!string.IsNullOrEmpty(ValueInt2))
                                BtnIten.ValueInt2 = (int)DataSource[LinhaAtual - 1].GetType().InvokeMember(ValueInt2, BindingFlags.GetProperty, null, DataSource[LinhaAtual - 1], null);
                            if (!string.IsNullOrEmpty(ValueInt3))
                                BtnIten.ValueInt3 = (int)DataSource[LinhaAtual - 1].GetType().InvokeMember(ValueInt3, BindingFlags.GetProperty, null, DataSource[LinhaAtual - 1], null);

                            if (!string.IsNullOrEmpty(ValueDateTime))
                                BtnIten.ValueDateTime = (DateTime)DataSource[LinhaAtual - 1].GetType().InvokeMember(ValueDateTime, BindingFlags.GetProperty, null, DataSource[LinhaAtual - 1], null);
                            if (!string.IsNullOrEmpty(ValueDateTime2))
                                BtnIten.ValueDateTime2 = (DateTime)DataSource[LinhaAtual - 1].GetType().InvokeMember(ValueDateTime2, BindingFlags.GetProperty, null, DataSource[LinhaAtual - 1], null);

                        }
                        catch (Exception) { }
                        LeftEspaco += 5;
                        LinhaAtual++;
                        if (Linhas == LinhaAtual) { FimProd = true; break; };

                        i++;
                    }
                    LeftEspaco = 5;
                    TopEspaco += 5;
                    j++;
                    if (FimProd)
                        break;
                }

            }
        }

        private void PanelViewDetail_Resize(object sender, EventArgs e)
        {
            if (Controls.Count > 0)
            {
                int Linhas;
                Linhas = Controls.Count;
                Linhas++;

                bool FimProd = false;
                int i = 0; //espaço left
                int QuantidadeLinha = this.Width / WidthDetailIten;
                int TamanhoEspaco = (QuantidadeLinha * 5);
                int x = (this.Width - TamanhoEspaco) / WidthDetailIten;
                int j = 0;
                int LinhaAtual = 1;
                int y = Linhas / x; //Verifica quantas linha tera
                int LeftEspaco = 5;
                int TopEspaco = 5;

                //Verifica se o resultado é fracionado e adiciona mais uma linha
                double Valor = ((double)Linhas / (double)x);
                if (Valor - Math.Truncate(Valor) > 0) y++;

                while (j < y)
                {
                    i = 0;
                    while (i < x)
                    {

                        this.Controls[LinhaAtual - 1].Left = (WidthDetailIten * i) + LeftEspaco;
                        this.Controls[LinhaAtual - 1].Top = TopEspaco;
                        this.Controls[LinhaAtual - 1].Top = this.Controls[LinhaAtual - 1].Top + (HeightDetailIten * j);
                        this.Controls[LinhaAtual -1].Height = HeightDetailIten;
                        this.Controls[LinhaAtual -1].Width = WidthDetailIten;


                        LinhaAtual++;
                        if (Linhas == LinhaAtual) { FimProd = true; break; };

                        LeftEspaco += 5;
                        i++;
                    }
                    LeftEspaco = 5;
                    TopEspaco += 5;
                    j++;
                    if (FimProd)
                        break;

                }
            }
        }

    }
}
