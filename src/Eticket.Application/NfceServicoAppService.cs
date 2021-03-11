using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class NfceServicoAppService : INfceServicoAppService
    {
        private readonly INfceServicoRepository _nfceServicoRepository;

        public NfceServicoAppService(INfceServicoRepository nfceServicoRepository)
        {
            _nfceServicoRepository = nfceServicoRepository;
        }

        public NFceViewModel GravaNfce(int vendaId)
        {
            return TypeAdapter.Adapt<NFce, NFceViewModel>(_nfceServicoRepository.GravaNfce(vendaId));
        }

        public string GeraNFce(int nfceId)
        {
            return _nfceServicoRepository.GeraNFce(nfceId);
        }

        public NfceSituacaoViewModel ConsultaSituacao(int nfceId, int serie, int modelo, string cnpjEmpresa)
        {
            return TypeAdapter.Adapt<NfceSituacao, NfceSituacaoViewModel>(
                _nfceServicoRepository.ConsultaSituacao(nfceId, serie, modelo, cnpjEmpresa));
        }

        public string ObterDiretorioNfce(int empresaId)
        {
            return _nfceServicoRepository.ObterDiretorioNfce(empresaId);
        }
    }
}