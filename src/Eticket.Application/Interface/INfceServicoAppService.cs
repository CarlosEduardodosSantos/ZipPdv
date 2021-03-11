using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface INfceServicoAppService
    {
        NFceViewModel GravaNfce(int vendaId);
        string GeraNFce(int nfceId);
        NfceSituacaoViewModel ConsultaSituacao(int nfceId, int serie, int modelo, string cnpjEmpresa);
        string ObterDiretorioNfce(int empresaId);
    }
}