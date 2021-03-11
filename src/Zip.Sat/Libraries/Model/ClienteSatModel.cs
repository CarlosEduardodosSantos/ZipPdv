using System;

namespace Zip.Sat.Libraries.Model
{
    public class ClienteSatModel
    {
        public ClienteSatModel()
        { }

        public long ClienteID { get; set; }
        public string Nome { get; set; }
        public int IDTipoCliente { get; set; }
        public string Fone1 { get; set; }
        public string Fone2 { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Observacao { get; set; }
        public string Observacao2 { get; set; }
        public string Fone3 { get; set; }
        public string Empresa { get; set; }
        public string FoneEmpresa { get; set; }
        public decimal Salario { get; set; }
        public DateTime DataAdmissao { get; set; }
        public string Funcao { get; set; }
        public string EstadoCivil { get; set; }
        public string Conjuge { get; set; }
        public string Ref1 { get; set; }
        public string FoneRef1 { get; set; }
        public string Ref2 { get; set; }
        public string FoneRef2 { get; set; }
        public string Ref3 { get; set; }
        public string FoneRef3 { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public decimal LimiteDeCredito { get; set; }
    }
}