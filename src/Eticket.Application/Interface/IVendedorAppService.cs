using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IVendedorAppService : IDisposable
    {
        VendedorViewModel GetById(int id);
        IEnumerable<VendedorViewModel> GetAll();
        VendedorViewModel GetAutenticacao(string senha);
    }
}