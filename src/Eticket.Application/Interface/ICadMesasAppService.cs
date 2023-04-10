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
    public interface ICadMesasAppService : IDisposable
    {
    
        IEnumerable<CadMesasViewModel> ObterMesas();

        IEnumerable<CadMesasViewModel> ObterMesasDisponiveis();

        CadMesasViewModel GetById(int id);

        void AlterarStatusMesa(CadMesasViewModel mesa);

        void IncluirOpMesa1(CadMesasViewModel mesa);
    }
}
