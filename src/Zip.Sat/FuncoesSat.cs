using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Zip.Sat.Libraries.Model;

namespace Zip.Sat
{
    public class FuncoesSat
    {
        private Byte[] m_ByteCodes;
        private string m_126;
        private string m_8;
        private string m_96;
        private string m_39;
        private string m_94;
        private Encoding m_Ascii;

        public string LimparTexto(string _text)
        {
            m_ByteCodes = new Byte[] { 126, 8, 96, 39, 94 };
            m_Ascii = Encoding.ASCII;
            m_126 = m_Ascii.GetString(m_ByteCodes, 0, 1);
            m_8 = m_Ascii.GetString(m_ByteCodes, 1, 1);
            m_96 = m_Ascii.GetString(m_ByteCodes, 2, 1);
            m_39 = m_Ascii.GetString(m_ByteCodes, 3, 1);
            m_94 = m_Ascii.GetString(m_ByteCodes, 4, 1);

            StringBuilder s = new StringBuilder(_text);
            s = s.Replace("à", m_96 + m_8 + "a");
            s = s.Replace("â", m_94 + m_8 + "a");
            s = s.Replace("ê", m_96 + m_8 + "e");
            s = s.Replace("ô", m_96 + m_8 + "o");
            s = s.Replace("û", m_96 + m_8 + "u");
            s = s.Replace("ã", m_126 + m_8 + "a");
            s = s.Replace("õ", m_126 + m_8 + "o");
            s = s.Replace("á", m_39 + m_8 + "a");
            s = s.Replace("é", m_39 + m_8 + "e");
            s = s.Replace("í", m_39 + m_8 + "i");
            s = s.Replace("ó", m_39 + m_8 + "o");
            s = s.Replace("ú", m_39 + m_8 + "u");
            s = s.Replace("ç", "," + m_8 + "c");
            s = s.Replace("ü", "u");
            s = s.Replace("À", m_96 + m_8 + "A");
            s = s.Replace("Â", m_94 + m_8 + "A");
            s = s.Replace("Ê", m_94 + m_8 + "E");
            s = s.Replace("Ô", m_94 + m_8 + "O");
            s = s.Replace("Û", m_94 + m_8 + "U");
            s = s.Replace("Ã", m_126 + m_8 + "A");
            s = s.Replace("Õ", m_126 + m_8 + "O");
            s = s.Replace("Á", m_39 + m_8 + "A");
            s = s.Replace("É", m_39 + m_8 + "E");
            s = s.Replace("Í", m_39 + m_8 + "I");
            s = s.Replace("Ó", m_39 + m_8 + "O");
            s = s.Replace("Ú", m_39 + m_8 + "U");
            s = s.Replace("Ç", "," + m_8 + "C");
            s = s.Replace("Ü", "U");
            s = s.Replace("&", " E ");
            s = s.Replace("º", " ");
            s = s.Replace("ª", " ");
            s = s.Replace("°", " ");
            s = s.Replace("^", "");
            s = s.Replace("'", "");
            s = s.Replace("~", "");
            s = s.Replace("?", "");
            s = s.Replace("´", "");
            s = s.Replace("\b", "");
            s = s.Replace(",", "");
            s = s.Replace("<", "");
            s = s.Replace(">", "");
            s = s.Replace(@"\", "");
            s = s.Replace("/", "");
            s = s.Replace("¦", "");
            return s.ToString().Trim();
        }
        public string Zeros(string _valor, int _tamanho)
        {
            return _valor.PadLeft(_tamanho).Replace(" ", "0");
        }

        public DialogResult MensagemDeInformacao(string _msg)
        {
            return MessageBox.Show(_msg, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public DialogResult MensagemDeAviso(string _msg)
        {
            return MessageBox.Show(_msg, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public DialogResult MensagemDeErro(string _msg)
        {
            return MessageBox.Show(_msg, "E r r o", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public DialogResult MensagemDeConfirmacao(string _msg, string _caption = "Atenção")
        {
            return MessageBox.Show(_msg, _caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }
        public DialogResult MensagemDeConfirmacao(string _msg, MessageBoxDefaultButton _defaultButton, string _caption = "Atenção")
        {
            return MessageBox.Show(_msg, _caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, _defaultButton);
        }
        public DialogResult MensagemDeConfirmacaoComCancel(string _msg, string _caption = "Atenção")
        {
            return MessageBox.Show(_msg, _caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }
        public DialogResult MensagemDeConfirmacaoComCancel(string _msg, MessageBoxDefaultButton _defaultButton, string _caption = "Atenção")
        {
            return MessageBox.Show(_msg, _caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, _defaultButton);
        }

        public short ConvertToInt16(object _value, short defaltulValue = 0)
        {
            try
            {
                if (_value == null || string.IsNullOrEmpty(_value.ToString()))
                    return defaltulValue;
                return Convert.ToInt16(_value);
            }
            catch
            {
                return defaltulValue;
            }
        }
        public int ConvertToInt32(object _value, int defaltulValue = 0)
        {
            try
            {
                if (_value == null || string.IsNullOrEmpty(_value.ToString()))
                    return defaltulValue;
                return Convert.ToInt32(_value);
            }
            catch
            {
                return defaltulValue;
            }
        }
        public long ConvertToInt64(object _value, long defaltulValue = 0)
        {
            try
            {
                if (_value == null || string.IsNullOrEmpty(_value.ToString()))
                    return defaltulValue;
                return Convert.ToInt64(_value);
            }
            catch
            {
                return defaltulValue;
            }
        }
        public float ConvertToSingle(object _value, float defaltulValue = 0f)
        {
            try
            {
                if (_value == null || string.IsNullOrEmpty(_value.ToString()))
                    return defaltulValue;
                return Convert.ToSingle(_value);
            }
            catch
            {
                return defaltulValue;
            }
        }
        public double ConvertToDouble(object _value, double defaltulValue = 0d)
        {
            try
            {
                if (_value == null || string.IsNullOrEmpty(_value.ToString()))
                    return defaltulValue;
                return Convert.ToDouble(_value);
            }
            catch
            {
                return defaltulValue;
            }
        }
        public decimal ConvertToDecimal(object _value, decimal defaltulValue = 0m)
        {
            try
            {
                if (_value == null || string.IsNullOrEmpty(_value.ToString()))
                    return defaltulValue;
                return Convert.ToDecimal(_value);
            }
            catch
            {
                return defaltulValue;
            }
        }
        public bool ConvertToBoolean(object _value, bool defaltulValue = false)
        {
            try
            {
                if (_value == null || string.IsNullOrEmpty(_value.ToString()))
                    return defaltulValue;
                return Convert.ToBoolean(_value);
            }
            catch
            {
                return defaltulValue;
            }
        }
        public DateTime ConvertToDateTime(object _value, DateTime? defaltulValue = null)
        {
            try
            {
                if (_value == null || string.IsNullOrEmpty(_value.ToString()))
                    return defaltulValue == null ? DateTime.MinValue : defaltulValue.Value;
                return Convert.ToDateTime(_value);
            }
            catch
            {
                return defaltulValue == null ? DateTime.MinValue : defaltulValue.Value;
            }
        }
        public DateTime ConvertToDateTime_yyyyMMdd(string _value)
        {
            try
            {
                return new DateTime
                    (Convert.ToInt32(_value.Substring(0, 4)),
                     Convert.ToInt32(_value.Substring(4, 2)),
                     Convert.ToInt32(_value.Substring(6, 2)));
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Erro ao tentar converter Data: " + _value.ToString());
            }
        }
        public DateTime GetDateToNfpExtension(object _extensionValue)
        {
            try
            {
                string extensionValue = ConvertToString(_extensionValue).ToUpper().Replace(".", "");

                IDictionary<string, int> dic = new Dictionary<string, int>();
                dic.Add("1", 1); dic.Add("2", 2); dic.Add("3", 3); dic.Add("4", 4); dic.Add("5", 5);
                dic.Add("6", 6); dic.Add("7", 7); dic.Add("8", 8); dic.Add("9", 9); dic.Add("A", 10);
                dic.Add("B", 11); dic.Add("C", 12); dic.Add("D", 13); dic.Add("E", 14); dic.Add("F", 15);
                dic.Add("G", 16); dic.Add("H", 17); dic.Add("I", 18); dic.Add("J", 19); dic.Add("L", 20);
                dic.Add("M", 21); dic.Add("N", 22); dic.Add("O", 23); dic.Add("P", 24); dic.Add("Q", 25);
                dic.Add("R", 26); dic.Add("S", 27); dic.Add("T", 28); dic.Add("U", 29); dic.Add("V", 30);
                dic.Add("X", 31); dic.Add("Z", 32);

                string data = "";
                for (int i = 0; i < 3; i++)
                    data += dic[extensionValue[i].ToString()];

                return DateTime.ParseExact(data, "ddMMyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DateTime? GetDateTimeOrNull(object _value)
        {
            try
            {
                return Convert.ToDateTime(_value);
            }
            catch
            {
                return null;
            }
        }
        public string ConvertToString(object _value, string defaltulValue = "")
        {
            try
            {
                if (_value == null || string.IsNullOrEmpty(_value.ToString()))
                    return defaltulValue;
                return Convert.ToString(_value);
            }
            catch
            {
                return defaltulValue;
            }
        }

        public VendaSatModel ConvetVendaToVendaSat(VendaViewModel venda)
        {
            try
            {
                var vendaSat = new VendaSatModel();
                vendaSat.VendaID = venda.VendaId;
                vendaSat.Cliente.Cpf = venda.Cnpj;
                vendaSat.EmpresaId = venda.Loja;
                vendaSat.Pdv = venda.Pdv;
                vendaSat.SenhaPainel = venda.Senha;
                vendaSat.DataVenda = venda.DataHora;

                foreach (var vendaItemViewModel in venda.VendaItens)
                {
                    using (var produtoAppService = ServiceLocator.Current.GetInstance<IProdutoAppService>())
                    {
                        var produto = produtoAppService.ObterPorId(vendaItemViewModel.ProdutoId);
                        var produtoTributacao = produtoAppService.ObterTributacaoPorProdutoId(vendaItemViewModel.ProdutoId);

                        if (produto == null)
                            throw new Exception("Produto não encontrado");

                        if (produtoTributacao == null)
                            throw new Exception("Tributação não encontrada.");


                        vendaSat.Itens.Add(
                            new VendaItemSatModel()
                            {
                                ProdutoID = vendaItemViewModel.ProdutoId,
                                Produto = new ProdutoSatModel() { Descricao = Utils.Funcoes.RemoveAccents( vendaItemViewModel.Produto), Unidade = produto.Unidade },
                                Qtde = vendaItemViewModel.Quantidade,
                                Valor = vendaItemViewModel.ValorUnitatio,
                                ValorItem = vendaItemViewModel.ValorUnitatio,
                                Ncm = produtoTributacao.NcmCodigo,
                                Cfop = produtoTributacao.Cfop,
                                CestCodigo = produtoTributacao.CestCodigo,
                                NcmImpostoFederal = ((vendaItemViewModel.ValorTotal / 100) * produtoTributacao.NcmAliquotaFederalNacional),
                                NcmImpostoEstadual = ((vendaItemViewModel.ValorTotal / 100) * produtoTributacao.NcmAliquotaEstadual),
                                ProdutoOrigem = produtoTributacao.Origem,
                                IcmsCstCsosn = produtoTributacao.IcmsCstSaida,
                                IcmsAliquota = produtoTributacao.IcmsAliquotaSaida,
                                PisCofinsCst = produtoTributacao.PisCofinsCstSaida,
                                PisAliquota = produtoTributacao.PisAliquota,
                                CofinsAliquota = produtoTributacao.CofinsAliquota
                            }
                        );

                    }


                }

                vendaSat.ValorTotal = venda.ValorTotal;
                vendaSat.ValorFinal = venda.ValorFinal;

                foreach (var caixaPagamentoViewModel in venda.VendaFinalizadora)
                {
                    vendaSat.Finalizadoras.Add(
                        new VendaFinalizadoraSatModel()
                        {
                            CodigoFinalizadora = caixaPagamentoViewModel.CodigoFiscal,
                            Valor = caixaPagamentoViewModel.Valor
                        }
                    );
                }
                return vendaSat;
            }
            catch (Exception e)
            {
                
                throw new Exception("Erro na função ConvetVendaToVendaSat: " + e.Message);
            }
            
        }
    }
}