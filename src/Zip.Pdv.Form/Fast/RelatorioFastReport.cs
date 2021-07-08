using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using FastReport;
using FastReport.Utils;

namespace Zip.Pdv.Fast
{
    public class RelatorioFastReport
    {
        private TipoImpressaoViewEnum _tipoImpressaoViewEnum;
        private readonly string _connectionString;
        public RelatorioFastReport()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyContext"].ConnectionString;
            //_tipoImpressaoViewEnum = Program.InicializacaoViewAux.TipoImpressao;
            _tipoImpressaoViewEnum = TipoImpressaoViewEnum.Print;
        }

        public void GerarRelatorio(string nameFrx, ParameterReportDynamic parameterReport)
        {
            var report = new FastReport.Report();

            var reportPropriedade = new EnvironmentSettings
            {
                PreviewSettings = { ShowInTaskbar = false },
                UIStyle = UIStyle.VisualStudio2005
            };

            reportPropriedade.PreviewSettings.Buttons = PreviewButtons.Close |
                                                        PreviewButtons.Print |
                                                        PreviewButtons.Zoom |
                                                        PreviewButtons.Save;

            report.LoadFromString(LoadFileReportDb(nameFrx));

            if (report.Dictionary.Connections.Count > 0)
            {
                report.Dictionary.Connections[0].ConnectionString = _connectionString;
                report.Prepare();
            }

            for (int i = 0; i < parameterReport.ListParameters.Count; i++)
            {
                var keys = parameterReport.ListParameters.Keys.ToList()[i];
                var value = parameterReport.ListParameters.Values.ToList()[i];

                report.SetParameterValue(keys, value);
            }

            switch (_tipoImpressaoViewEnum)
            {
                case TipoImpressaoViewEnum.PopUp:
                {
                    report.Show();
                    break;
                }
                case TipoImpressaoViewEnum.Print:
                {
                    report.PrintSettings.ShowDialog = false;
                    report.Print();
                    break;
                }
                case TipoImpressaoViewEnum.Design:
                {
                    report.Design();
                    UpdateFileReportDb(nameFrx, report.ReportResourceString);
                    break;
                }
                default:
                    break;
            }
        }
        public void GerarRelatorio(string nameFrx, ParameterReportDynamic parameterReport, DataSet ds)
        {
            var report = new Report();



            report.RegisterData(ds);

            var reportPropriedade = new EnvironmentSettings
            {
                PreviewSettings = { ShowInTaskbar = false },
                UIStyle = UIStyle.VisualStudio2005
            };

            reportPropriedade.PreviewSettings.Buttons = PreviewButtons.Close |
                                                        PreviewButtons.Print |
                                                        PreviewButtons.Zoom |
                                                        PreviewButtons.Save;

            var file = LoadFileReportDb(nameFrx);
            if (File.Exists(file))
            {
                report.LoadFromString(file);
                report.Prepare();
            }
            else
                _tipoImpressaoViewEnum = TipoImpressaoViewEnum.Design;

            
            if (report.Dictionary.Connections.Count > 0)
            {
                report.Dictionary.Connections[0].ConnectionString = _connectionString;
                report.Prepare();
            }

            for (int i = 0; i < parameterReport.ListParameters.Count; i++)
            {
                var keys = parameterReport.ListParameters.Keys.ToList()[i];
                var value = parameterReport.ListParameters.Values.ToList()[i];

                report.SetParameterValue(keys, value);
            }

            switch (_tipoImpressaoViewEnum)
            {
                case TipoImpressaoViewEnum.PopUp:
                {
                    report.Show();
                    break;
                }
                case TipoImpressaoViewEnum.Print:
                {
                    report.PrintSettings.ShowDialog = false;
                    report.Print();
                    break;
                }
                case TipoImpressaoViewEnum.Design:
                {
                    report.Design();
                    UpdateFileReportDb(nameFrx, report.ReportResourceString);
                    break;
                }
                default:
                    break;
            }
        }
        public void GerarRelatorio<T>(string nameFrx, ParameterReportDynamic parameterReport, List<T> models)
        {
            var report = new Report();
            var ds = new DataSet();

            ds.Tables.Add(Funcoes.ConverteListParaDataTable(models, "models"));
            report.RegisterData(ds);

            var reportPropriedade = new EnvironmentSettings
            {
                PreviewSettings = { ShowInTaskbar = false },
                UIStyle = UIStyle.VisualStudio2005
            };

            reportPropriedade.PreviewSettings.Buttons = PreviewButtons.Close |
                                                        PreviewButtons.Print |
                                                        PreviewButtons.Zoom |
                                                        PreviewButtons.Save;

            report.LoadFromString(LoadFileReportDb(nameFrx));
            if (report.Dictionary.Connections.Count > 0)
            {
                report.Dictionary.Connections[0].ConnectionString = _connectionString;
                report.Prepare();
            }

            for (int i = 0; i < parameterReport.ListParameters.Count; i++)
            {
                var keys = parameterReport.ListParameters.Keys.ToList()[i];
                var value = parameterReport.ListParameters.Values.ToList()[i];

                report.SetParameterValue(keys, value);
            }

            switch (_tipoImpressaoViewEnum)
            {
                case TipoImpressaoViewEnum.PopUp:
                {
                    report.Show();
                    break;
                }
                case TipoImpressaoViewEnum.Print:
                {
                    report.PrintSettings.ShowDialog = false;
                    report.Print();
                    break;
                }
                case TipoImpressaoViewEnum.Design:
                {
                    report.Design();
                    UpdateFileReportDb(nameFrx, report.ReportResourceString);
                    break;
                }
                default:
                    break;
            }
        }

