using System;
using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IUsuarioRepository : IDisposable
    {
        Usuario GetById(int id);
        IEnumerable<Usuario> GetAll();
        Usuario GetAutenticacao(string senha);
    }
}