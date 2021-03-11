using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class FichaAppService : IFichaAppService
    {
        private readonly IFichaRepository _fichaRepository;

        public FichaAppService(IFichaRepository fichaRepository)
        {
            _fichaRepository = fichaRepository;
        }

        public void Dispose()
        {
            _fichaRepository.Dispose();
        }

        public FichaViewModel GetByFichaId(string fichaId)
        {
            return TypeAdapter.Adapt<Ficha, FichaViewModel>(_fichaRepository.GetByFichaId(fichaId));
        }

        public IEnumerable<FichaViewModel> GetAll()
        {
            return TypeAdapter.Adapt<IEnumerable<Ficha>, IEnumerable<FichaViewModel>>(_fichaRepository.GetAll());
        }
    }
}