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

        public void Add(VendaPendenteViewModel vendaView)
        {
            var venda = TypeAdapter.Adapt<VendaPendenteViewModel, VendaPendente>(vendaView);
            _vendaPendenteRepository.Add(venda);
        }

        public void Dispose()
        {
            _vendaPendenteRepository.Dispose();
        }

        public IEnumerable<VendaPendenteViewModel> ObterPorNome(string nome)
        {
            return TypeAdapter.Adapt<IEnumerable<VendaPendente>, IEnumerable<VendaPendenteViewModel>>(_vendaPendenteRepository.ObterPorNome(nome));
        }

        public IEnumerable<VendaPendenteViewModel> ObterTodos()
        {
            return TypeAdapter.Adapt<IEnumerable<VendaPendente>, IEnumerable<VendaPendenteViewModel>>(_vendaPendenteRepository.ObterTodos());
        }

        public int ObterUltimaSequencia(string nome)
        {
            throw new System.NotImplementedException();
        }

        public int ObterUltimoNro()
        {
            return _vendaPendenteRepository.VendaId();
        }

        public int PendenciaExistente(string nome)
        {
            return _vendaPendenteRepository.PendenciaExistente(nome);
        }

        public void Remover(int nro)
        { 
            _vendaPendenteRepository.Remover(nro);
        }
        public bool GeraImpressaoFechamento(int pendenciaId, int tipoOperacao)
        {
            return _vendaPendenteRepository.GeraImpressaoFechamento(pendenciaId, tipoOperacao);
        }

        public void NotificarPronto(int nro)
        {
            _vendaPendenteRepository.NotificarPronto(nro);
        }

        public bool GeraImpressaoItem(int pendenciaId, int tipoOperacao)
        {
            return _vendaPendenteRepository.GeraImpressaoItem(pendenciaId, tipoOperacao);
        }
    }
}
