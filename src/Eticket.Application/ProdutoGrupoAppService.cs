using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class ProdutoGrupoAppService : IProdutoGrupoAppService
    {
        private readonly IProdutoGrupoRepository _produtoGrupoRepository;

        public ProdutoGrupoAppService(IProdutoGrupoRepository produtoGrupoRepository)
        {
            _produtoGrupoRepository = produtoGrupoRepository;
        }

        public ProdutoGrupoViewModel ObterPorId(int id)
        {
            return TypeAdapter.Adapt<ProdutoGrupo, ProdutoGrupoViewModel>(_produtoGrupoRepository.GetById(id));
        }

        public IEnumerable<ProdutoGrupoViewModel> ObterTodos(int lojaId)
        {
            return TypeAdapter.Adapt<IEnumerable<ProdutoGrupo>, IEnumerable<ProdutoGrupoViewModel>>(_produtoGrupoRepository.GetAll(lojaId));
        }

        public void Dispose()
        {
            _produtoGrupoRepository.Dispose();
        }
    }
}