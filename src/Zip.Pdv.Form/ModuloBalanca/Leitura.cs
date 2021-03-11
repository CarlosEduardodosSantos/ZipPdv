using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Zip.Pdv.ModuloBalanca
{
    public class Leitura
    {
        public double ObterPeso()
        {
            var balanca = FindByPC();
            if (balanca != null)
            {
                try
                {
                    //ModeloBalancas modelo = (ModeloBalancas)balanca.Modelo;
                    ModeloBalancas modelo = ModeloBalancas.Filizola_E;
                    //PortaCOM porta = balanca.GetPortaCOM();
                    var porta = PortaCOM.COM5;
                    int velocidade = balanca.DataBits;
                    
                    PcScale.SalvaParametrosBalanca(1, modelo, porta, balanca.BaudRate);
                    PcScale.InicializaLeitura(1);

                    int intervalo = GetParametroArquivo(1, 2000);
                    int tentativas = GetParametroArquivo(2, 3);

                    Thread.Sleep(intervalo);

                    double statusBalanca = PcScale.ObtemInformacao(1, RetornoBalanca.Status);
                    if (statusBalanca != 2)
                    {
                        for (int i = 0; i < tentativas; i++)
                        {
                            statusBalanca = PcScale.ObtemInformacao(1, RetornoBalanca.Status);
                            if (statusBalanca == 2)
                                break;
                        }
                    }

                    if (statusBalanca != 2)
                    {
                        var erro = string.Empty;
                        PcScale.ObtemMsgErro(out erro);

                        PcScale.FinalizaLeitura(1);
                        return 0;
                    }

                    double valor = PcScale.ObtemInformacao(1, RetornoBalanca.PesoBruto);
                    if (valor <= 0)
                    {
                        for (int i = 0; i < tentativas; i++)
                        {
                            valor = PcScale.ObtemInformacao(1, RetornoBalanca.PesoBruto);
                            Thread.Sleep(100);
                            if (valor > 0)
                                break;
                        }
                    }

                    if (valor > 0)
                        valor = (valor / 1000);

                    PcScale.FinalizaLeitura(1);
                    return valor;
                }
                catch (Exception ex)
                {
                    //LogController.WriteLog(" *** ERRO BALANÇA *** ", ex);
                    return 0;
                }
            }
            else
                return 0;
        }
        private int GetParametroArquivo(int linha, int padrao)
        {
            linha = (linha - 1);
            if (File.Exists(@"C:\MonitorBalanca\Intervalo.txt"))
            {
                try
                {
                    return int.Parse(File.ReadAllLines(@"C:\MonitorBalanca\Intervalo.txt")[linha]);
                }
                catch
                {
                    return padrao;
                }
            }

            return padrao;
        }
        private Model.Balanca FindByPC()
        {
            string computador = Environment.MachineName;
            //balanca = db.Where(b => b.Computador.Equals(computador)).FirstOrDefault();
            var balanca = new Model.Balanca()
            {
                BaudRate = 9600,
                DataBits = 8
            };
            return balanca;
        }
    }
}