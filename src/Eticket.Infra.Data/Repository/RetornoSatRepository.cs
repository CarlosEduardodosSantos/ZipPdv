using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class RetornoSatRepository : RepositoryBase, IRetornoSatRepository
    {

        public RetornoSat GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<RetornoSat> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Adicionar(RetornoSat retornoSat)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Insert Into SAT_TAB (NROVENDA, EMP, PDV, FUNCAO, NRO_SERIE_SAT, NRO_SESSAO, MENSAGEM, NUMDOCFISCAL,");
            sql.AppendLine("VALORTOTALCFE, XML_ASSINADO, EEEEE, CCCC, TIMESTAMPCFE, CHAVECONSULTA, DATAHORA)");
            sql.AppendLine("Values (@NROVENDA, @EMP, @PDV, @FUNCAO, @NRO_SERIE_SAT, @NRO_SESSAO, @MENSAGEM, @NUMDOCFISCAL,");
            sql.AppendLine("@VALORTOTALCFE, @XML_ASSINADO, @EEEEE, @CCCC, @TIMESTAMPCFE, @CHAVECONSULTA, @DATAHORA)");

            var parms = new DynamicParameters();
            parms.Add("@NROVENDA", retornoSat.VendaId);
            parms.Add("@EMP", retornoSat.EmpresaId);
            parms.Add("@PDV", retornoSat.Pdv);
            parms.Add("@FUNCAO", retornoSat.Funcao);
            parms.Add("@NRO_SERIE_SAT", retornoSat.NumeroSerie);
            parms.Add("@NRO_SESSAO", retornoSat.Secao);
            parms.Add("@MENSAGEM", retornoSat.Mensagem);
            parms.Add("@NUMDOCFISCAL", retornoSat.CfeSatNumeroExtrato);
            parms.Add("@VALORTOTALCFE", retornoSat.ValorCfe);
            parms.Add("@XML_ASSINADO", retornoSat.XmlSatAssinado);
            parms.Add("@EEEEE", retornoSat.Eeeee);
            parms.Add("@CCCC", retornoSat.Cccc);
            parms.Add("@TIMESTAMPCFE", retornoSat.Timestampcfe);
            parms.Add("@CHAVECONSULTA", retornoSat.ChaveEletronicaCFeSATNFce);
            parms.Add("@DATAHORA", retornoSat.DataHora);

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql.ToString(), parms);
                conn.Close();
            }

            sql = new StringBuilder();
            sql.AppendLine("IF NOT EXISTS(SELECT NAME FROM SYSCOLUMNS WHERE NAME = 'MSG_SAT' AND ID IN (SELECT ID FROM SYSOBJECTS WHERE NAME = 'VENDA_1'))");
            sql.AppendLine("ALTER TABLE VENDA_1 ADD MSG_SAT VARCHAR(500)");
            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql.ToString());
                conn.Close();
            }

            sql = new StringBuilder();
            sql.AppendLine("Update Venda_1 Set Cupom_fiscal = @CfeSatNumeroExtrato, MSG_SAT = @Mensagem Where Nro = @VendaId");

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql.ToString(), new {retornoSat.CfeSatNumeroExtrato, retornoSat.VendaId, retornoSat.Mensagem });
                conn.Close();
            }
        }

        public RetornoSat ObterPorVendaId(string vendaId)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Select NROVENDA as VendaId,");
            sql.AppendLine("CHAVECONSULTA as ChaveEletronicaCFeSATNFce,");
            sql.AppendLine("XML_ASSINADO as XmlSatAssinado, ");
            sql.AppendLine("DATAHORA AS DataHora ");
            sql.AppendLine("From SAT_TAB Where NROVENDA = @vendaId");
            using (var conn = Connection)
            {
                conn.Open();
                var retorno = conn.Query<RetornoSat>(sql.ToString(), new {vendaId}).FirstOrDefault();
                conn.Close();

                return retorno;
            }
        }

        public IEnumerable<RetornoSat> ObterPorData(DateTime dataInicio, DateTime dataFinal)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Select NROVENDA as VendaId,");
            sql.AppendLine("CHAVECONSULTA as ChaveEletronicaCFeSATNFce,");
            sql.AppendLine("Mensagem,");
            sql.AppendLine("XML_ASSINADO as XmlSatAssinado, ");
            sql.AppendLine("DATAHORA AS DataHora ");
            sql.AppendLine("From SAT_TAB Where Cast(DataHora as Date) Between @dataInicio And @dataFinal");
            using (var conn = Connection)
            {
                conn.Open();
                var retorno = conn.Query<RetornoSat>(sql.ToString(), new { dataInicio, dataFinal});
                conn.Close();

                return retorno;
            }
        }
    }
}