using Eticket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Domain.Interface.Repository
{
    public interface IOpMesa1Repository : IRepositoryBase<OpMesa1>
    {

        int Abrir(OpMesa1 opmesa);

        void Atualizar(OpMesa1 opmesa);

        void Transferir(OpMesa1 opmesa);

        OpMesa1 GetById(int id);

        void Pagamento(OpMesa1 opmesa);
    }
}
