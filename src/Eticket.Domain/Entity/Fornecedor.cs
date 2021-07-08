namespace Eticket.Domain.Entity
{
    public class Fornecedor
    {
        public int FornecedorId { get; set; }
        public int Situacao { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Cnpj { get; set; }
        public string Ie { get; set; }
        public string Email { get; set; }

    }
}