using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IUsuarioAppService : IDisposable
    {
        UsuarioViewModel GetById(int id);
        IEnumerable<UsuarioViewModel> GetAll();
        UsuarioViewModel GetAutenticacao(string senha);
    }
}