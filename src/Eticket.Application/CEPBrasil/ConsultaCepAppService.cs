using System;
using System.IO;
using System.Net;
using System.Text;
using Eticket.Application.ViewModels;
using Newtonsoft.Json;

namespace Eticket.Application.CEPBrasil
{
    public class ConsultaCepAppService
    {
        public static ConsultaCepViewModel ConsultaPorCep(string cep)
        {
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;

            var text = client.DownloadString("https://viacep.com.br/ws/" + cep + "/json/");
            return JsonConvert.DeserializeObject<ConsultaCepViewModel>(text);
        }

        public static ConsultaCepViewModel ConsultaCep(string cep)
        {
            var getUri = "https://viacep.com.br/ws/" + cep + "/json/";

            WebRequest request = (WebRequest)WebRequest.Create(getUri);
            // execute the request
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //read the response
            using (var responseReader = new StreamReader(response.GetResponseStream()))
            {
                String resultmsg = responseReader.ReadToEnd();
                responseReader.Close();

                var resultobj = JsonConvert.DeserializeObject<ConsultaCepViewModel>(resultmsg);

                return resultobj;
            }

        }
    }
}