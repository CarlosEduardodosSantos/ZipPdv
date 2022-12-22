using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;


namespace Eticket.Application
{
    public class DataHoraAtualAppService : IDataHoraAtualAppService
    {
        public DataHoraAtualViewModel ConsultaDataHora()
        {
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;

            var text = client.DownloadString("http://worldtimeapi.org/api/timezone/America/Sao_Paulo");
            return JsonConvert.DeserializeObject<DataHoraAtualViewModel>(text);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
