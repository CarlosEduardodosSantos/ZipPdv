using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface INfceServicoRepository
    {
        NFce GravaNfce(int vendaId);
        string GeraNFce(int nfceId);
        NfceSituacao ConsultaSituacao(int nfceId, int serie, int modelo, string cnpjEmpresa);
        string ObterDiretorioNfce(int empresaId);
    }
}