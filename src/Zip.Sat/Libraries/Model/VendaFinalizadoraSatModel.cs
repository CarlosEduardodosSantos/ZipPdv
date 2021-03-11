using System.Collections.ObjectModel;

namespace Zip.Sat.Libraries.Model
{
    public class VendaFinalizadoraItemCollection : Collection<VendaFinalizadoraSatModel>
    { }
    public class VendaFinalizadoraSatModel
    {
        public VendaFinalizadoraSatModel()
        {
            CodigoFinalizadora = "";
        }

        public long VendaID { get; set; }
        public string CodigoFinalizadora { get; set; }
        public decimal Valor { get; set; }
    }
}
