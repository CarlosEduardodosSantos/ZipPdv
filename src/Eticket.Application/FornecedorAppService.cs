using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class FornecedorAppService : IFornecedorAppService
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorAppService(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public void Dispose()
        {
            _fornecedorRepository.Dispose();
        }

        public void Adicionar(FornecedorViewModel fornecedorView)
        {
            var fornecedor = TypeAdapter.Adapt<FornecedorViewModel, Fornecedor>(fornecedorView);
            _fornecedorRepository.Adicionar(fornecedor);
        }

        public void Editar(FornecedorViewModel fornecedorView)
        {
            var fornecedor = TypeAdapter.Adapt<FornecedorViewModel, Fornecedor>(fornecedorView);
            _fornecedorRepository.Editar(fornecedor);
        }

        public void Excluir(FornecedorViewModel fornecedorView)
        {
            var fornecedor = TypeAdapter.Adapt<FornecedorViewModel, Fornecedor>(fornecedorView);
            _fornecedorRepository.Excluir(fornecedor);
        }

        public FornecedorViewModel ObterPorId(int id)
        {
            return TypeAdapter.Adapt<Fornecedor, FornecedorViewModel>(_fornecedorRepository.GetById(id));
        }

        public IEnumerable<FornecedorViewModel> ObterTodos()
        {
            return TypeAdapter.Adapt<IEnumerable<Fornecedor>, IEnumerable<FornecedorViewModel>>(_fornecedorRepository
                .GetAll());
        }
    }
}