using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;
using System.Collections.Generic;

namespace Eticket.Application
{
    public class ConfiguracaoSistemaAppService : IConfiguracaoSistemaAppService
    {
        private readonly IConfiguracaoSistemaRepository _configuracaoSistemaRepository;

        public ConfiguracaoSistemaAppService(IConfiguracaoSistemaRepository configuracaoSistemaRepository)
        {
            _configuracaoSistemaRepository = configuracaoSistemaRepository;
        }

        public void Dispose()
        {
            _configuracaoSistemaRepository.Dispose();
        }

        public ConfiguracaoSistemaViewModel ObterPorVariavel(string variavel)
        {
            return TypeAdapter.Adapt<ConfiguracaoSistema, ConfiguracaoSistemaViewModel>(_configuracaoSistemaRepository.ObterByValiavel(variavel));
        }

        public IEnumerable<ConfiguracaoSistemaViewModel> ObterTodos()
        {
            return TypeAdapter.Adapt<IEnumerable<ConfiguracaoSistema>, IEnumerable<ConfiguracaoSistemaViewModel>>(_configuracaoSistemaRepository.GetAll());
        }
    }
}
