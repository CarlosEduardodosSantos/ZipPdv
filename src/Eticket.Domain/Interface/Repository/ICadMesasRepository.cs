using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface ICadMesasRepository: IRepositoryBase<CadMesas>
    {
        IEnumerable<CadMesas> ObterMesas();

        IEnumerable<CadMesas> ObterMesasDisponiveis();

        CadMesas GetById(int id);

        void AlterarStatusMesa(CadMesas cadmesas);

        void IncluirOpMesa1(CadMesas cadmesa);
    }
}
