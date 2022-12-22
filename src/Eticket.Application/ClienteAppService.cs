using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteAppService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public void Dispose()
        {
            _clienteRepository.Dispose();
        }

        public IEnumerable<ClienteViewModel> ObterPorCodigo(int codigo)
        {
            return TypeAdapter.Adapt<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(
                 _clienteRepository.ObterPorCodigo(codigo));
        }

        public IEnumerable<ClienteViewModel> ObterPorNome(string nome)
        {
            return TypeAdapter.Adapt<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(
                 _clienteRepository.ObterPorNome(nome));
        }
    }
}
