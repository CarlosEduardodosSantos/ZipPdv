using System;
using System.Collections.Generic;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Domain.Interface
{
    public interface IVendaRepository : IRepositoryBase<Venda>
    {
        void Adicionar(Venda venda);
        void AtualizaFiscal(Venda venda);
        void Cancelar(Venda venda);
        int VendaId();
        bool GeraImpressaoItens(int vendaId, int tipoOperacao);
        bool GeraImpressaoFechamento(int vendaId, int tipoOperacao);
        IEnumerable<Venda> ObterEntregaPendentes();
        IEnumerable<Venda> ObterData(DateTime dataInicio, DateTime dataFinal);
        IEnumerable<Venda> ObterPendenteSat(DateTime dataInicio, DateTime dataFinal, int pdv);
        IEnumerable<Venda> ObterNroSat(string nroSat);
        Venda ObterPorId(int vendaId);
    }
}