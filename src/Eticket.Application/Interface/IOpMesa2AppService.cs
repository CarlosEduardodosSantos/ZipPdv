using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.Interface
{
    public interface IOpMesa2AppService: IDisposable
    {
        void Abrir(OpMesa2ViewModel OpMesa2ItemView);
        IEnumerable<OpMesa2ViewModel> PegarItens(int id);

        void DeletarItem(int id);

        void Bonificar(int id);
        void BonificarMesa(int id);

        void PagaItem(int id, string metodo);

        void EstornaItem(OpMesa2ViewModel opmesa);
    }
}
