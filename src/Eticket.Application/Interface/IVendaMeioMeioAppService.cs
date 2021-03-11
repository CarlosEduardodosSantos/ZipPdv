using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IVendaMeioMeioAppService : IDisposable
    {
        void Adicionar(VendaMeioMeioViewModel vendaMeioMeioView);
        void Remover(VendaMeioMeioViewModel vendaMeioMeioView);
        VendaMeioMeioViewModel GetById(int id);
        IEnumerable<VendaMeioMeioViewModel> GetByOperacaoId(int operacaoId, string operacaoTipo);
    }
}