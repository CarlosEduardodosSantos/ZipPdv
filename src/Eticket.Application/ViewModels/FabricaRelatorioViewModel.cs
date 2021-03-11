using System;

namespace Eticket.Application.ViewModels
{
    public class FabricaRelatorioViewModel
    {
        public FabricaRelatorioViewModel()
        {
            IdGuid = Guid.NewGuid();
        }
        public Guid IdGuid { get; set; }
        public int UduarioId { get; set; }
        public DateTime DataHora { get; set; }
        public string FileName { get; set; }
        public string File { get; set; }
        public FabricaRelatorioTipoViewEnum Tipo { get; set; }
    }
}