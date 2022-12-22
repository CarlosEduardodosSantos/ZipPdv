using Eticket.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace Eticket.Application.Interface
{
    public interface IConfiguracaoSistemaAppService : IDisposable
    {
        IEnumerable<ConfiguracaoSistemaViewModel> ObterTodos();
        ConfiguracaoSistemaViewModel ObterPorVariavel(string variavel);
        void ZerarSenha();
    }
}