        private string LoadFileReportDb(string fileName)
        {
            using (var db = Program.Container.GetInstance<IFabricaRelatorioAppService>())
            {
                var report = db.ObterPorPesquisa(fileName).FirstOrDefault();
                return report == null ? InsertFileReportDb(fileName) : report.File;
            }

        }

        private string InsertFileReportDb(string fileName)
        {
            var danfeFRx = new Report();
            String frx = Application.StartupPath + "/Report/" + fileName + ".frx";
            if (!File.Exists(frx))
                return danfeFRx.ReportResourceString = "";

            danfeFRx.Load(frx);

            var fabricaRelatorio = new FabricaRelatorioViewModel();
            fabricaRelatorio.DataHora = DateTime.Now;
            fabricaRelatorio.UduarioId = Program.Usuario.UsuarioId;
            fabricaRelatorio.FileName = fileName;
            fabricaRelatorio.File = danfeFRx.ReportResourceString;
            fabricaRelatorio.Tipo = FabricaRelatorioTipoViewEnum.Sistema;

            using (var fabricaRelatorioService = Program.Container.GetInstance<IFabricaRelatorioAppService>())
            {
                fabricaRelatorioService.Adcionar(fabricaRelatorio);
            }

            return danfeFRx.ReportResourceString;
        }

        private void UpdateFileReportDb(string fileName, string file)
        {
            using (var fabricaRelatorioService = Program.Container.GetInstance<IFabricaRelatorioAppService>())
            {

                var fabricaRelatorio = fabricaRelatorioService.ObterPorPesquisa(fileName).FirstOrDefault();

                if (fabricaRelatorio != null)
                {
                    fabricaRelatorio.FileName = fileName;
                    fabricaRelatorio.File = file;

                    fabricaRelatorioService.Editar(fabricaRelatorio);
                }
                else
                {
                    fabricaRelatorio = new FabricaRelatorioViewModel
                    {
                        DataHora = DateTime.Now,
                        UduarioId = Program.Usuario.UsuarioId,
                        FileName = fileName,
                        File = file,
                        Tipo = FabricaRelatorioTipoViewEnum.Sistema
                    };

                    fabricaRelatorioService.Adcionar(fabricaRelatorio);
                }
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}