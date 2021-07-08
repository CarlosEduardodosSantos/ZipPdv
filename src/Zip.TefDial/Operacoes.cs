using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Eticket.Application.ViewModels;
using Zip.Utils;

namespace Zip.TefDial
{
    public static class Operacoes
    {
        public static CartaoRespostaViewModel ProcessaResposta(string fileResp)
        {
            var cartaoRespostaView = new CartaoRespostaViewModel();
            var cartaoRespostaParcelaView = new CartaoRespostaParcelaViewModel();
            int parcelas = 0;
            int quantidadeParcelas = 0;
            int linhaComprovante = 0;

            StreamReader file = null;

            var maxRetryAttempts = 2;
            var pauseBetweenFailures = TimeSpan.FromSeconds(2);
            RetryHelper.RetryOnException(maxRetryAttempts, pauseBetweenFailures, () =>
            {
                file = new StreamReader(fileResp,
                    Encoding.GetEncoding(CultureInfo.GetCultureInfo("pt-BR").TextInfo.ANSICodePage));

            });

            string line;
            while ((line = file.ReadLine()) != null)
            {
                var herader = line.ToRegistro();
                if (herader == "000-000")
                {
                    var valor = line.ToRegistroValor();

                    var tipoOperacao =
                        (CartaoTipoOperacaoEnumView) Enum.Parse(typeof(CartaoTipoOperacaoEnumView), valor, true);
                    cartaoRespostaView.TipoOperacao = tipoOperacao;
                    continue;
                }
                if (herader == "001-000")
                {
                    var valor = line.ToRegistroValor();

                    cartaoRespostaView.Requisicao = int.Parse(valor);
                    continue;
                }
                if (herader == "002-000")
                {
                    var valor = line.ToRegistroValor();

                    cartaoRespostaView.Vinculado = valor;
                    continue;
                }
                if (herader == "003-000")
                {
                    var valor = line.ToRegistroValor();
                    var valorTotal = decimal.Parse(valor) / 100;
                    cartaoRespostaView.Valor = valorTotal;
                    continue;
                }
                if (herader == "009-000")
                {
                    var valor = line.ToRegistroValor();

                    cartaoRespostaView.Autorizado = valor == "0" ? true : false;
                    continue;
                }
                if (herader == "013-000")
                {
                    var valor = line.ToRegistroValor();

                    cartaoRespostaView.CodigoAutorizacao = valor;
                    continue;
                }
                if (herader == "010-000")
                {
                    var valor = line.ToRegistroValor();

                    cartaoRespostaView.NomeRede = valor;
                    continue;
                }
                if (herader == "010-001")
                {
                    var valor = line.ToRegistroValor();

                    cartaoRespostaView.CnpjRede = valor;
                    continue;
                }
                if (herader == "010-002")
                {
                    var valor = line.ToRegistroValor();

                    cartaoRespostaView.CodigoSat = valor;
                    continue;
                }
                if (herader == "011-000")
                {
                    var valor = line.ToRegistroValor();

                    cartaoRespostaView.TipoTransacao = (CartaoTipoTransacaoEnumView) int.Parse(valor);
                    continue;
                }
                if (herader == "012-000")
                {
                    var valor = line.ToRegistroValor();

                    cartaoRespostaView.CodigoNsu = valor;
                    continue;
                }
                if (herader == "013-000")
                {
                    var valor = line.ToRegistroValor();

                    cartaoRespostaView.CodigoAutorizacao = valor;
                    continue;
                }
                if (herader == "014-000")
                {
                    var valor = line.ToRegistroValor();

                    cartaoRespostaView.LoteAutorizacao = valor;
                    continue;
                }
                if (herader == "015-000")
                {
                    var valor = line.ToRegistroValor();
                    var dia = int.Parse(valor.Substring(0, 2));
                    var mes = int.Parse(valor.Substring(2, 2));
                    var ano = DateTime.Now.Year;

                    var data = new DateTime(ano, mes, dia);

                    cartaoRespostaView.DataTransacao = data;
                    continue;
                }

                if (herader == "022-000")
                {
                    var valor = line.ToRegistroValor();
                    var dia = int.Parse(valor.Substring(0, 2));
                    var mes = int.Parse(valor.Substring(2, 2));
                    var ano = DateTime.Now.Year;

                    var data = new DateTime(ano, mes, dia);

                    cartaoRespostaView.DataComprovante = DateTime.Now;
                    continue;
                }
                if (herader == "023-000")
                {
                    var valor = line.ToRegistroValor();
                    var hora = int.Parse(valor.Substring(0, 2));
                    var mim = int.Parse(valor.Substring(2, 2));

                    cartaoRespostaView.DataComprovante = cartaoRespostaView.DataComprovante.AddHours(hora)
                        .AddMinutes(mim);

                    continue;
                }
                if (herader == "030-000")
                {
                    var valor = line.ToRegistroValor();

                    cartaoRespostaView.Menssagem = valor;
                    continue;
                }
                if (herader == "040-000")
                {
                    var valor = line.ToRegistroValor();

                    cartaoRespostaView.Bandeira = valor;
                    continue;
                }
                if (herader == "740-000")
                {
                    var valor = line.ToRegistroValor();

                    cartaoRespostaView.NumeroCartao = valor;
                    continue;
                }
                if (herader == "743-000")
                {
                    var valor = line.ToRegistroValor();
                    var valorRestante = decimal.Parse(valor) / 100;
                    cartaoRespostaView.ValorRestante = valorRestante;
                    continue;
                }

                if (herader == "018-000")
                {
                    var valor = line.ToRegistroValor();

                    quantidadeParcelas = int.Parse(valor);
                    if (quantidadeParcelas >= 1)
                    {
                        cartaoRespostaParcelaView = new CartaoRespostaParcelaViewModel();
                    }
                    continue;
                }
                if (quantidadeParcelas >= 1)
                {
                    var partHeader = parcelas.ToString("000");

                    if (herader == $"019-{partHeader}")
                    {
                        var valor = line.ToRegistroValor();
                        var dia = int.Parse(valor.Substring(0, 2));
                        var mes = int.Parse(valor.Substring(2, 2));
                        var ano = int.Parse(valor.Substring(4, 4));

                        var dataVencimento = new DateTime(ano, mes, dia);
                        cartaoRespostaParcelaView.DataVencimento = dataVencimento;
                        cartaoRespostaParcelaView.NumeroParcela = partHeader;
                        continue;
                    }
                    if (herader == $"020-{partHeader}")
                    {
                        var valor = line.ToRegistroValor();
                        var valorRestante = decimal.Parse(valor) / 100;
                        cartaoRespostaParcelaView.ValorParcela = valorRestante;
                        continue;
                    }
                    if (herader == $"021-{partHeader}")
                    {
                        var valor = line.ToRegistroValor();
                        cartaoRespostaParcelaView.NsuParcela = valor;

                        cartaoRespostaView.CartaoRespostaoParcelas.Add(cartaoRespostaParcelaView);
                        parcelas++;

                        continue;
                    }
                }

                #region Comprovante

                if (herader == "028-000")
                {
                    var valor = line.ToRegistroValor();

                    linhaComprovante = int.Parse(valor);
                    //Via Cliente
                    cartaoRespostaView.Comprovantes.Add(new ComprovanteTef());

                    //Via Estabelecimento
                    cartaoRespostaView.Comprovantes.Add(new ComprovanteTef());
                }
                if (herader.Substring(0, 3) == "029")
                {
                    var linha = int.Parse(herader.Substring(4, 3));

                    var valor = line.ToRegistroValor();
                    if (string.IsNullOrEmpty(valor)) continue;

                    if (linha <= linhaComprovante)
                    {
                        cartaoRespostaView.Comprovantes[0].Comprovante += $"{valor.Replace("\"", "")}{Environment.NewLine}";
                    }
                    else
                    {
                        cartaoRespostaView.Comprovantes[1].Comprovante += $"{valor.Replace("\"", "")}{Environment.NewLine}";
                    }

                    cartaoRespostaView.Comprovante += $"{valor.Replace("\"", "")}{Environment.NewLine}";

                }

                #endregion
            }
            file.Close();

            return cartaoRespostaView;
        }
    }
}