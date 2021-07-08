using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IVendaAppService : IDisposable
    {
        void Adicionar(VendaViewModel vendaView);
        void AtualizaFiscal(VendaViewModel vendaView);
        void Cancelar(VendaViewModel vendaView);
        VendaViewModel ObterPorId(int id);
        IEnumerable<VendaViewModel> ObterEntregaPendentes();
        IEnumerable<VendaViewModel> ObterEntregaAguardandoRetorno();
        IEnumerable<VendaViewModel> ObterPorData(DateTime dataInicio, DateTime dataFinal);
        IEnumerable<VendaViewModel> ObterPendenteSat(DateTime dataInicio, DateTime dataFinal, int pdv);
        IEnumerable<VendaViewModel> ObterNroSat(string nroSat);
        int ObterVendaId();
        bool GeraImpressaoFechamento(int vendaId, int tipoOperacao);
        bool GeraImpressaoItens(int vendaId, int tipoOperacao);
    }
}