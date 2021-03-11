using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface;
using FastMapper;

namespace Eticket.Application
{
    public class ProdutoTipoAppService : IProdutoTipoAppService
    {
        private readonly IProdutoTipoRepository _produtoTipoRepository;

        public ProdutoTipoAppService(IProdutoTipoRepository produtoTipoRepository)
        {
            _produtoTipoRepository = produtoTipoRepository;
        }

        public void Dispose()
        {
            _produtoTipoRepository.Dispose();
        }

        public ProdutoTipoViewModel ObterPorId(int id)
        {
            return TypeAdapter.Adapt<ProdutoTipo, ProdutoTipoViewModel>(_produtoTipoRepository.GetById(id));
        }

        public IEnumerable<ProdutoTipoViewModel> ObterTodos()
        {
            return TypeAdapter.Adapt<IEnumerable<ProdutoTipo>, IEnumerable<ProdutoTipoViewModel>>(_produtoTipoRepository
                .GetAll());
        }
    }
}