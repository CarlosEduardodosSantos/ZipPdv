using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class EntregadorAppService : IEntregadorAppService
    {
        private readonly IEntregadorRepository _entregadorRepository;

        public EntregadorAppService(IEntregadorRepository entregadorRepository)
        {
            _entregadorRepository = entregadorRepository;
        }

        public void Dispose()
        {
            _entregadorRepository.Dispose();
        }

        public EntregadorViewModel ObterPorId(int entregadorId)
        {
            return TypeAdapter.Adapt<Entregador, EntregadorViewModel>(_entregadorRepository.GetById(entregadorId));
        }

        public IEnumerable<EntregadorViewModel> ObterTodos()
        {
            return TypeAdapter.Adapt<IEnumerable<Entregador>, IEnumerable<EntregadorViewModel>>(_entregadorRepository
                .GetAll());
        }
    }
}