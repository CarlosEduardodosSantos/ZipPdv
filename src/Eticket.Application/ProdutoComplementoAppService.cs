using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class ProdutoComplementoAppService : IProdutoComplementoAppService
    {
        private readonly IProdutoComplementoRepository _complementoRepository;

        public ProdutoComplementoAppService(IProdutoComplementoRepository complementoRepository)
        {
            _complementoRepository = complementoRepository;
        }

        public void Dispose()
        {
            _complementoRepository.Dispose();
        }

        public ProdutoComplementoViewModel GetById(int id)
        {
            return TypeAdapter.Adapt<ProdutoComplemento, ProdutoComplementoViewModel>(_complementoRepository.GetById(id));
        }

        public IEnumerable<ProdutoComplementoViewModel> ObterPorGrupoId(int grupoId)
        {
            return
                TypeAdapter.Adapt<IEnumerable<ProdutoComplemento>, IEnumerable<ProdutoComplementoViewModel>>(
                    _complementoRepository.ObterPorGrupoId(grupoId));
        }
    }
}