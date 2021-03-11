namespace Eticket.Application.ViewModels
{
    public class EmpresaViewModel
    {
        public int EmpresaId { get; set; }
        public string RazaoSocial { get; set; }
        public string Fantasia { get; set; }
        public string Cnpj { get; set; }
        public string Ie { get; set; }
        public string Im { get; set; }
        public string Cep { get; set; }
        public string LogradouroTipo { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string CidadeIbge { get; set; }
        public string UfIbge { get; set; }
        public string Fone1 { get; set; }
        public string Fone2 { get; set; }
        public string Fone3 { get; set; }
        public string HomePage { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public string SignAC { get; set; }
        public int Crt { get; set; }
        public string SoftwareHouseCnpj { get; set; }
        public string SoftwareHouseChaveAtivacao { get; set; }
        private EmpresaConfiguracaoViewModel _empresaConfiguracao;
        public EmpresaConfiguracaoViewModel EmpresaConfiguracao
        {
            get
            {
                if (_empresaConfiguracao == null)
                    _empresaConfiguracao = new EmpresaConfiguracaoViewModel();
                return _empresaConfiguracao;
            }
            set { _empresaConfiguracao = value; }
        }
    }
}