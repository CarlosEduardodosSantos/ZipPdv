using System;
using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IVendaPendenteRepository : IDisposable
    {
        void Add(VendaPendente venda);
        void Remover(int Nro);
        void NotificarPronto(int nro);
        IEnumerable<VendaPendente> ObterPorNome(string nome);
        IEnumerable<VendaPendente> ObterTodos();
        int ObterUltimaSequencia(string nome);
        int VendaId();
  
        int PendenciaExistente(string nome);
        bool GeraImpressaoFechamento(int pendenciaId, int tipoOperacao);
        bool GeraImpressaoItem(int pendenciaId, int tipoOperacao);
    }
}