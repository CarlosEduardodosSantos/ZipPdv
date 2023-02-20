using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using System.Collections.Generic;
using System;

namespace Eticket.Application.Interface
{
    public interface INfceServicoAppService : IDisposable
    {
        NFceViewModel GravaNfce(int vendaId);
        string GeraNFce(int nfceId);
        NfceSituacaoViewModel ConsultaSituacao(int nfceId, int serie, int modelo, string cnpjEmpresa);
        string ObterDiretorioNfce(int empresaId);
        IEnumerable<NFceViewModel> NfceNãoEnviadas(DateTime datahora);
    }
}