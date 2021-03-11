using System.Data.SqlClient;
using MobileAppServer.ServerObjects;

namespace Zip.Sat.Service.InjecaoDependencia
{
    public class InjetorDeConexaoComBanco : IDependencyInjectorMaker
    {
        /* Devemos informar aqui qual o Controller
         * no qual essa injeção se aplica
         * Caso seja um controller específico, informamos
         * o nome dele (nome da classe apenas)
         * 
         * Caso se aplique a TODOS os controllers no servidor,
         * retornamos ""
         * */
        public string ControllerName { get { return ""; } } //todos os controllers

        public object[] BuildInjectValues(RequestBody body)
        {
            /*
             * Aqui devemos retornar um object[] com os
             * dados solicitados no construtor do controller
             * 
             * A ordem e os tipos de objetes devem obedecer
             * a ordem definida no construtor do controller
             * */
            return new object[]{
                new SqlConnection()
            };
        }
    }
}
