using System;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IEmpresaAppService : IDisposable
    {
        EmpresaViewModel ObterPorLoja(int lojaId);
    }
}