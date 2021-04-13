using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;
using System.Collections.Generic;

namespace Eticket.Application
{
    public class VendaPendenteAppService : IVendaPendenteAppService
    {
        private readonly IVendaPendenteRepository _vendaPendenteRepository;

        public VendaPendenteAppService(IVendaPendenteRepository vendaPendenteRepository)
        {
            _vendaPendenteRepository = vendaPendenteRepository;
        }

        public void Add(VendaViewModel vendaView)
        {
            var venda = TypeAdapter.Adapt<VendaViewModel, Venda>(vendaView);
            _vendaPendenteRepository.Add(venda);
        }

        public void Dispose()
        {
            _vendaPendenteRepository.Dispose();
        }

        public void ImprimePendencia(string pendenciaId)
        {
            _vendaPendenteRepository.ImprimeFichaGr(pendenciaId);
        }

        public IEnumerable<VendaViewModel> ObterPorNome(string nome)
        {
            throw new System.NotImplementedException();
        }

        public int ObterUltimaSequencia(string nome)
        {
            throw new System.NotImplementedException();
        }

        public bool PendenciaExistente(string nome)
        {
            return _vendaPendenteRepository.PendenciaExistente(nome);
        }

        public void Remover(VendaViewModel vendaView)
        {
            var venda = TypeAdapter.Adapt<VendaViewModel, Venda>(vendaView);
            _vendaPendenteRepository.Remover(venda);
        }
    }
}
