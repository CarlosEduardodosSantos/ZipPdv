/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;

namespace Zip.Pdv.Zip.Balanca
{
    public class BalancaController : IDisposable
    {
        private BalancaRepository db = null;

        public BalancaController()
        {
            db = new BalancaRepository();
        }

        public bool Save(Balanca balanca)
        {
            try
            {
                if (!Valid(balanca))
                    return false;

                if (db.Find(balanca.Id) == null)
                    db.Save(balanca);
                else
                    db.Update(balanca);

                db.Commit();
                BStatus.Success($"Balança do computador {balanca.Computador} salva com sucesso");
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += ex.InnerException.Message;

                BStatus.Error($"Ocorreu um problema ao salvar a balança. {msg}");
                return false;
            }
        }

        private bool Valid(Balanca b)
        {
            if (string.IsNullOrEmpty(b.Computador))
            {
                BStatus.Alert("Informe o nome do computador para a balança");
                return false;
            }

            if (b.TempoResposta == 0)
            {
                BStatus.Alert("O tempo de resposta deve ser superior a 0");
                return false;
            }

            return true;
        }

        public EtiquetaBalanca LerEtiqueta(string etiqueta)
        {
            try
            {
                var balanca = FindByPC();
                if (balanca == null)
                    return null;

                if (!balanca.LerCodigoBarras)
                    return null;

                if (etiqueta.Length < balanca.TamanhoCodigoBarras)
                    return null;

                if (!etiqueta.StartsWith(balanca.DigitosIniciais))
                    return null;

                string codigoProduto = etiqueta.Substring(balanca.InicioCodProd - 1,
                    balanca.FimCodProd - 1);
                string pesoPreco = etiqueta.Substring(balanca.InicioPesoPreco - 1,
                    (balanca.FimPesoPreco - 1) -  (balanca.InicioPesoPreco - 1));
                
                var produto = new ProdutosController().Find(int.Parse(codigoProduto));
                return new EtiquetaBalanca(codigoProduto, (TipoPesoPreco)balanca.TipoPesoPreco,
                    decimal.Parse(pesoPreco) / 100, produto);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public Balanca Find(int id)
        {
            return db.Find(id);
        }

        public List<Balanca> ListAll()
        {
            return db.Where(p => p.Id > 0).ToList();
        }

        public bool Remove(int id)
        {
            try
            {
                db.Remove(Find(id));
                db.Commit();

                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += ex.InnerException.Message;

                BStatus.Error($"Ocorreu um problema ao remover a balança. {msg}");
                return false;
            }
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

        public async Task<double> ObterPeso()
        {
            return await Task.Run(() =>
             {
                 var balanca = FindByPC();
                 if (balanca != null)
                 {
                     try
                     {
                         //ModeloBalancas modelo = (ModeloBalancas)balanca.Modelo;
                         ModeloBalancas modelo = ModeloBalancas.Filizola_MF;
                         //PortaCOM porta = balanca.GetPortaCOM();
                         var porta = PortaCOM.COM1;
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
                         LogController.WriteLog(" *** ERRO BALANÇA *** ", ex);
                         return 0;
                     }
                 }
                 else
                     return (double)GetPesoAcbr();
             });
        }

        private decimal GetPesoAcbr()
        {
            decimal result = 0;
            StreamWriter writer = null;
            StreamReader reader = null;
            try
            {
                int intervalo = 500;
                if (File.Exists(@"C:\MonitorBalanca\Intervalo.txt"))
                    intervalo = int.Parse(File.ReadAllText(@"C:\MonitorBalanca\Intervalo.txt"));

                writer = new StreamWriter(@"C:\MonitorBalanca\ENT\ENT.TXT");
                writer.WriteLine($"BAL.LePeso({intervalo})");
                writer.Close();

                Thread.Sleep(intervalo);

                string line = File.ReadAllText(@"C:\MonitorBalanca\SAI\ENT-resp.txt");
                File.Delete(@"C:\MonitorBalanca\SAI\ENT-resp.txt");

                line = line.Replace("OK: ", string.Empty);
                decimal.TryParse(line, out result);
            }
            catch (Exception ex)
            {
                LogController.WriteLog("Erro peso balanca", ex);

                if (writer != null)
                    writer.Close();
                if (reader != null)
                    reader.Close();
            }
            return (result == 0
                ? GetPesoModoAlternativo()
                : result);
        }

        private decimal GetPesoModoAlternativo()
        {
            Thread.Sleep(100);
            string path = @"C:\MonitorBalanca\SAI\ENT-resp2.txt";
            if (!File.Exists(path))
                return 0;

            decimal result = 0;
            StreamReader reader = null;

            try
            {
                reader = new StreamReader(path);
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("UltimoPesoLido"))
                    {
                        string[] parts = line.Split(' ');
                        for (int i = 0; i < parts.Length; i++)
                        {
                            if (parts[i].Contains("kg"))
                            {
                                string valorPeso = parts[i].Replace("kg", "");
                                reader.Close();
                                File.WriteAllText(path, "");
                                return decimal.Parse(valorPeso);
                            }
                        }
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                if (reader != null)
                    reader.Close();
            }

            File.WriteAllText(path, "");
            return result;
        }

        private Model.Balanca FindByPC()
        {
            Model.Balanca balanca = null;
            string computador = Environment.MachineName;
            balanca = db.Where(b => b.Computador.Equals(computador)).FirstOrDefault();
            return balanca;
        }

        private Model.Balanca GetFromCache()
        {
            string computador = Environment.MachineName;
            Cache<Model.Balanca> balanca = CacheRepository<Balanca>.Get($"Balanca-{computador}");

            return (balanca == null
                ? null
                : balanca.Value);
        }

        public static void exportBalanca(string destino, string tipo)
        {
            using (var produtoAppService = Program.Container.GetInstance<IProdutoAppService>())
            {
                var lp = produtoAppService.ObterPorNome("%%%").Where(p => p.ParaBalanca).ToList();
                if (lp.Count == 0)
                    return;

                if (tipo.Equals("T"))
                    exportBalanca_Toleto(destino, lp);
                else if (tipo.Equals("F"))
                    exportBalanca_Filizola(destino, lp);
                else
                    return;
            }

        }

        private static void exportBalanca_Filizola(string destino, List<ProdutoViewModel> lp)
        {
            try
            {
                StreamWriter file = new StreamWriter(destino + "\\CADTXT.TXT");

                foreach (ProdutoViewModel obj in lp)
                {
                    string line = formatString(obj.ProdutoId.ToString(), 6, '0', false);
                    line += "P"; // tipo produto
                    line += formatString(obj.Descricao, 22, ' ', true);
                    line += formatString(obj.ValorVenda.ToString("n").Replace(".", string.Empty)
                        .Replace(",", string.Empty), 7, '0', false);
                    line += "010";
                    file.WriteLine(line);
                }

                file.Close();
            }
            catch
            {
                //BStatus.Error("Erro ao exportar");
            }
        }

        private static void exportBalanca_Toleto(string destino, List<ProdutoViewModel> lp)
        {
            StreamWriter file = new StreamWriter(destino + "\\ITENSMGV.TXT");

            foreach (ProdutoViewModel obj in lp)
            {
                string line = formatString(01.ToString("00"), 2, '0', false); //SEÇÃO
                line += "0"; // tipo produto
                line += formatString(obj.ProdutoId.ToString(), 6, '0', false); //CODIGO PROD
                line += formatString(obj.ValorVenda.ToString("n").Replace(".", string.Empty)
                    .Replace(",", string.Empty), 6, '0', false);
                line += formatString("0", 3, '0', false); // dias validade produto
                line += formatString(obj.Descricao, 50, ' ', true); //DESCRICAO

                int diasValidade = 0;
                int.TryParse("0", out diasValidade);
                string imprimirValidade = (diasValidade > 0
                    ? "1"
                    : "0");
                line += $"0000000000000000{imprimirValidade}10000000000000000000000000000000000000000000000000000000000000000";
                file.WriteLine(line);
            }

            file.Close();
        }

        private static string formatString(string valor, 
            int total, char complete, bool direita)
        {
            if (valor.Length > total)
                return valor.Substring(0, total);
            else
            {
                if (direita)
                    return valor.PadRight(total, complete);
                else
                    return valor.PadLeft(total, complete); ;
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                db.Context.Dispose();
                db.Context = null;
            }

            disposed = true;
        }
    }
}
*/