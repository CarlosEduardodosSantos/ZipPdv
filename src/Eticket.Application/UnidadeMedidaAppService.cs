using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface;
using FastMapper;

namespace Eticket.Application
{
    public class UnidadeMedidaAppService : IUnidadeMedidaAppService
    {
        private readonly IUnidadeMedidaRepository _unidadeMedidaRepository;

        public UnidadeMedidaAppService(IUnidadeMedidaRepository unidadeMedidaRepository)
        {
            _unidadeMedidaRepository = unidadeMedidaRepository;
        }

        public void Dispose()
        {
            _unidadeMedidaRepository.Dispose();
        }

        public UnidadeMedidaViewModel ObterPorId(int id)
        {
            return TypeAdapter.Adapt<UnidadeMedida, UnidadeMedidaViewModel>(_unidadeMedidaRepository.GetById(id));
        }

        public IEnumerable<UnidadeMedidaViewModel> ObterTodos()
        {
            return TypeAdapter.Adapt<IEnumerable<UnidadeMedida>, IEnumerable<UnidadeMedidaViewModel>>(
                _unidadeMedidaRepository.GetAll());
        }

        public void Adicionar(UnidadeMedidaViewModel unidadeView)
        {
            var unidade = TypeAdapter.Adapt<UnidadeMedidaViewModel, UnidadeMedida>(unidadeView);
            _unidadeMedidaRepository.Adicionar(unidade);
        }

        public void Alterar(UnidadeMedidaViewModel unidadeView)
        {
            var unidade = TypeAdapter.Adapt<UnidadeMedidaViewModel, UnidadeMedida>(unidadeView);
            _unidadeMedidaRepository.Alterar(unidade);
        }

        public void Remover(UnidadeMedidaViewModel unidadeView)
        {
            var unidade = TypeAdapter.Adapt<UnidadeMedidaViewModel, UnidadeMedida>(unidadeView);
            _unidadeMedidaRepository.Remover(unidade);
        }
    }
}