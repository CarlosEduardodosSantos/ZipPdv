using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Eticket.Application.Interface
{
    public interface IVendaPendenteAppService : IDisposable
    {
        void Add(VendaPendenteViewModel venda);
        void Remover(int nro);
        void NotificarPronto(int nro);
        IEnumerable<VendaPendenteViewModel> ObterPorNome(string nome);
        IEnumerable<VendaPendenteViewModel> ObterTodos();
        int ObterUltimaSequencia(string nome);
        int ObterUltimoNro();
        int PendenciaExistente(string nome);
        bool GeraImpressaoFechamento(int pendenciaId, int tipoOperacao);
        bool GeraImpressaoItem(int pendenciaId, int tipoOperacao);
    }
}
