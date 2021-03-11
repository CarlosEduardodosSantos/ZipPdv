using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Eticket.Application.ViewModels;
using MobileAppServer.ServerObjects;
using SAT;
using SAT.Interface;

namespace Zip.Sat.Service.Controllers
{
    public class NotaFiscalController : IController
    {
        private SqlConnection Conn { get; }
        Random rdn = new Random();

        /* Aqui temos uma SqlConnection fictícia
         * para demonstrar a Injeção de Dependencia
         * provida pelo framework
         * Não usaremos ela
         * */
        public NotaFiscalController(SqlConnection conn)
        {
            Conn = conn;
        }


        public ActionResult ConsultaNF(string chaveAcesso)
        {
            return ActionResult.Json(new OperationResult(null, 900, "Nota fiscal não encontrada"));

        }

        /* Este é um exemplo de action com
         * parametro de classe
         * */
        public ActionResult EmiteNF(ModelParametrosServiceViewModel parametros)
        {

            try
            {

                ISat sat = SatFactory.CreateSat(Global.ConfiguracaoInicial.SatMarca);
                var secao = rdn.Next(999999);
                var retorno = sat.IEnviarDadosVenda(secao, parametros.Value);

                return ActionResult.Json(new OperationResult(retorno, 600, "Sat enviado com sucesso."));
            }
            catch (Exception e)
            {

                return ActionResult.Json(new OperationResult("Erro ao tentar enviar CFe-SAT.\nEmiteNF: ", 600, e.Message));
            }
        }

        public ActionResult CancelarSat(string chave, string cnpjSoftwareHouse, string cnpjCpfDestinatario, string NumeroCaixa, string SignAC)
        {

            try
            {
                ISat sat = SatFactory.CreateSat(Global.ConfiguracaoInicial.SatMarca);
                var secao = rdn.Next(999999);
                var retorno = sat.ICancelarUltimaVenda(secao, chave, cnpjSoftwareHouse, cnpjCpfDestinatario, NumeroCaixa, SignAC);

                return ActionResult.Json(new OperationResult(retorno, 600, "Sat enviado com sucesso."));
            }
            catch (Exception e)
            {

                return ActionResult.Json(new OperationResult("Erro ao tentar enviar CFe-SAT.\n", 600, e.Message));
            }
        }

        private string GeraChaveAcessoFake()
        {
            int seed = DateTime.Now.Millisecond + DateTime.Now.Second;
            int chave = new Random(seed).Next();
            string result = $"NFe{chave}";
            if (result.Length > 15)
                result = result.Substring(0, 15);
            return result;
        }
    }
}
