namespace Eticket.Application.ViewModels
{
    public class DescontoViewResult
    {
        public bool ResultOk { get; set; }
        public decimal ValorPercentual { get; set; }
        public decimal ValorReal { get; set; }
        public bool ForceDesc { get; set; }
    }
}