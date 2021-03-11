using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IRetornoSatAppService : IDisposable
    {
        void Adicionar(RetornoSatViewModel retornoSatView);
        RetornoSatViewModel ObterPorVendaId(string vendaId);
        IEnumerable<RetornoSatViewModel> ObterPorData(DateTime dataInicio, DateTime dataFinal);}
}