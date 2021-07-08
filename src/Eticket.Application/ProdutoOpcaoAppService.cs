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
    public class ProdutoOpcaoAppService : IProdutoOpcaoAppService
    {
        private readonly IProdutoOpcaoRepository _produtoOpcaoRepository;

        public ProdutoOpcaoAppService(IProdutoOpcaoRepository produtoOpcaoRepository)
        {
            _produtoOpcaoRepository = produtoOpcaoRepository;
        }

        public void Alterar(ProdutoOpcaoViewModel produtoOpcaoViewModel)
        {
            var produtoOpcao = TypeAdapter.Adapt<ProdutoOpcaoViewModel, ProdutoOpcao>(produtoOpcaoViewModel);
            _produtoOpcaoRepository.Alterar(produtoOpcao);
        }

        public void AlterarTipo(ProdutoOpcaoTipoVewModel produtoOpcaoTipoViewModel)
        {
            var produtoOpcaoTipo = TypeAdapter.Adapt<ProdutoOpcaoTipoVewModel, ProdutoOpcaoTipo>(produtoOpcaoTipoViewModel);
            _produtoOpcaoRepository.AlterarTipo(produtoOpcaoTipo);
        }

        public void Deletar(string id)
        {
            throw new NotImplementedException();
        }

        public void DeletarRelacao(string produtosOpcaoId, int produtoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProdutoOpcaoTipoVewModel> GetByprodutoId(int produtoId)
        {
            return TypeAdapter.Adapt<IEnumerable<ProdutoOpcaoTipo>, IEnumerable<ProdutoOpcaoTipoVewModel>>(
                _produtoOpcaoRepository.GetByprodutoId(produtoId));
        }

        public void Insert(ProdutoOpcaoViewModel produtoOpcaoViewModel)
        {
            throw new NotImplementedException();
        }

        public void InsertTipo(ProdutoOpcaoTipoVewModel produtoOpcaoTipoViewModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> ObterProdutoOpcaoRelacao(int produtosOpcaoTipoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProdutoOpcaoTipoVewModel> ObterTipoAll()
        {
            throw new NotImplementedException();
        }

        public void Relacionar(ProdutosOpcaoTipoRelacaoViewModel produtosOpcaoTipoRelacaoViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
