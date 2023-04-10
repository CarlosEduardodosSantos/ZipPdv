using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;
using System;
using System.Collections.Generic;

namespace Eticket.Application
{
    public class CadGarcomAppService : ICadGarcomAppService
    {
        private readonly ICadGarcomRepository _garcomRepository;

        public CadGarcomAppService(ICadGarcomRepository mesaRepository)
        {
            _garcomRepository = mesaRepository;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public CadGarcomViewModel GetById(int id)
        {
            return TypeAdapter.Adapt<CadGarcom, CadGarcomViewModel>(_garcomRepository.GetById(id));
        }

        public IEnumerable<CadGarcomViewModel> ObterGarcons()
        {
            return
                TypeAdapter.Adapt<IEnumerable<CadGarcom>, IEnumerable<CadGarcomViewModel>>(
                    _garcomRepository.ObterGarcons());
        }
    }
}
