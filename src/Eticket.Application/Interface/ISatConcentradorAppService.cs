using System;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface ISatConcentradorAppService
    {
        void Adicionar(SatConcentradorViewModel satConcentradorView);
        void Alterar(SatConcentradorViewModel satConcentradorView);
        void Excluir(SatConcentradorViewModel satConcentradorView);
        SatConcentradorViewModel ObterPorId(Guid satConcentradorId);
        SatConcentradorViewModel ObterSatPendente(string pcName);

    }
}