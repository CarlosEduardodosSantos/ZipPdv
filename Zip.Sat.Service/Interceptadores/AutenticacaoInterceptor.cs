using System;
using System.Linq;
using MobileAppServer.ServerObjects;

namespace Zip.Sat.Service.Interceptadores
{
    public class AutenticacaoInterceptor : IHandlerInterceptor
    {
        /* Aqui devemos informar para qual Controller
         * esse interceptor deve agir.
         * Deve-se retornar aqui o nome da classe do controller
         * Caso se aplique a TODOS os controllers, deve-se retornar ""
         * */
        public string ControllerName { get { return "NotaFiscalController"; } }

        /* Aqui devemos informar para qual Action
         * esse interceptor deve agir.
         * Mesmo padrão do ControllerName
         * */
        public string ActionName { get { return ""; } }

        private static string tokenFicticio = "token_exemplo";

        public InterceptorHandleResult PreHandle(SocketRequest socketRequest)
        {
            /* Aqui podemos executar qualquer
             * regra ou código ANTES da action no servidor
             * ser invocada
             * Podemos validar qualquer coisa, ou preparar qualquer coisa
             * e assim rejeitar ou aceitar a requisição
             * */

            /*var parametroToken = socketRequest.Parameters.FirstOrDefault(p => p.Name.Equals("tokenAutorizacao"));
            if (parametroToken == null)
                return new InterceptorHandleResult(true, true, "Token não informado", "");

            if(!parametroToken.Value.Equals(tokenFicticio))
                return new InterceptorHandleResult(true, true, "Não autorizado: token inválido", "");
*/
            Program.WriteToFile(String.Format("{0}: Logon Token Ok", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            return new InterceptorHandleResult(false, true, "Token ok", "");
        }
    }
}
