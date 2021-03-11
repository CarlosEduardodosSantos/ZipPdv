using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioAppService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
        }

        public UsuarioViewModel GetById(int id)
        {
            return TypeAdapter.Adapt<Usuario, UsuarioViewModel>(_usuarioRepository.GetById(id));
        }

        public IEnumerable<UsuarioViewModel> GetAll()
        {
            return TypeAdapter.Adapt<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(_usuarioRepository.GetAll());
        }

        public UsuarioViewModel GetAutenticacao(string senha)
        {
            return TypeAdapter.Adapt<Usuario, UsuarioViewModel>(_usuarioRepository.GetAutenticacao(senha));
        }
    }
}