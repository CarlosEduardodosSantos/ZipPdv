using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface ITributacaoFiscalRepository : IRepositoryBase<ProdutoTributacao>
    {
        void Adicionar(ProdutoTributacao tributacaoFiscal);
        void Editar(ProdutoTributacao tributacaoFiscal);
        void Excluir(ProdutoTributacao tributacaoFiscal);
    }
}