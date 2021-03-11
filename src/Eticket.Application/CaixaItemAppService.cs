using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class CaixaItemAppService : ICaixaItemAppService
    {
        private readonly ICaixaItemRepository _caixaItemRepository;

        public CaixaItemAppService(ICaixaItemRepository caixaItemRepository)
        {
            _caixaItemRepository = caixaItemRepository;
        }

        public void Dispose()
        {
            _caixaItemRepository.Dispose();
        }

        public void Adicionar(CaixaItemViewModel caixaItemView)
        {
            var caixaItem = TypeAdapter.Adapt<CaixaItemViewModel, CaixaItem>(caixaItemView);
            _caixaItemRepository.Adicionar(caixaItem);
        }

        public IEnumerable<CaixaItemViewModel> ObterPorCaixaId(int caixaId)
        {
            return TypeAdapter.Adapt<IEnumerable<CaixaItem>, IEnumerable<CaixaItemViewModel>>(_caixaItemRepository
                .ObterPorCaixaId(caixaId));
        }
    }
}