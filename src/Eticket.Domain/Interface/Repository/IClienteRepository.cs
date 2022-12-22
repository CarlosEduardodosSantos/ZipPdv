using Eticket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Domain.Interface.Repository
{
    public interface IClienteRepository : IRepositoryBase<Cliente>
    {
        IEnumerable<Cliente>  ObterPorCodigo(int codigo);
        IEnumerable<Cliente> ObterPorNome(string nome);
    }
}
