using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.Interface
{
    public interface IOpMesa1AppService :IDisposable
    {
        int Abrir(OpMesa1ViewModel OpMesa1ItemView);

        void Atualizar(OpMesa1ViewModel opmesa);

        void Transferir(OpMesa1ViewModel opmesa);

        void Pagamento(OpMesa1ViewModel opmesa);

        OpMesa1ViewModel GetById(int id);
    }
}
