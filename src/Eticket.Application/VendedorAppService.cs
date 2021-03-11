using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class VendedorAppService : IVendedorAppService
    {
        private readonly IVendedorRepository _vendedorRepository;

        public VendedorAppService(IVendedorRepository vendedorRepository)
        {
            _vendedorRepository = vendedorRepository;
        }

        public void Dispose()
        {
            _vendedorRepository.Dispose();
        }

        public VendedorViewModel GetById(int id)
        {
            return TypeAdapter.Adapt<Vendedor, VendedorViewModel>(_vendedorRepository.GetById(id));
        }

        public IEnumerable<VendedorViewModel> GetAll()
        {
            return TypeAdapter.Adapt<IEnumerable<Vendedor>, IEnumerable<VendedorViewModel>>(_vendedorRepository.GetAll());
        }

        public VendedorViewModel GetAutenticacao(string senha)
        {
            return TypeAdapter.Adapt<Vendedor, VendedorViewModel>(_vendedorRepository.GetAutenticacao(senha));
        }
    }
}