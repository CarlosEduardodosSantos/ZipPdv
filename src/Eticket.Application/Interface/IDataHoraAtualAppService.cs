using Eticket.Application.ViewModels;
using System;

namespace Eticket.Application
{
    public interface IDataHoraAtualAppService : IDisposable
    {
        DataHoraAtualViewModel ConsultaDataHora();
    }
}
