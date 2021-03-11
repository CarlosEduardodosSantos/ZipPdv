namespace Eticket.Domain.Entity
{
    public class Empresa
    {
        public int EmpresaId { get; set; }
        public string RazaoSocial { get; set; }
        public string Fantasia { get; set; }
        public string Cnpj { get; set; }
        public string Ie { get; set; }
        public string SignAC { get; set; }
        public string SoftwareHouseCnpj { get; set; }
        public string SoftwareHouseChaveAtivacao { get; set; }
        public int Crt { get; set; }
    }
}