using Eticket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Domain.Interface.Repository
{
    public interface IOpMesa2Repository : IRepositoryBase<OpMesa2>
    {


        void Abrir(OpMesa2 opmesa);

        IEnumerable<OpMesa2> PegarItens(int id);

        void DeletarItem(int id);

        void Bonificar(int id);

        void BonificarMesa(int id);
        void PagaItem(int id, string metodo);

        void EstornaItem(OpMesa2 opmesa);
    }
}
