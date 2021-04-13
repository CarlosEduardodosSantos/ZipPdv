using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IConfiguracaoSistemaRepository : IRepositoryBase<ConfiguracaoSistema>
    {
        ConfiguracaoSistema ObterByValiavel(string variavel);
    }
}
