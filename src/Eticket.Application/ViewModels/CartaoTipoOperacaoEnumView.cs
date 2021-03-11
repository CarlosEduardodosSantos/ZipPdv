namespace Eticket.Application.ViewModels
{
    public enum CartaoTipoOperacaoEnumView
    {
        /// <summary>
        /// Verifica se o Gerenciador Padrão está ativo 
        /// </summary>
        ATV = 1,
        /// <summary>
        ///  Administrativa 
        /// </summary>
        ADM = 2,
        /// <summary>
        /// Cheque 
        /// </summary>
        CHQ = 3,
        /// <summary>
        /// Cartão
        /// </summary>
        CRT = 4,
        /// <summary>
        ///  Recarga de Celular 
        /// </summary>
        CEL = 5,
        /// <summary>
        ///  Cancelamento 
        /// </summary>
        CNC = 6,
        /// <summary>
        /// Confirmação de finalização da venda e impressão do cupom 
        /// </summary>
        CNF = 7,
        /// <summary>
        /// Não confirmação de finalização da venda por desistência do cliente ou erro na impressão 
        /// </summary>
        NCN = 8
    }
}