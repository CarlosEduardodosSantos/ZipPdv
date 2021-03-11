using System;

namespace Eticket.Domain.Entity
{
    public class FabricaRelatorio
    {
        public FabricaRelatorio()
        {
            IdGuid = new Guid();
        }
        public Guid IdGuid { get; set; }
        public int UduarioId { get; set; }
        public DateTime DataHora { get; set; }
        public string FileName { get; set; }
        public string File { get; set; }
        public int Tipo { get; set; }

    }
}