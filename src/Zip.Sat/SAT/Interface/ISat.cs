namespace SAT.Interface
{
    public interface ISat
    {
        string IConsultarSAT(int NumeroSessao);
        string IConsultarStatusOperacional(int NumeroSessao);
        string IConsultarNumeroSessao(int NumeroSessao, int NumeroSessaoConsulta);
        string IEnviarDadosVenda(int NumeroSessao, string XML);
        string ICancelarUltimaVenda(int NumeroSessao, string chave, string cnpjSoftwareHouse, string cnpjCpfDestinatario, string NumeroCaixa, string SignAC);
    }
}