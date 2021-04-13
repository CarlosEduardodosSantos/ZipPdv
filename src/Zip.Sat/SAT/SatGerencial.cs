using Eticket.Application.ViewModels;
using SAT.Interface;
using SocketAppServerClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Zip.Sat;
using Zip.Sat.Libraries.Model;
using Zip.Utils;

namespace SAT
{
    public enum SatOperacaoEnum { None, Enviar, Cancelar }
    
    public class SatGerencial
    {
        
        ISat sat = SatFactory.CreateSat(Global.ConfiguracaoInicial.SatMarca);

        const int TAMANHO_BUFFER = 100000;
        Random rdn = new Random();
        string mMessageClient = string.Empty;
        string mMsg = "";

        public string NomeImpressora { get; set; }
        public string ArquivoXML { get; set; }
        public string ArquivoPath { get; set; }
        public bool SatConsultarStatus(ref string _msg)
        {
            _msg = "";
            try
            {
                int totalTentativas = 10;
                int tentativa = 1;
                string[] vetor = null;

                int secao = rdn.Next(999999);
                while (totalTentativas != tentativa)
                {
                    string retorno = sat.IConsultarStatusOperacional(secao);
                    if (!string.IsNullOrEmpty(retorno))
                    {
                        retorno = retorno.Replace("$#$#", "").Replace("\0", "");
                        vetor = retorno.Split('|');
                    }
                    if (vetor != null && vetor.Length > 2 && vetor[2].ToLower().Contains("resposta com sucesso"))
                    {
                        tentativa = 1;
                        break;
                    }
                    else
                        tentativa++;
                }
                if (tentativa == 10)
                {
                    _msg = "Não foi possível estabelecer conexão com o SAT.\nNúmero de tentativas excedido.";
                    return false;
                }

                int vetorSat = Global.ConfiguracaoInicial.SatMarca == "Emulador" ? 26 : 27;

                if (vetor.Length > vetorSat)
                {
                    bool sucesso = false;
                    if (vetor[2].ToLower().Contains("resposta com sucesso"))
                    {
                        switch (vetor[vetorSat])
                        {
                            case "0":
                                _msg = "SAT EM OPERAÇÃO (DESBLOQUEADO).";
                                sucesso = true;
                                break;
                            case "1":
                                _msg = "SAT BLOQUEIO SEFAZ";
                                break;
                            case "2":
                                _msg = "SAT BLOQUEIO CONTRIBUINTE";
                                break;
                            case "3":
                                _msg = "SAT BLOQUEIO AUTÔNOMO";
                                break;
                            case "4":
                                _msg = "SAT BLOQUEIO PARA DESATIVAÇÃO";
                                break;
                            default:
                                _msg = "SAT STATUS DESCONHECIDO";
                                break;
                        }
                    }
                    return sucesso;
                }
                else
                {
                    _msg = "Erro não definido";
                    return false;
                }
            }
            catch (Exception err)
            {
                var error = "Erro ao tentar consultar SAT.\n" + err.Message;
                new LogWriter(error);
                Global.Funcoes.MensagemDeErro(error);
            }
            return false;
        }
        public RetornoSatViewModel SatEnviarImprimir(SatOperacaoEnum _satOperacao, VendaSatModel _venda)
        {
            var retornoModel = new RetornoSatViewModel();
            StringBuilder sb = new StringBuilder();

            try
            {
                retornoModel.VendaId = Global.Funcoes.ConvertToInt32(_venda.VendaID);
                retornoModel.EmpresaId = _venda.EmpresaId;
                retornoModel.Pdv = _venda.Pdv;

                if (_satOperacao == SatOperacaoEnum.Enviar)
                {
                    sb.Append("<CFe>");
                    sb.Append("<infCFe versaoDadosEnt=\"" + Global.ConfiguracaoInicial.SatLayoutVersao + "\">");

                    sb.Append("<ide>");
                    sb.Append("<CNPJ>" + Global.ConfiguracaoInicial.SoftwareHouseCnpj.Trim() + "</CNPJ>");
                    sb.Append("<signAC>" + Global.Empresa.SignAC.Trim() + "</signAC>");
                    sb.Append("<numeroCaixa>" + _venda.Pdv.ToString("000") + "</numeroCaixa>");
                    sb.Append("</ide>");

                    sb.Append("<emit>");
                    sb.Append("<CNPJ>" + Global.Empresa.Cnpj + "</CNPJ>");
                    sb.Append("<IE>" + Global.Empresa.Ie + "</IE>");
                    sb.Append("<indRatISSQN>N</indRatISSQN>");
                    sb.Append("</emit>");

                    if (!string.IsNullOrEmpty(_venda.ClienteCpf.Trim()))
                    {
                        sb.Append("<dest>");
                        if (_venda.ClienteCpf.Trim().Length <= 11)
                            sb.Append("<CPF>" + _venda.ClienteCpf.Trim() + "</CPF>");
                        else
                            sb.Append("<CNPJ>" + _venda.ClienteCpf.Trim() + "</CNPJ>");
                        sb.Append("</dest>");
                    }
                    else
                        sb.Append("<dest/>");

                    int item = 1;
                    decimal impostoIbptFederalItem = 0M;
                    decimal impostoIbptEstadualItem = 0M;
                    decimal impostoIbptFederalTotal = 0M;
                    decimal impostoIbptEstadualTotal = 0M;
                    decimal totalItem = 0M;
                    decimal descontoItem = 0M;
                    decimal qtde = 0M;
                    decimal valorUnitario = 0M;
                    decimal valorComDesconto = 0M;

                    decimal totalVendaBruta = _venda.ValorTotal;
                    decimal totalVendaLiquida = _venda.ValorFinal;
                    decimal totalDesconto = totalVendaBruta - totalVendaLiquida;

                    foreach (VendaItemSatModel r in _venda.Itens)
                    {
                        if (r.Ncm.Length < 8)
                            throw new Exception("NCM inválido!");

                        qtde = r.Qtde;
                        valorUnitario = r.Valor;
                        descontoItem = 0;
                        valorComDesconto = r.Valor; // -Math.Round(descontoItem / qtde, 2);



                        sb.Append("<det nItem=\"" + item.ToString() + "\">");
                        sb.Append("<prod>");
                        sb.Append("<cProd>" + r.ProdutoID.ToString() + "</cProd>");
                        //sb.Append("<cEAN>" + r.Cells["ean"].Value.ToString() + "</cEAN>");
                        sb.Append("<xProd>" + r.ProdutoDescricao.Trim() + "</xProd>");
                        sb.Append("<NCM>" + r.Ncm + "</NCM>");
                        sb.Append("<CFOP>" + r.Cfop + "</CFOP>");
                        sb.Append("<uCom>" + r.ProdutoUnidade + "</uCom>");
                        sb.Append("<qCom>" + qtde.ToString("N4").Replace(".", "#").Replace(",", ".").Replace("#", "") + "</qCom>");
                        sb.Append("<vUnCom>" + valorComDesconto.ToString("N2").Replace(".", "#").Replace(",", ".").Replace("#", "") + "</vUnCom>");
                        sb.Append("<indRegra>A</indRegra>");

                        if (r.IcmsCstCsosn == "60" || r.IcmsCstCsosn == "500")
                        {
                            if (string.IsNullOrEmpty(r.CestCodigo))
                                r.CestCodigo = "0000000";

                            sb.Append("<obsFiscoDet xCampoDet=\"Cod. CEST\">");
                            sb.Append("<xTextoDet>" + r.CestCodigo + "</xTextoDet>");
                            sb.Append("</obsFiscoDet>");
                        }

                        sb.Append("</prod>");
                        totalItem += Math.Round(qtde * valorComDesconto, 2);

                        impostoIbptFederalItem = Global.Funcoes.ConvertToDecimal(r.NcmImpostoFederal);
                        impostoIbptEstadualItem = Global.Funcoes.ConvertToDecimal(r.NcmImpostoEstadual);
                        impostoIbptFederalTotal += impostoIbptFederalItem;
                        impostoIbptEstadualTotal += impostoIbptEstadualItem;


                        sb.Append("<imposto>");
                        sb.Append("<vItem12741>" + (impostoIbptFederalItem + impostoIbptEstadualItem).ToString("N2").Replace(".", "#").Replace(",", ".").Replace("#", "") + "</vItem12741>");


                        sb.Append("<ICMS>");
                        if (Global.Empresa.EmpresaConfiguracao.CodigoRegimeTributario == 3)
                        {

                            if (r.IcmsCstCsosn == "00" || r.IcmsCstCsosn == "20" || r.IcmsCstCsosn == "90")
                            {
                                sb.Append("<ICMS00>");
                                sb.Append("<Orig>" + r.ProdutoOrigem + "</Orig>");
                                sb.Append("<CST>00</CST>");
                                sb.Append("<pICMS>" + r.IcmsAliquota + "</pICMS>");
                                sb.Append("</ICMS00>");
                            }
                            else
                            {
                                sb.Append("<ICMS40>");
                                sb.Append("<Orig>" + r.ProdutoOrigem + "</Orig>");
                                sb.Append("<CST>60</CST>");
                                sb.Append("</ICMS40>");
                            }


                        }
                        else
                        {
                            sb.Append("<ICMSSN102>");
                            sb.Append("<Orig>" + r.ProdutoOrigem + "</Orig>");
                            sb.Append("<CSOSN>" + r.IcmsCstCsosn + "</CSOSN>");
                            sb.Append("</ICMSSN102>");
                        }
                        sb.Append("</ICMS>");

                        sb.Append("<PIS>");
                        if (Global.Empresa.EmpresaConfiguracao.CodigoRegimeTributario == 3)
                        {
                            if (r.PisCofinsCst == "01" || r.PisCofinsCst == "02" || r.PisCofinsCst == "05")
                            {
                                sb.Append("<PISAliq>");
                                sb.Append("<CST>" + r.PisCofinsCst + "</CST>");
                                sb.Append("<vBC>" + valorComDesconto.ToString("N2").Replace(".", "#").Replace(",", ".").Replace("#", "") + "</vBC>");
                                sb.Append("<pPIS>" + r.PisAliquota.ToString("N4").Replace(".", "#").Replace(",", ".").Replace("#", "") + "</pPIS>");
                                sb.Append("</PISAliq>");
                            }
                            else if (r.PisCofinsCst == "04" || r.PisCofinsCst == "06" || r.PisCofinsCst == "07" ||
                                     r.PisCofinsCst == "08" || r.PisCofinsCst == "09")
                            {
                                sb.Append("<PISNT>");
                                sb.Append("<CST>" + r.PisCofinsCst + "</CST>");
                                sb.Append("</PISNT>");
                            }
                            else if (r.PisCofinsCst == "99")
                            {
                                sb.Append("<PISOutr>");
                                sb.Append("<CST>" + r.PisCofinsCst + "</CST>");
                                sb.Append("<vBC>" + valorComDesconto.ToString("N2").Replace(".", "#").Replace(",", ".")
                                              .Replace("#", "") + "</vBC>");
                                sb.Append("<pPIS>" + r.PisAliquota.ToString("N4").Replace(".", "#").Replace(",", ".").Replace("#", "") + "</pPIS>");
                                sb.Append("</PISOutr>");
                            }
                            else
                            {
                                sb.Append("<PISSN>");
                                sb.Append("<CST>" + r.PisCofinsCst + "</CST>");
                                sb.Append("</PISSN>");
                            }
                        }
                        else
                        {
                            sb.Append("<PISSN>");
                            sb.Append("<CST>" + r.PisCofinsCst + "</CST>");
                            sb.Append("</PISSN>");
                        }
                        sb.Append("</PIS>");


                        sb.Append("<COFINS>");
                        if (Global.Empresa.EmpresaConfiguracao.CodigoRegimeTributario == 3)
                        {
                            if (r.PisCofinsCst == "01" || r.PisCofinsCst == "02" || r.PisCofinsCst == "05")
                            {
                                sb.Append("<COFINSAliq>");
                                sb.Append("<CST>" + r.PisCofinsCst + "</CST>");
                                sb.Append("<vBC>" + valorComDesconto.ToString("N2").Replace(".", "#").Replace(",", ".").Replace("#", "") + "</vBC>");
                                sb.Append("<pCOFINS>" + r.CofinsAliquota.ToString("N4").Replace(".", "#").Replace(",", ".").Replace("#", "") + "</pCOFINS>");
                                sb.Append("</COFINSAliq>");
                            }
                            else if (r.PisCofinsCst == "04" || r.PisCofinsCst == "06" || r.PisCofinsCst == "07" ||
                                     r.PisCofinsCst == "08" || r.PisCofinsCst == "09")
                            {
                                sb.Append("<COFINSNT>");
                                sb.Append("<CST>" + r.PisCofinsCst + "</CST>");
                                sb.Append("</COFINSNT>");
                            }
                            else if (r.PisCofinsCst == "99")
                            {
                                sb.Append("<COFINSOutr>");
                                sb.Append("<CST>" + r.PisCofinsCst + "</CST>");
                                sb.Append("<vBC>" + valorComDesconto.ToString("N2").Replace(".", "#").Replace(",", ".")
                                              .Replace("#", "") + "</vBC>");
                                sb.Append("<pCOFINS>" + r.CofinsAliquota.ToString("N4").Replace(".", "#").Replace(",", ".").Replace("#", "") + "</pCOFINS>");
                                sb.Append("</COFINSOutr>");
                            }
                            else
                            {
                                sb.Append("<COFINSSN>");
                                sb.Append("<CST>" + r.PisCofinsCst + "</CST>");
                                sb.Append("</COFINSSN>");
                            }
                        }
                        else
                        {

                            sb.Append("<COFINSSN>");
                            sb.Append("<CST>" + r.PisCofinsCst + "</CST>");
                            sb.Append("</COFINSSN>");
                        }
                        sb.Append("</COFINS>");

                        sb.Append("</imposto>");
                        //}
                        sb.Append("</det>");
                        impostoIbptFederalItem = 0M;
                        impostoIbptEstadualItem = 0M;
                        item++;
                    }

                    sb.Append("<total>");
                    if (totalDesconto > 0M)
                    {
                        sb.Append("<DescAcrEntr>");
                        sb.Append("<vDescSubtot>" + totalDesconto.ToString("N2").Replace(".", "#").Replace(",", ".").Replace("#", "") + "</vDescSubtot>");
                        sb.Append("</DescAcrEntr>");
                    }
                    sb.Append("<vCFeLei12741>" + (impostoIbptFederalTotal + impostoIbptEstadualTotal).ToString("N2").Replace(".", "#").Replace(",", ".").Replace("#", "") + "</vCFeLei12741>");
                    sb.Append("</total>");

                    //01 - Dinheiro
                    //02 - Cheque
                    //03 - Cartão de Crédito
                    //04 - Cartão de Débito
                    //05 - Crédito Loja
                    //10 - Vale Alimentação
                    //11 - Vale Refeição
                    //12 - Vale Presente
                    //13 - Vale Combustível
                    //99 - Outros 
                    sb.Append("<pgto>");
                    if (_venda.Finalizadoras.Count > 0)
                    {
                        foreach (var finalizadora in _venda.Finalizadoras)
                        {
                            sb.Append("<MP>");
                            sb.Append("<cMP>" + finalizadora.CodigoFinalizadora + "</cMP>");
                            sb.Append("<vMP>" + finalizadora.Valor.ToString("N2").Replace(".", "#").Replace(",", ".").Replace("#", "") + "</vMP>");
                            sb.Append("</MP>");
                        }
                    }
                    else
                    {
                        sb.Append("<MP>");
                        sb.Append("<cMP>99</cMP>");
                        sb.Append("<vMP>" + _venda.ValorTotal.ToString("N2").Replace(".", "#").Replace(",", ".").Replace("#", "") + "</vMP>");
                        sb.Append("</MP>");
                    }
                    sb.Append("</pgto>");

                    sb.Append("<infAdic>");
                    mMsg = "CLIENTE: " + _venda.ClienteNome.Trim();
                    mMsg += "|Venda: " + _venda.VendaID.ToString();
                    mMsg += "|ICMS a ser recolhido conforme LC 123/2006";
                    mMsg += "|Simples Nacional";
                    mMsg += "|Trib aprox R$" + impostoIbptFederalTotal.ToString("N2").Replace(".", "#").Replace(",", ".").Replace("#", "") + " Federal e R$" + impostoIbptEstadualTotal.ToString("N2").Replace(".", "#").Replace(",", ".").Replace("#", "") + " Estadual|Fonte IBPT ca7gi3";
                    if (Global.ConfiguracaoInicial.SatMarca == "Gertec")
                        mMsg = mMsg.Replace("|", ";");
                    if (Global.ConfiguracaoInicial.SatMarca == "Emulador")
                        sb.Append("<infCpl>Trib aprox R$" + impostoIbptFederalTotal.ToString("N2").Replace(".", "#").Replace(",", ".").Replace("#", "") + " Federal e R$" + impostoIbptEstadualTotal.ToString("N2").Replace(".", "#").Replace(",", ".").Replace("#", "") + " Estadual - Fonte IBPT ca7gi3</infCpl>");
                    else
                        sb.Append("<infCpl>" + mMsg + "</infCpl>");
                    sb.Append("</infAdic>");
                    sb.Append("</infCFe>");
                    sb.Append("</CFe>");
                }
                else if (_satOperacao == SatOperacaoEnum.Cancelar)
                {
                    sb.Append("CANCELAMENTO|" + _venda.ChaveEletronicaCFeSATNFce.ToUpper().Replace("CFE", "") + "|" + (Global.ConfiguracaoInicial.SatMarca == "Emulador" ? "00000000000000" : Global.ConfiguracaoInicial.SoftwareHouseCnpj) + "|" + _venda.ClienteCpf.Trim() + "|" + Global.ConfiguracaoInicial.CaixaNumero.ToString("000") + Global.Empresa.SignAC);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro na função SatEnviarImprimir Gerando XML: " + e.Message);
            }


            string conteudo = sb.ToString().Trim();
            _venda.DataCompleta = DateTime.Now;
            _venda.Modelo = "59";
            string retorno = "";
            
            retornoModel.Secao = rdn.Next(999999); ;
            retornoModel.DataHora = DateTime.Now;
            retornoModel.Funcao = _satOperacao == SatOperacaoEnum.Enviar ? "VIP_EnviarDadosVenda" : "VIP_CancelarUltimaVenda";

            if (_satOperacao == SatOperacaoEnum.Enviar)
            {
                try
                {
                    var xVezes = 0;
                    var isOk = false;

                    while (!isOk)
                    {
                        retorno = string.IsNullOrEmpty(Global.ConfiguracaoInicial.SatServidor)
                            ? sat.IEnviarDadosVenda(retornoModel.Secao, conteudo)
                            : SatEnviarViaSocket(conteudo);

                        if (string.IsNullOrEmpty(retorno))
                        {
                            if (xVezes >= 3)
                            {
                                throw new Exception("Erro na função SatEnviarImprimir Retorno SAT: Erro no retorno do SAT.");
                            }
                            Thread.Sleep(3000);
                            xVezes++;
                        }
                        else
                        {
                            isOk = true;
                        }

                    }




                }
                catch (Exception e)
                {
                    var error = "Erro na função EnviarDadosVenda Retorno SAT: " + e.Message;
                    new LogWriter(error);
                    throw new Exception(error);
                }

            }
            else
            {
                try
                {
                    retorno = string.IsNullOrEmpty(Global.ConfiguracaoInicial.SatServidor)
                        ? sat.ICancelarUltimaVenda(retornoModel.Secao, _venda.ChaveEletronicaCFeSATNFce.ToUpper().Replace("CFE", ""), (Global.ConfiguracaoInicial.SatMarca == "Emulador" ? "00000000000000" : Global.ConfiguracaoInicial.SoftwareHouseCnpj), _venda.ClienteCpf.Trim(), Global.ConfiguracaoInicial.CaixaNumero.ToString("000"), Global.Empresa.SignAC)
                        : SatCancelarViaSocket(_venda.ChaveEletronicaCFeSATNFce.ToUpper().Replace("CFE", ""), (Global.ConfiguracaoInicial.SatMarca == "Emulador" ? "00000000000000" : Global.ConfiguracaoInicial.SoftwareHouseCnpj), _venda.ClienteCpf.Trim(), Global.ConfiguracaoInicial.CaixaNumero.ToString("000"), Global.Empresa.SignAC);

                    if (string.IsNullOrEmpty(retorno))
                        Global.Funcoes.MensagemDeInformacao("Erro no retorno do SAT.");
                }
                catch (Exception e)
                {
                    //Global.Funcoes.MensagemDeInformacao(e.Message);
                }
            }
            //retorno = sat.ICancelarUltimaVenda(retornoModel.Secao, _venda.ChaveEletronicaCFeSATNFce.ToUpper().Replace("CFE", ""), (Global.ConfiguracaoInicial.SatMarca == "Emulador" ? "00000000000000" : Global.ConfiguracaoInicial.SoftwareHouseCnpj), _venda.ClienteCpf.Trim(), Global.ConfiguracaoInicial.CaixaNumero.ToString("000"), Global.Empresa.SignAC);
            try
            {
                if (!string.IsNullOrEmpty(retorno))
                {
                    string[] vetorRetorno = retorno.Split('|');

                    string path = Global.ConfiguracaoInicial.SalvarArquivosEm + @"\CfeSAT";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    if (!Directory.Exists(path))
                    {
                        path = Application.StartupPath + @"\CfeSAT";
                        Directory.CreateDirectory(path);
                    }

                    if (vetorRetorno != null && vetorRetorno.Length < 3)
                    {
                        retornoModel.Mensagem = (vetorRetorno != null && vetorRetorno[0].Length > 3 ? vetorRetorno[0] : "");
                        new LogWriter(retorno);

                        return retornoModel;
                    }

                    if (vetorRetorno != null && vetorRetorno[3].Length > 3 &&
                        (vetorRetorno[3].ToLower().Contains("emitido com sucesso") ||
                         vetorRetorno[3].ToLower().Contains("cancelado com sucesso")))
                    {
                        retornoModel.Mensagem = (vetorRetorno != null && vetorRetorno[3].Length > 3 ? vetorRetorno[3] : "");


                        string xmlRetorno = SatDescriptogragia(vetorRetorno[6]);
                        XmlDocument xmlSat = new XmlDocument();
                        xmlSat.LoadXml(xmlRetorno);
                        XmlNodeList elemList = xmlSat.GetElementsByTagName("infCFe");
                        XmlNodeList elemListIde = xmlSat.GetElementsByTagName("ide");
                        _venda.ChaveEletronicaCFeSATNFce = "";
                        if (elemList != null && elemList.Count > 0)
                        {
                            retornoModel.ChaveEletronicaCFeSATNFce = elemList[0].Attributes["Id"].Value;
                            retornoModel.CfeSatNumeroNf = Global.Funcoes.ConvertToInt32(elemListIde[0]["cNF"].InnerText);
                            retornoModel.CfeSatNumeroExtrato = elemListIde[0]["nCFe"].InnerText;
                            retornoModel.NumeroSerie = elemListIde[0]["nserieSAT"].InnerText;
                            retornoModel.Eeeee = vetorRetorno[1];
                            retornoModel.Cccc = vetorRetorno[2];
                            retornoModel.Timestampcfe = vetorRetorno[7];
                            retornoModel.ValorCfe = decimal.Parse(vetorRetorno[9].Replace(".", ","));


                            string nomeArquivo = (_satOperacao == SatOperacaoEnum.Enviar ? "AD" : "ADC") +
                                                 retornoModel.ChaveEletronicaCFeSATNFce.Replace("CFe", "") + ".xml";
                            if (SatXmlSalvar(xmlRetorno, nomeArquivo, ref path, _venda))
                            {
                                retornoModel.XmlSatAssinado = xmlRetorno;
                                ArquivoPath = path;
                                ArquivoXML = path + @"\" + nomeArquivo;
                                retornoModel.IsOk = true;

                                NomeImpressora = Global.ConfiguracaoInicial.SatImpressora;
                                try
                                {
                                    if (string.IsNullOrEmpty(NomeImpressora))
                                    {
                                        using (PrintDialog pd = new PrintDialog())
                                        {
                                            if (pd.ShowDialog() == DialogResult.OK)
                                                NomeImpressora = pd.PrinterSettings.PrinterName;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(NomeImpressora))
                                    {
                                        if (_satOperacao == SatOperacaoEnum.Enviar)
                                            ImprimirSat();
                                        else if (_satOperacao == SatOperacaoEnum.Cancelar)
                                            ImprimirSat(SatOperacaoEnum.Cancelar);

                                        retornoModel.IsOk = true;
                                        return retornoModel;
                                    }
                                }
                                catch (Exception e)
                                {
                                    return retornoModel;
                                }
  
                            }
                        }
                    }
                    else
                    {
                        var fileNameError = $"ERRO_{retornoModel.Secao}.xml";
                        SatXmlSalvar(conteudo, fileNameError, ref path, _venda);

                        retornoModel.Mensagem = (vetorRetorno != null && vetorRetorno[3].Length > 3 ? vetorRetorno[3] : "");
                        new LogWriter(retornoModel.Mensagem);

                        //Global.Funcoes.MensagemDeInformacao("Não foi possivel enviar o CFe-SAT.\n\n" + (vetorRetorno != null && vetorRetorno[3].Length > 3 ? vetorRetorno[3] : ""));
                    }

                }
                else
                    retornoModel.Mensagem = "Não foi possivel enviar o CFe-SAT.\n\nOperação não definida";
            }
            catch (Exception e)
            {
                var error = "Erro na função SatEnviarImprimir: " + e.Message;
                new LogWriter(error);
                throw new Exception(error);

            }

            return retornoModel;
        }

        public string SatEnviarViaSocket(string conteudoXML)
        {
            try
            {

                if (string.IsNullOrEmpty(conteudoXML))
                    return "Arquivo em formato inválido.";

                string servidor = Global.ConfiguracaoInicial.SatServidor;
                int porta = Global.ConfiguracaoInicial.PortaServidor;

                Client.Configure(servidor, porta, Encoding.UTF8, 3);
                
                Client client = new Client();
                //client.ByteBuffer = 38000;

                SocketConnectionFactory.SetDefaultSettings(new SocketClientSettings(
                        "localhost", 7001,
                        Encoding.UTF8, 3
                    ));

                RequestBody rb = RequestBody.Create("NotaFiscalController", "EmiteNF")
                    .AddParameter("parametros.Parametro", "vendaJson")
                    .AddParameter("parametros.Value", conteudoXML)
                    .AddParameter("tokenAutorizacao", "token_exemplo");
                client.SendRequest(rb);


                
                var result = client.GetResult(typeof(VendaSatModel));
                
                return (string)result.Entity;

             
            }
            catch (Exception err)
            {
                var error = $"Erro ao tentar enviar CFe-SAT via Concentrador.\nSatEnviarViaSocket {err.Message}";
                new LogWriter(error);
                new LogWriter(conteudoXML);
                return string.Empty;
            }
        }

        public string SatCancelarViaSocket(string chave, string cnpjSoftwareHouse, string cnpjCpfDestinatario, string NumeroCaixa, string SignAC)
        {
            try
            {


                string servidor = Global.ConfiguracaoInicial.SatServidor;
                int porta = Global.ConfiguracaoInicial.PortaServidor;

                Client.Configure(servidor, porta, Encoding.UTF8, 3);

                Client client = new Client();
                //client.ByteBuffer = 38000;

                
                RequestBody rb = RequestBody.Create("NotaFiscalController", "CancelarSat")
                    .AddParameter("chave", chave)
                    .AddParameter("cnpjSoftwareHouse", cnpjSoftwareHouse)
                    .AddParameter("cnpjCpfDestinatario", cnpjCpfDestinatario)
                    .AddParameter("NumeroCaixa", NumeroCaixa)
                    .AddParameter("SignAC", SignAC)
                    .AddParameter("tokenAutorizacao", "token_exemplo");
                client.SendRequest(rb);



                var result = client.GetResult(typeof(VendaSatModel));

                return (string)result.Entity;


            }
            catch (Exception err)
            {
                var error = "Erro ao tentar enviar CFe-SAT.\n" + err.Message;
                new LogWriter(error);
                throw new Exception(error);
            }
        }
        public bool SatXmlSalvar(string _conteudoXML, string _nomeArquivo, ref string _path, VendaSatModel _venda = null)
        {
            try
            {
                if (_venda != null)
                {
                    string ano = _venda.DataCompleta.Year.ToString("0000");
                    string mes = _venda.DataCompleta.Month.ToString("00");

                    if (!Directory.Exists(_path + @"\" + ano))
                        Directory.CreateDirectory(_path + @"\" + ano);
                    if (Directory.Exists(_path + @"\" + ano))
                        _path += @"\" + ano;

                    if (!Directory.Exists(_path + @"\" + mes))
                        Directory.CreateDirectory(_path + @"\" + mes);
                    if (Directory.Exists(_path + @"\" + mes))
                        _path += @"\" + mes;
                }
                using (StreamWriter outfile = new StreamWriter(_path + @"\" + _nomeArquivo, true))
                {
                    outfile.Write(_conteudoXML.ToString());
                    outfile.Flush();
                }
                return true;
            }
            catch (Exception err)
            {
                var error = "Erro ao tentar salvar Xml CFe-SAT.\n" + err.Message;
                new LogWriter(error);
                throw new Exception(error);
            }
        }
        public void ImprimirSat(SatOperacaoEnum _operacao = SatOperacaoEnum.Enviar)
        {
            try
            {
                using (var printDc = new PrintDocument())
                {
                    if (_operacao == SatOperacaoEnum.Enviar)
                    {
                        printDc.PrintPage += new PrintPageEventHandler(this.PrintEmissaoCfeSAT);
                        printDc.PrintController = new System.Drawing.Printing.StandardPrintController();
                        printDc.PrinterSettings.PrinterName = NomeImpressora;

                        if (printDc.PrinterSettings.IsValid)
                            printDc.Print();
                    }
                    else if (_operacao == SatOperacaoEnum.Cancelar)
                    {
                        var newSettings = new System.Drawing.Printing.PrinterSettings();

                        printDc.PrintPage += new PrintPageEventHandler(this.PrintCancelamento);
                        printDc.PrintController = new System.Drawing.Printing.StandardPrintController();
                        printDc.PrinterSettings.PrinterName = newSettings.PrinterName;
                        printDc.Print();

                    }
                }

            }
            catch (Exception err)
            {
                var error = "Erro ao tentar Imprimir o CFe-SAT.\n" + err.Message;
                new LogWriter(error);
                throw new Exception(error);
            }

        }
        private void PrintCancelamento(object sender, PrintPageEventArgs ppeArgs)
        {
            #region Variaveis Locais

            float Font_Tamanho1 = 7;
            Font Font1 = new Font("courier new", Font_Tamanho1);

            float Font_Tamanho1_negrito = 7;
            Font Font1_negrito = new Font("courier new", Font_Tamanho1_negrito, FontStyle.Bold);

            float Font_Tamanho11_negrito = 7;
            Font Font11_negrito = new Font("arial", Font_Tamanho11_negrito, FontStyle.Bold);

            float Font_Tamanho2_negrito = 10;
            Font Font2_negrito = new Font("arial", Font_Tamanho2_negrito, FontStyle.Bold);

            int _posicaoBlocoFinalY = 0;
            int _posicaoColunaInicial = 0;

            string strTmp = "";

            string _separador = "____________________________________________________________";
            string _strChaveAcesso = "".PadLeft(44, ' ');
            string _strChaveAcessoEnvio = "".PadLeft(44, ' ');
            string _strChaveAcessoLeituraHumana = "";
            string _strChaveAcessoLeituraHumanaEnvio = "";
            string _AssinaturaQRcode = "";
            string _AssinaturaQRcodeEnvio = "";
            string _DataHora = "";
            string _DataHoraEnvio = "";
            string _NumeroSerieSAT = "";
            string _ValorTotalCFe = "0,00";
            string _NFP = "";
            string _NumeroExtrato = "";
            string _RazaoSocial = "";
            string _CNPJ = "";
            string _IE = "";
            string _Endereco = "";
            string _Cidade = "";
            string _Nro = "";
            string _Complemento = "";
            string _Bairro = "";
            string _CEP = "";

            #endregion

            #region Dataset Xml

            DataSet dsCancelamento = new DataSet();
            DataSet dsEnvio = new DataSet();
            Graphics g = ppeArgs.Graphics;

            dsCancelamento.ReadXml(this.ArquivoXML);
            DataRow[] rowIde = dsCancelamento.Tables["ide"] != null ? dsCancelamento.Tables["ide"].Select() : null;
            DataRow[] rowEmit = dsCancelamento.Tables["emit"] != null ? dsCancelamento.Tables["emit"].Select() : null;
            DataRow[] rowEnderEmit = dsCancelamento.Tables["EnderEmit"] != null ? dsCancelamento.Tables["EnderEmit"].Select() : null;
            DataRow[] rowTotal = dsCancelamento.Tables["total"] != null ? dsCancelamento.Tables["total"].Select() : null;
            DataRow[] rowDest = dsCancelamento.Tables["dest"] != null ? dsCancelamento.Tables["dest"].Select() : null;
            DataRow[] rowinfCFe = dsCancelamento.Tables["infcfe"] != null ? dsCancelamento.Tables["infcfe"].Select() : null;

            DataRow[] rowIdeEnvio = null;
            DataRow[] rowinfCFeEnvio = null;
            if (rowinfCFe != null && rowinfCFe.Length > 0)
            {
                dsEnvio.ReadXml(ArquivoPath + @"\AD" + rowinfCFe[0]["chCanc"].ToString().Replace("CFe", "") + ".xml");
                rowIdeEnvio = dsEnvio.Tables["ide"] != null ? dsEnvio.Tables["ide"].Select() : null;
                rowinfCFeEnvio = dsCancelamento.Tables["infcfe"] != null ? dsEnvio.Tables["infcfe"].Select() : null;
            }

            #endregion

            #region Numero Extrato
            try
            {
                if (rowIde.Length > 0)
                    _NumeroExtrato = rowIde[0]["nCFe"].ToString();
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }
            #endregion

            #region Numero Serie Sat
            try
            {
                if (rowIde.Length > 0)
                    _NumeroSerieSAT = rowIde[0]["nserieSAT"].ToString().Insert(3, ".").Insert(7, ".");//"900.001.246";
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }
            #endregion

            #region Data e Hora
            try
            {
                if (rowIde.Length > 0)
                    _DataHora = rowIde[0]["DEmi"].ToString() + rowIde[0]["hEmi"].ToString();

                if (rowIdeEnvio.Length > 0)
                    _DataHoraEnvio = rowIdeEnvio[0]["DEmi"].ToString() + rowIdeEnvio[0]["hEmi"].ToString();
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }
            #endregion

            #region Assinatura QRCODE

            try
            {
                if (rowIde.Length > 0)
                    _AssinaturaQRcode = rowIde[0]["assinaturaQRCODE"].ToString();

                if (rowIdeEnvio.Length > 0)
                    _AssinaturaQRcodeEnvio = rowIdeEnvio[0]["assinaturaQRCODE"].ToString();
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }

            #endregion

            #region Chave CFe
            try
            {
                if (rowinfCFe.Length > 0)
                {
                    _strChaveAcesso = rowinfCFe[0]["Id"].ToString().Replace("CFe", "");
                    _strChaveAcessoLeituraHumana = _strChaveAcesso.Substring(0, 4) + " " + _strChaveAcesso.Substring(4, 4) + " " + _strChaveAcesso.Substring(8, 4) + " " + _strChaveAcesso.Substring(12, 4) + " " + _strChaveAcesso.Substring(16, 4) + " " + _strChaveAcesso.Substring(20, 4) + " " + _strChaveAcesso.Substring(24, 4) + " " + _strChaveAcesso.Substring(28, 4) + " " + _strChaveAcesso.Substring(32, 4) + " " + _strChaveAcesso.Substring(36, 4) + " " + _strChaveAcesso.Substring(40, 4);

                    _strChaveAcessoEnvio = rowinfCFeEnvio[0]["Id"].ToString().Replace("CFe", "");
                    _strChaveAcessoLeituraHumanaEnvio = _strChaveAcessoEnvio.Substring(0, 4) + " " + _strChaveAcessoEnvio.Substring(4, 4) + " " + _strChaveAcessoEnvio.Substring(8, 4) + " " + _strChaveAcessoEnvio.Substring(12, 4) + " " + _strChaveAcessoEnvio.Substring(16, 4) + " " + _strChaveAcessoEnvio.Substring(20, 4) + " " + _strChaveAcessoEnvio.Substring(24, 4) + " " + _strChaveAcessoEnvio.Substring(28, 4) + " " + _strChaveAcessoEnvio.Substring(32, 4) + " " + _strChaveAcessoEnvio.Substring(36, 4) + " " + _strChaveAcessoEnvio.Substring(40, 4);
                }
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }


            #endregion

            #region Dados Razao Social Empresa
            try
            {
                if (rowEmit.Length > 0)
                {
                    _RazaoSocial = rowEmit[0]["xNome"].ToString();
                    _CNPJ = rowEmit[0]["cnpj"].ToString();
                    _IE = rowEmit[0]["ie"].ToString();
                    _Endereco = rowEnderEmit[0]["xlgr"].ToString();
                    _Cidade = rowEnderEmit[0]["xmun"].ToString();
                    _Nro = rowEnderEmit[0]["nro"].ToString();
                    _Bairro = rowEnderEmit[0]["xBairro"].ToString();
                    _CEP = rowEnderEmit[0]["CEP"].ToString();

                    g.DrawString(_RazaoSocial, Font1, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY));
                    g.DrawString(_Endereco + "," + _Nro + "," + _Complemento + "," + _Bairro + "," + _Cidade, Font1, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                    g.DrawString("CNPJ:" + _CNPJ + "  " + "IE:" + _IE, Font1, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                }
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }
            #endregion

            #region Numero Extrato

            try
            {
                g.DrawString(_separador, Font1, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY));
                g.DrawString("Extrato No.", Font2_negrito, Brushes.Black, new PointF(_posicaoColunaInicial + 80, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 15));
                g.DrawString(_NumeroExtrato, Font2_negrito, Brushes.Black, new PointF(_posicaoColunaInicial + 155, _posicaoBlocoFinalY));
                g.DrawString("CUPOM FISCAL ELETRÔNICO - SAT", Font2_negrito, Brushes.Black, new PointF(_posicaoColunaInicial + 20, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 15));
                g.DrawString("CANCELAMENTO", Font2_negrito, Brushes.Black, new PointF(_posicaoColunaInicial + 80, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 15));
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }
            #endregion

            #region NFP

            try
            {
                if (rowDest != null)
                {
                    _NFP = "";
                    try { _NFP = rowDest[0]["cpf"].ToString(); }
                    catch { _NFP = rowDest[0]["cnpj"].ToString(); }
                }
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }

            g.DrawString(_separador, Font1, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
            g.DrawString("DADOS DO CUPOM FISCAL CANCELADO", Font1, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 15));
            g.DrawString("CPF/CNPJ do Consumidor: " + _NFP, Font1, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 20));

            #endregion

            #region Totais Cupom

            if (rowTotal.Length > 0)
            {
                _ValorTotalCFe = rowTotal[0]["vCFe"].ToString();
                g.DrawString("TOTAL R$" + _ValorTotalCFe.Trim(), Font1_negrito, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
            }

            #endregion

            #region Bloco Final

            #region Posicao Bloco Final

            // _posicaoBlocoFinalY = 100;

            #endregion

            try
            {
                g.DrawString(" SAT Nº  " + _NumeroSerieSAT, Font1_negrito, Brushes.Black, new PointF(_posicaoColunaInicial + 90, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 15));
                g.DrawString(FormataDataHoraSDF(_DataHoraEnvio), Font1, Brushes.Black, new PointF(_posicaoColunaInicial + 90, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                g.DrawString(_strChaveAcessoLeituraHumanaEnvio, Font11_negrito, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                g.DrawImage(GerarEAN128(30, 30, _strChaveAcessoEnvio), new Rectangle(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10, 270, 40));
                g.DrawImage(GerarQRCode(80, 80, _strChaveAcessoEnvio + @"|" + _DataHoraEnvio + "|" + _ValorTotalCFe + "|" + _NFP + "|" + _AssinaturaQRcodeEnvio), new Rectangle(_posicaoColunaInicial + 65, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 40, 160, 160));

                _posicaoBlocoFinalY = _posicaoBlocoFinalY + 155;

                g.DrawString(_separador, Font1, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY));
                g.DrawString("DADOS DO CUPOM FISCAL DE CANCELAMENTO", Font1, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 15));
                g.DrawString(" SAT Nº  " + _NumeroSerieSAT, Font1_negrito, Brushes.Black, new PointF(_posicaoColunaInicial + 90, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 15));
                g.DrawString(FormataDataHoraSDF(_DataHora), Font1, Brushes.Black, new PointF(_posicaoColunaInicial + 90, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                g.DrawString(_strChaveAcessoLeituraHumana, Font11_negrito, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                g.DrawImage(GerarEAN128(30, 30, _strChaveAcesso), new Rectangle(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10, 270, 40));
                g.DrawImage(GerarQRCode(80, 80, _strChaveAcesso + @"|" + _DataHora + "|" + _ValorTotalCFe + "|" + _NFP + "|" + _AssinaturaQRcode), new Rectangle(_posicaoColunaInicial + 65, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 40, 160, 160));
            }
            catch
            { }

            #endregion

            #region Rodape

            _posicaoBlocoFinalY = _posicaoBlocoFinalY + 145;
            strTmp = Global.ConfiguracaoInicial.SatTextoRodape;
            g.DrawString(strTmp, Font1_negrito, Brushes.Black, new PointF(_posicaoColunaInicial + 68, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));

            int i = 0;
            while (i++ < Global.ConfiguracaoInicial.NumeroLinhasEntreCupom)
                _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10;

            g.DrawString(".", Font1_negrito, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));

            #endregion
        }
        private void PrintEmissaoCfeSAT(object sender, PrintPageEventArgs ppeArgs)
        {
            #region Variaveis Locais

            float Font_Tamanho1 = 7;
            Font FontCourier = new Font("courier new", Font_Tamanho1);

            Font FontSegoe = new Font("Segoe UI", Font_Tamanho1);

            float Font_Tamanho1_negrito = 7;
            Font Font1_negrito = new Font("courier new", Font_Tamanho1_negrito, FontStyle.Bold);

            float Font_Tamanho11_negrito = 7;
            Font Font11_negrito = new Font("arial", Font_Tamanho11_negrito, FontStyle.Bold);

            float Font_Tamanho2_negrito = 10;
            Font Font2_negrito = new Font("arial", Font_Tamanho2_negrito, FontStyle.Bold);

            int _posicaoBlocoFinalY = 0;
            int _posicaoColunaInicial = 0;

            string strTmp = "";
            string _separador = "_______________________________________________";
            string _strChaveAcesso = "".PadLeft(44, ' ');
            string _strChaveAcessoLeituraHumana = "";
            string _AssinaturaQRcode = "";
            string _DataHora = "";
            string _NumeroSerieSAT = "";
            string _MensagemContribuinte = "";
            string _ValorTotalCFe = "0,00";
            string _ValorTotalDesconto = "0,00";
            string _ValorTotalAcrescimo = "0,00";
            string _ValorTotalProdutos = "0,00";
            string _NFP = "";
            string _NumeroExtrato = "";
            string _RazaoSocial = "";
            string _CNPJ = "";
            string _IE = "";
            string _Endereco = "";
            string _Cidade = "";
            string _Nro = "";
            string _Complemento = "";
            string _Bairro = "";
            string _CEP = "";

            #endregion

            #region Dataset Xml

            DataSet ds = new DataSet();
            Graphics g = ppeArgs.Graphics;

            ds.ReadXml(this.ArquivoXML);
            DataRow[] rowIde = ds.Tables["ide"] != null ? ds.Tables["ide"].Select() : null;
            DataRow[] rowEmit = ds.Tables["emit"] != null ? ds.Tables["emit"].Select() : null;
            DataRow[] rowEnderEmit = ds.Tables["EnderEmit"] != null ? ds.Tables["EnderEmit"].Select() : null;
            DataRow[] rowProd = ds.Tables["prod"] != null ? ds.Tables["prod"].Select() : null;
            DataRow[] rowImposto = ds.Tables["imposto"] != null ? ds.Tables["imposto"].Select() : null;
            DataRow[] rowIcmsTot = ds.Tables["IcmsTot"] != null ? ds.Tables["IcmsTot"].Select() : null;
            DataRow[] rowDet = ds.Tables["det"] != null ? ds.Tables["det"].Select() : null;
            DataRow[] rowInfAdic = ds.Tables["infAdic"] != null ? ds.Tables["infAdic"].Select() : null;
            DataRow[] rowTotal = ds.Tables["total"] != null ? ds.Tables["total"].Select() : null;
            DataRow[] rowDest = ds.Tables["dest"] != null ? ds.Tables["dest"].Select() : null;
            DataRow[] rowinfCFe = ds.Tables["infcfe"] != null ? ds.Tables["infcfe"].Select() : null;
            DataRow[] rowICMSTot = ds.Tables["ICMSTot"] != null ? ds.Tables["ICMSTot"].Select() : null;
            DataRow[] rowDescAcrEntr = ds.Tables["DescAcrEntr"] != null ? ds.Tables["DescAcrEntr"].Select() : null;
            DataRow[] rowFinalizadoras = ds.Tables["MP"] != null ? ds.Tables["MP"].Select() : null;
            DataRow[] rowTroco = ds.Tables["pgto"] != null ? ds.Tables["pgto"].Select() : null;

            #endregion

            #region Numero Extrato

            try
            {
                if (rowIde.Length > 0)
                    _NumeroExtrato = rowIde[0]["nCFe"].ToString();
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }

            #endregion

            #region Numero Serie Sat

            try
            {
                if (rowIde.Length > 0)
                    _NumeroSerieSAT = rowIde[0]["nserieSAT"].ToString().Insert(3, ".").Insert(7, ".");//"900.001.246";
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }

            #endregion

            #region Data e Hora

            try
            {
                if (rowIde.Length > 0)
                    _DataHora = rowIde[0]["DEmi"].ToString() + rowIde[0]["hEmi"].ToString();
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }

            #endregion

            #region Assinatura QRCODE

            try
            {
                if (rowIde.Length > 0)
                    _AssinaturaQRcode = rowIde[0]["assinaturaQRCODE"].ToString();
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }

            #endregion

            #region Chave CFe

            try
            {
                if (rowinfCFe.Length > 0)
                {
                    _strChaveAcesso = rowinfCFe[0]["id"].ToString().Replace("CFe", "");
                    _strChaveAcessoLeituraHumana = _strChaveAcesso.Substring(0, 4) + " " + _strChaveAcesso.Substring(4, 4) + " " + _strChaveAcesso.Substring(8, 4) + " " + _strChaveAcesso.Substring(12, 4) + " " + _strChaveAcesso.Substring(16, 4) + " " + _strChaveAcesso.Substring(20, 4) + " " + _strChaveAcesso.Substring(24, 4) + " " + _strChaveAcesso.Substring(28, 4) + " " + _strChaveAcesso.Substring(32, 4) + " " + _strChaveAcesso.Substring(36, 4) + " " + _strChaveAcesso.Substring(40, 4);
                }
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }

            #endregion

            #region Dados Razao Social Empresa

            try
            {
                if (rowEmit.Length > 0)
                {
                    _RazaoSocial = rowEmit[0]["xNome"].ToString();
                    _CNPJ = rowEmit[0]["cnpj"].ToString();
                    _IE = rowEmit[0]["ie"].ToString();
                    _Endereco = rowEnderEmit.Length > 0 ? rowEnderEmit[0]["xlgr"].ToString() : "";
                    _Cidade = rowEnderEmit.Length > 0 ? rowEnderEmit[0]["xmun"].ToString() : "";
                    _Nro = rowEnderEmit.Length > 0 ? rowEnderEmit[0]["nro"].ToString() : "";
                    _Bairro = rowEnderEmit.Length > 0 ? rowEnderEmit[0]["xBairro"].ToString() : "";
                    _CEP = rowEnderEmit.Length > 0 ? rowEnderEmit[0]["CEP"].ToString() : "";

                    g.DrawString(_RazaoSocial, FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY));
                    g.DrawString(_Endereco + "," + _Nro + "," + _Complemento + "," + _Bairro + "," + _Cidade, FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                    g.DrawString("CNPJ:" + _CNPJ + "  " + "IE:" + _IE, FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                }
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }

            #endregion

            #region Numero Extrato

            try
            {
                g.DrawString(_separador, FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY));
                g.DrawString("Extrato No.", Font2_negrito, Brushes.Black, new PointF(_posicaoColunaInicial + 80, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 15));
                g.DrawString(_NumeroExtrato, Font2_negrito, Brushes.Black, new PointF(_posicaoColunaInicial + 155, _posicaoBlocoFinalY));
                g.DrawString("CUPOM FISCAL ELETRÔNICO - SAT", Font2_negrito, Brushes.Black, new PointF(_posicaoColunaInicial + 20, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 15));
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }

            #endregion

            #region NFP

            try
            {
                if (rowDest != null)
                {
                    _NFP = "";
                    try { _NFP = rowDest[0]["cpf"].ToString(); }
                    catch { _NFP = rowDest[0]["cnpj"].ToString(); }
                }
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }

            g.DrawString(_separador, FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
            g.DrawString("CPF/CNPJ do Consumidor: " + _NFP, FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 15));

            #endregion

            #region Itens

            try
            {
                g.DrawString(_separador, FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY));
                g.DrawString("#|COD|DESC|QTD|UN|VL UN R$|VL TR R$*|VL ITEM R$", FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 13));
                g.DrawString(_separador, FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 3));

                _posicaoBlocoFinalY = _posicaoBlocoFinalY + 3;

                if (rowProd.Length > 0)
                {
                    var linhasPorPagina = ppeArgs.MarginBounds.Height;
                    var linhasPorPaginas = ppeArgs.PageBounds;

                    int cont = 1;
                    foreach (DataRow r in ds.Tables["Prod"].Rows)
                    {
                        string _prodContador = cont.ToString("000");
                        string _prodCodigo = r["cProd"].ToString();
                        try
                        {
                            if (r["cEAN"].ToString().Trim() != "")
                                _prodCodigo = r["cEAN"].ToString();
                        }
                        catch { }

                        #region Desconto

                        string _prodValorDesconto = "";
                        try
                        {
                            _prodValorDesconto = r["vDesc"].ToString();
                        }
                        catch { }

                        #endregion

                        #region Acrescimo

                        string _prodValorAcrescimo = "";
                        try
                        {
                            _prodValorAcrescimo = r["vOutro"].ToString();
                        }
                        catch { }

                        #endregion


                        string _impostoIBPT = "";
                        try
                        {
                            if (rowImposto != null && rowImposto.Length > 0)
                                _impostoIBPT = rowImposto[cont - 1]["vItem12741"].ToString();
                        }
                        catch { }

                        string _prodDescritivo = r["xProd"].ToString();
                        string _prodQuantidade = r["qcom"].ToString();
                        string _prodUnidade = r["ucom"].ToString();
                        string _prodValorUnitario = r["vUnCom"].ToString();
                        string _prodValorTotal = FormatarStr(r["vProd"].ToString(), 9, 3, ' ');
                        string _prodValorLiquido = r["vItem"].ToString();

                        string _rateioDesconto = "";
                        try
                        {
                            _rateioDesconto = r["vRatDesc"].ToString();
                        }
                        catch { }

                        string _rateioAcrescimo = "";
                        try
                        {
                            _rateioAcrescimo = r["vRatAcr"].ToString();
                        }
                        catch { }


                        try
                        {
                            g.DrawString(_prodContador + " " + _prodCodigo + " " + _prodDescritivo, FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                            g.DrawString(_prodQuantidade + " " + _prodUnidade + " X " + _prodValorUnitario + (string.IsNullOrEmpty(_impostoIBPT) ? "" : " (" + _impostoIBPT + ")"), FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                            g.DrawString(_prodValorTotal, FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial + 227, _posicaoBlocoFinalY));
                        }
                        catch { }

                        #region Acrescimo

                        try
                        {
                            if (_prodValorAcrescimo != "")
                            {
                                g.DrawString("acréscimo sobre item", FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                                g.DrawString(FormatarStr(_prodValorAcrescimo, 9, 3, ' '), FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial + 227, _posicaoBlocoFinalY));
                            }
                        }
                        catch { }

                        #endregion

                        #region Desconto

                        try
                        {
                            if (_prodValorDesconto != "")
                            {
                                g.DrawString("desconto sobre item", FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                                g.DrawString(FormatarStr("-" + _prodValorDesconto, 9, 3, ' '), FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial + 227, _posicaoBlocoFinalY));
                            }
                        }
                        catch { }

                        #endregion


                        #region Rateio Acrescimo Subtotal

                        try
                        {
                            if (!string.IsNullOrEmpty(_rateioAcrescimo))
                            {
                                g.DrawString("rateio acréscimo sobre subtotal", FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                                g.DrawString(FormatarStr(_rateioAcrescimo, 9, 3, ' '), FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial + 227, _posicaoBlocoFinalY));
                            }
                        }
                        catch { }

                        #endregion

                        #region Rateio Descinto Subtotal

                        try
                        {
                            if (!string.IsNullOrEmpty(_rateioDesconto))
                            {
                                g.DrawString("rateio desconto sobre subtotal", FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                                g.DrawString(FormatarStr("-" + _rateioDesconto, 9, 3, ' '), FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial + 227, _posicaoBlocoFinalY));
                            }
                        }
                        catch { }

                        #endregion

                        cont++;
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
            }

            #endregion

            #region Totais Cupom

            if (rowTotal.Length > 0)
            {
                _ValorTotalCFe = rowTotal[0]["vCFe"].ToString();

                if (rowICMSTot.Length > 0)
                {
                    try
                    {
                        try
                        {
                            _ValorTotalDesconto = rowICMSTot[0]["vDesc"].ToString();
                        }
                        catch { }

                        try
                        {
                            _ValorTotalProdutos = rowIcmsTot[0]["vProd"].ToString();
                        }
                        catch { }
                        try
                        {
                            _ValorTotalAcrescimo = rowIcmsTot[0]["vOutro"].ToString();
                        }
                        catch { }
                    }
                    catch (Exception ex)
                    {
                        //Log.LogEcf("AP CFe-SAT --IMPRESSAO  :" + ex.ToString());
                    }
                }

                string valorDescontoSubTotal = "";
                string valorAcrescimoSubTotal = "";
                if (rowDescAcrEntr != null && rowDescAcrEntr.Length > 0)
                {
                    try
                    {
                        valorDescontoSubTotal = rowDescAcrEntr[0]["vDescSubtot"].ToString();
                    }
                    catch { }

                    try
                    {
                        valorAcrescimoSubTotal = rowDescAcrEntr[0]["vAcresSubtot"].ToString();
                    }
                    catch { }
                }

                g.DrawString("Total bruto dos itens", FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 20));
                g.DrawString(FormatarStr(_ValorTotalProdutos, 9, 3, ' '), FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial + 227, _posicaoBlocoFinalY));

                decimal totalAcresDesc = Global.Funcoes.ConvertToDecimal(_ValorTotalAcrescimo.Replace(",", "#").Replace(".", ",").Replace("#", "")) + (Global.Funcoes.ConvertToDecimal(_ValorTotalDesconto.Replace(",", "#").Replace(".", ",").Replace("#", "")) * -1);
                if (totalAcresDesc != 0M)
                {
                    g.DrawString("Total desc/acrés sobre item", FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                    g.DrawString(FormatarStr(totalAcresDesc.ToString("N2").Replace(".", "#").Replace(",", ".").Replace("#", ""), 9, 3, ' '), FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial + 227, _posicaoBlocoFinalY));
                }
                if (!string.IsNullOrEmpty(valorAcrescimoSubTotal))
                {
                    g.DrawString("Acréscimo sobre subtotal", FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                    g.DrawString(FormatarStr(valorAcrescimoSubTotal, 9, 3, ' '), FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial + 227, _posicaoBlocoFinalY));
                }
                else if (!string.IsNullOrEmpty(valorDescontoSubTotal))
                {
                    g.DrawString("Desconto sobre subtotal", FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                    g.DrawString(FormatarStr("-" + valorDescontoSubTotal, 9, 3, ' '), FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial + 227, _posicaoBlocoFinalY));
                }

                g.DrawString("TOTAL R$ ", Font1_negrito, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 25));
                g.DrawString(FormatarStr(_ValorTotalCFe.Trim(), 9, 3, ' '), FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial + 227, _posicaoBlocoFinalY));
            }

            #endregion

            #region Formas de Pagamento

            if (rowFinalizadoras.Length > 0)
            {

                foreach (DataRow r in ds.Tables["MP"].Rows)
                {
                    string _NomeFinalizadora = "Dinheiro";
                    string _ValorFinalizadora = r["vMP"].ToString();

                    if (r["cMP"].ToString() == "01")
                        _NomeFinalizadora = "Dinheiro";
                    else if (r["cMP"].ToString() == "02")
                        _NomeFinalizadora = "Cheque";
                    else if (r["cMP"].ToString() == "03")
                        _NomeFinalizadora = "Cartão de Crédito";
                    else if (r["cMP"].ToString() == "04")
                        _NomeFinalizadora = "Cartão de Débito";
                    else if (r["cMP"].ToString() == "05")
                        _NomeFinalizadora = "Crédito Loja";
                    else if (r["cMP"].ToString() == "10")
                        _NomeFinalizadora = "Vale Alimentação";
                    else if (r["cMP"].ToString() == "11")
                        _NomeFinalizadora = "Vale Refeição";
                    else if (r["cMP"].ToString() == "12")
                        _NomeFinalizadora = "Vale Presente";
                    else if (r["cMP"].ToString() == "13")
                        _NomeFinalizadora = "Vale Combustível";
                    else if (r["cMP"].ToString() == "99")
                        _NomeFinalizadora = "Outros";

                    g.DrawString(_NomeFinalizadora, FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                    g.DrawString(FormatarStr(_ValorFinalizadora.Trim(), 9, 3, ' '), FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial + 227, _posicaoBlocoFinalY));
                }
            }

            #endregion

            #region Troco

            if (rowTroco.Length > 0)
            {
                foreach (DataRow r in ds.Tables["pgto"].Rows)
                {
                    string _ValorTroco = r["vTroco"].ToString();
                    g.DrawString("Troco", FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                    g.DrawString(FormatarStr(_ValorTroco.Trim(), 9, 3, ' '), FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial + 227, _posicaoBlocoFinalY));
                }
            }

            #endregion

            #region Mensagem Contribuinte

            try
            {
                if (rowInfAdic.Length > 0)
                    _MensagemContribuinte = rowInfAdic[0]["infcpl"].ToString().Trim(); ;
            }
            catch { }

            g.DrawString(_separador, FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
            g.DrawString("OBSERVAÇÕES DO CONTRIBUINTE", FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial + 50, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 15));

            #region Dados da Mensagem

            string[] words = _MensagemContribuinte.Split(new Char[] { '|',';' });
            foreach (string s in words)
                g.DrawString(s, FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));

            #endregion

            g.DrawString(_separador, FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 5));


            #endregion

            #region Bloco Final

            try
            {
                g.DrawString(" SAT Nº  " + _NumeroSerieSAT, Font1_negrito, Brushes.Black, new PointF(_posicaoColunaInicial + 90, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 15));
                g.DrawString(FormataDataHoraSDF(_DataHora), FontCourier, Brushes.Black, new PointF(_posicaoColunaInicial + 90, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                g.DrawString(_strChaveAcessoLeituraHumana, Font11_negrito, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));
                g.DrawImage(GerarEAN128(30, 30, _strChaveAcesso), new Rectangle(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10, 270, 40));
                g.DrawImage(GerarQRCode(80, 80, _strChaveAcesso + @"|" + _DataHora + "|" + _ValorTotalCFe + "|" + _NFP + "|" + _AssinaturaQRcode), new Rectangle(_posicaoColunaInicial + 65, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 40, 160, 160));
            }
            catch { }

            #endregion

            #region Rodape

            _posicaoBlocoFinalY = _posicaoBlocoFinalY + 145;
            strTmp = Global.ConfiguracaoInicial.SatTextoRodape;
            g.DrawString(strTmp, Font1_negrito, Brushes.Black, new PointF(_posicaoColunaInicial + 90, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 15));

            int i = 0;
            while (i++ < Global.ConfiguracaoInicial.NumeroLinhasEntreCupom)
                _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10;

            g.DrawString(".", Font1_negrito, Brushes.Black, new PointF(_posicaoColunaInicial, _posicaoBlocoFinalY = _posicaoBlocoFinalY + 10));

            #endregion
        }

        public Bitmap GerarQRCode(int width, int height, string text)
        {
            try
            {
                var bw = new ZXing.BarcodeWriter();
                var encOptions = new ZXing.Common.EncodingOptions() { Width = width, Height = height, Margin = 0 };
                bw.Options = encOptions;
                bw.Format = ZXing.BarcodeFormat.QR_CODE;
                var resultado = new Bitmap(bw.Write(text));
                return resultado;
            }
            catch
            {
                throw;
            }
        }
        public Bitmap GerarEAN128(int width, int height, string text)
        {
            try
            {
                var code = text;
                var wr = new ZXing.BarcodeWriter { Format = ZXing.BarcodeFormat.CODE_128, Options = new ZXing.Common.EncodingOptions { PureBarcode = true, Width = width, Height = height } };
                var bytes = wr.Write(code);
                var resultado = new Bitmap(wr.Write(text));
                return resultado;
            }
            catch
            {
                throw;
            }
        }

        public string FormatarStr(string Str, int Quantidade, int Alinhamento, char Caracter)
        {
            if (Alinhamento == 1) // alinhamento a Direta
            {
                if (Str.Length < Quantidade)
                    Str = Str.PadRight(Quantidade, Caracter);
                else
                    Str = Str.Substring(0, Quantidade);
            }
            else if (Alinhamento == 2) // alinhamento a Esquerda FORMATANDO VALOR MONETARIO
            {
                Str = Str.Replace("R$ ", "").Replace(",", "").Replace(".", "");
                if (Str.Length < Quantidade)
                    Str = Str.PadLeft(Quantidade, Caracter);
                else
                    Str = Str.Substring(0, Quantidade);
            }
            else if (Alinhamento == 3) // alinhamento a Esquerda SEM FORMATACAO
            {
                if (Str.Length < Quantidade)
                    Str = Str.PadLeft(Quantidade, Caracter);
                else
                    Str = Str.Substring(0, Quantidade);
            }
            return Str;
        }
        public string FormatarDataSdf(string Data)
        {
            string retorno = "";
            if (Data.Length == 4)
                retorno = Data.Substring(0, 2) + "/" + Data.Substring(2, 2);
            else if (Data.Length == 6)
                retorno = Data.Substring(0, 2) + "/" + Data.Substring(2, 2) + "/" + Data.Substring(4, 2);
            else if (Data.Length == 8)
                retorno = Data.Substring(0, 2) + "/" + Data.Substring(2, 2) + "/" + Data.Substring(4, 4);

            try
            {
                return (Convert.ToDateTime(retorno)).ToString("dd-MM-yyyy"); ;
            }
            catch
            {
                return "01-01-2015";
            }
        }
        public static string FormataDataHoraSDF(string data)
        {
            if (data.Trim() == "") return "00/00/0000";
            return Convert.ToDateTime(data.Substring(0, 4) + "/" + data.Substring(4, 2) + "/" + data.Substring(6, 2)).ToString("dd/MM/yyyy") + " - " + data.Substring(8, 2) + ":" + data.Substring(10, 2) + ":" + data.Substring(12, 2);
        }
        public string SatDescriptogragia(string _valueDecrypt)
        {
            byte[] data = Convert.FromBase64String(_valueDecrypt);
            return Encoding.UTF8.GetString(data);
        }
    }
}