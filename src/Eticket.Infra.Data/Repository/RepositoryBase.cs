using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Eticket.Infra.Data.Repository
{
    public class RepositoryBase : IDisposable
    {
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["MyContext"].ConnectionString);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}