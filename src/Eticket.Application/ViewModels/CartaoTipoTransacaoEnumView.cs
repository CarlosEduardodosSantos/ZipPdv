using System.ComponentModel;

namespace Eticket.Application.ViewModels
{
    public enum CartaoTipoTransacaoEnumView
    {
        [Description("00  Administrativas – Outras")]
        Administrativas = 00,
        [Description("Administrativa – Fechamento")]
        Administrativas1 = 01,
        [Description("Cartão de Crédito à Vista")]
        CartaoCreditoVista = 10,
        [Description("Cartão de Crédito Parcelado pelo Estabelecimento")]
        CartaoCreditoParceladoEstabelicimento = 11,
        [Description("Cartão de Crédito Parcelado pela Administradora")]
        CartaoCreditoParceladoAdministradora = 12,
        [Description("Pré-Autorização com Cartão de Crédito")]
        PreAutorizadoCartaoCredito = 13,
        [Description("Cartão de Débito à Vista")]
        CartaoDebitoVista = 20,
        [Description("Cartão de Débito Pré-Datado")]
        CartaoDebitoPreDatado = 21,
        [Description("Cartão de Débito Parcelada")]
        CartaoDebitoParcelado = 22,
        [Description("Cartão de Débito à Vista Forçada")]
        CartaoDebitoVistaForcada = 23,
        [Description("Cartão de Débito Pré-Datado Forçada")]
        CartaoDebitoParceladoForcada = 24,
        [Description("Cartão de Débito Pré-Datado sem Garantia")]
        CartaoDebitoPreDatadoSemGarantia = 25,
        [Description("Outros Cartões")]
        OutrosCartoes = 30,
        [Description("CDC ")]
        CDC = 40,
        [Description("Consulta CDC")]
        ConsultaCDC = 41,
        [Description("Convênio ")]
        Convênio = 50,
        [Description("Voucher")]
        Voucher = 60,
        [Description("Consulta Cheque")]
        ConsultaCheque = 70,
        [Description("Garantia de Cheque")]
        GarantiaCheque = 71,
        [Description("Outras")]
        Outras = 99
    }
}