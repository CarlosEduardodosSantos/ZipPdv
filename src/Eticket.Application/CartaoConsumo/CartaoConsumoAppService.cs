using Eticket.Application.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.CartaoConsumo
{
    public class CartaoConsumoAppService
    {
        public static CartaoConsumoMovRespViewModel AutorizarMovimentacao(int restauranteId,string numeroCartao, 
            decimal valor, string historico, int tipoOp)
        {
            var cartaoMovViewModel = new CartaoConsumoMov() 
            {
                NumeroCartao = numeroCartao,
                DataMov = DateTime.Now,
                Valor = valor,
                RestauranteId = restauranteId,
                Historico = historico,
                TipoMov = tipoOp
            };
            
            var cartaoMovJdon = JsonConvert.SerializeObject(cartaoMovViewModel);
            var httpContent = new StringContent(cartaoMovJdon, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                var response = client.PostAsync($"http://api.zclub.com.br/api/CartaoConsumo/adicionarMov", httpContent);
                //var response = client.PostAsync($"http://localhost:56435/api/CartaoConsumo/adicionarMov", httpContent);
                if (response.Result.IsSuccessStatusCode)
                {
                    var xml = response.Result.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<CartaoConsumoMovRespViewModel>(xml);
                    return result;
                }
                else
                {
                    throw new Exception(response?.Exception?.Message);
                }
            }
        }
        public static RespostaGenericaViewModel EstornaMovimentacao(Guid movId, string usuario,
            decimal valor, string historico)
        {
            /*var cartaoMovViewModel = new CartaoConsumoMov()
            {
                NumeroCartao = numeroCartao,
                DataMov = DateTime.Now,
                Valor = valor,
                RestauranteId = restauranteId,
                Historico = historico
            };
            var cartaoMovJdon = JsonConvert.SerializeObject(cartaoMovViewModel);
            var httpContent = new StringContent(cartaoMovJdon, Encoding.UTF8, "application/json");
             */


            using (var client = new HttpClient())
            {
                //var response = client.DeleteAsync($"http://api.zclub.com.br/api/CartaoConsumo/deletarMov/{movId}/{usuario}");
                var response = client.DeleteAsync($"http://localhost:56435/api/CartaoConsumo/deletarMov/{movId}/{usuario}");
                if (response.Result.IsSuccessStatusCode)
                {
                    var xml = response.Result.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<RespostaGenericaViewModel>(xml);
                    return result;
                }
                else
                {
                    throw new Exception(response?.Exception?.Message);
                }
            }
        }
    }
}
