using System.ComponentModel;

namespace Eticket.Application.ViewModels
{
    public enum TipoImpressaoViewEnum
    {
        [Description("PopUp")]
        PopUp = 1,
        [Description("Print")]
        Print = 2,
        [Description("Design")]
        Design = 3
    }
}