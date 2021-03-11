using System;

namespace Zip.Sat.Libraries.Model
{
    public class VendaSatModel
    {
        public VendaSatModel()
        { }

        ClienteSatModel cliente;
        public ClienteSatModel Cliente
        {
            get
            {
                if (cliente == null)
                    cliente = new ClienteSatModel();
                return cliente;
            }
            set { cliente = value; }
        }

        VendaItemCollection itens;
        public VendaItemCollection Itens
        {
            get
            {
                if (itens == null)
                    itens = new VendaItemCollection();
                return itens;
            }
        }

        VendaFinalizadoraItemCollection finalizadoras;
        public VendaFinalizadoraItemCollection Finalizadoras
        {
            get
            {
                if (finalizadoras == null)
                    finalizadoras = new VendaFinalizadoraItemCollection();
                return finalizadoras;
            }
        }

        public long VendaID { get; set; }
        public int EmpresaId { get; set; }
        public int Pdv { get; set; }
        public DateTime DataVenda { get; set; }
        public int ClienteID { get; set; }
        public int Tipo { get; set; }
        public int OperadorID { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorFinal { get; set; }
        public bool Entrada { get; set; }
        public int CupomFiscal { get; set; }
        public int NrCaixa { get; set; }
        public string FormaPagto { get; set; }
        public decimal TotalImpostoFederal { get; set; }
        public decimal TotalImpostoEstadual { get; set; }
        public DateTime DtEntregaCondicional { get; set; }
        public bool BaixaCondicional { get; set; }
        public string ChaveEletronicaCFeSATNFce { get; set; }
        public string Modelo { get; set; }
        public DateTime DataCompleta { get; set; }
        public string ChaveEletronicaCFeSATNFceCanc { get; set; }
        public int CfeSatNumeroNf { get; set; }
        public string CfeSatNumeroExtrato { get; set; }
        public int EmpresaID { get; set; }
        public int Comanda { get; set; }
        public string ClienteNome { get { return (string.IsNullOrEmpty(Cliente.Nome) ? "" : Cliente.Nome); } }
        public string ClienteCpf { get { return (string.IsNullOrEmpty(Cliente.Cpf) ? "" : Cliente.Cpf); } }
    }
}