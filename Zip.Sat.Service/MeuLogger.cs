using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MobileAppServer.ServerObjects;

namespace Zip.Sat.Service
{
    public class MeuLogger : ILoggerWrapper
    {
        public List<ServerLog> List(Expression<Func<ServerLog, bool>> query)
        {
            /**
             * Aqui voce pode retornar (caso queira) os logs
             * que voce pode persistir em algum repositorio
             * (como arquivo Sqlite ou mesmo json txt)
             * */
            return new List<ServerLog>();
        }

        public void Register(ServerLog log)
        {
            /* Tudo que acontece no servidor
             * vai chegar aqui
             * Voce pode usar isso para gravar em algum
             * repositorio de dados, como arquivo sqlite,
             * seu banco de dados ou mesmo um arquivo json txt
             * */
            Program.WriteToFile(String.Format("{0}: " + log.LogText, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
        }
    }
}
