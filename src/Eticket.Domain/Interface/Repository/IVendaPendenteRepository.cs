using System;
using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IVendaPendenteRepository : IDisposable
    {
        void Add(Venda venda);
        void Remover(Venda venda);
        IEnumerable<Venda> ObterPorNome(string nome);
        int ObterUltimaSequencia(string nome);
        void ImprimeFichaGr(string pendenciaId);
        bool PendenciaExistente(string nome);
    }
}