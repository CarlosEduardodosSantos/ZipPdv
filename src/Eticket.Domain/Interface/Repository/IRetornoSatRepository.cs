using System;
using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IRetornoSatRepository : IRepositoryBase<RetornoSat>
    {
        void Adicionar(RetornoSat retornoSat);
        RetornoSat ObterPorVendaId(string vendaId);
        IEnumerable<RetornoSat> ObterPorData(DateTime dataInicio, DateTime dataFinal);

    }
}