using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class VendedorRepository : RepositoryBase, IVendedorRepository
    {
        public Vendedor GetById(int id)
        {
            using (var cn = Connection)
            {
                var sql = "Select  " +
                          "IdGarcom as VendedorId, " +
                          "Descricao as Nome, " +
                          "Senha " +
                          "from CadGarcom  Where IdGarcom = @id";

                cn.Open();
                var vendedor = cn.Query<Vendedor>(sql, new { id }).FirstOrDefault();
                cn.Close();

                return vendedor;
            }
        }

        public IEnumerable<Vendedor> GetAll()
        {
            using (var cn = Connection)
            {
                var sql = "Select  " +
                          "IdGarcom as VendedorId, " +
                          "Descricao as Nome, " +
                          "Senha " +
                          "from CadGarcom Where Status = 'A'";

                cn.Open();
                var vendedores = cn.Query<Vendedor>(sql, new {  });
                cn.Close();

                return vendedores;
            }
        }

        public Vendedor GetAutenticacao(string senha)
        {
            using (var cn = Connection)
            {
                var sql = "Select  " +
                          "IdGarcom as VendedorId, " +
                          "Descricao as Nome, " +
                          "Senha " +
                          "from CadGarcom  Where Senha = @senha";

                cn.Open();
                var vendedor = cn.Query<Vendedor>(sql, new { senha }).FirstOrDefault();
                cn.Close();

                return vendedor;
            }
        }
    }
}