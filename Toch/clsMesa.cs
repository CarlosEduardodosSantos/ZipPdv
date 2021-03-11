using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toch
{
    class clsMesa
    {
        public List<opMesa2> ListaItensMesas(int idMesa)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Data.IDataReader dr = null;
            cmd.Connection = Program.CreateManager();
            cmd.CommandText = "SELECT * FROM OPMESA2 WHERE IdopMesa1 = @IDMESA";
            cmd.Parameters.AddWithValue("@IDMESA", idMesa);
            cmd.CommandType = System.Data.CommandType.Text;
            dr = cmd.ExecuteReader();

            List<opMesa2> LitResult = new List<opMesa2>();
            opMesa2 OpMesa2;

            while (dr.Read())
            {
                OpMesa2 = new opMesa2();
                OpMesa2.IdopMesa1 = Utils.FieldAsInt32(dr, "IdopMesa1");
                OpMesa2.idOpMesa2 = Utils.FieldAsInt32(dr, "idOpMesa2");
                OpMesa2.idpromocao = Utils.FieldAsInt32(dr, "idpromocao");
                OpMesa2.Meio1 = Utils.FieldAsInt32(dr, "Meio1");
                OpMesa2.Meio2 = Utils.FieldAsInt32(dr, "Meio2");
                OpMesa2.Qtde = Utils.FieldAsDecimal(dr, "Qtde");
                OpMesa2.SABOR = Utils.FieldAsInt32(dr, "SABOR");
                OpMesa2.SEQLANC = Utils.FieldAsInt32(dr, "SEQLANC");
                OpMesa2.Status = Utils.FieldAsBoolean(dr, "Status");
                OpMesa2.Valor = Utils.FieldAsDecimal(dr, "Valor");
                OpMesa2.vlunit = Utils.FieldAsDecimal(dr, "vlunit");
                OpMesa2.DesProduto = Utils.FieldAsString(dr, "DesProduto");
                OpMesa2.CodProduto = Utils.FieldAsInt32(dr, "CodProduto");
                OpMesa2.cod_garcom = Utils.FieldAsInt32(dr, "cod_garcom");
                LitResult.Add(OpMesa2);


            }
            dr.Close();

            return LitResult;
        }

        public decimal PagamentoParcial(int idMesa)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = Program.CreateManager();
            cmd.CommandText = @"select (ISNULL(dinheiro,0) + ISNULL(cheque,0) + ISNULL(cartao_debito,0) + ISNULL(cartao_credito,0) +
		                                ISNULL(ticket,0) ) - (ISNULL(troco,0)) from OPMESA1 where IdopMesa1 = @IDMESA";
            cmd.Parameters.AddWithValue("@IDMESA", idMesa);
            cmd.CommandType = System.Data.CommandType.Text;
            decimal Parcial = (decimal)cmd.ExecuteScalar();

            return Parcial;
        }

        public int GravaItensMesa(opMesa2 itens)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            cmd.Connection = Program.CreateManager();
            cmd.CommandText = @"PROC_INSERT_OPMESA2_PROMO";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdopMesa1", itens.IdopMesa1);
            cmd.Parameters.AddWithValue("@CodProduto", itens.CodProduto);
            cmd.Parameters.AddWithValue("@Qtde", itens.Qtde);
            cmd.Parameters.AddWithValue("@Status", itens.Status);
            cmd.Parameters.AddWithValue("@Valor", itens.Valor);
            cmd.Parameters.AddWithValue("@DesProduto", itens.DesProduto);
            cmd.Parameters.AddWithValue("@Meio1", itens.Meio1);
            cmd.Parameters.AddWithValue("@Meio2", itens.Meio2);
            cmd.Parameters.AddWithValue("@vlunit", itens.vlunit);
            cmd.Parameters.AddWithValue("@cod_garcom", itens.cod_garcom);
            cmd.Parameters.AddWithValue("@SABOR", itens.SABOR);
            cmd.Parameters.AddWithValue("@SEQLANC", itens.SEQLANC);
            cmd.Parameters.AddWithValue("@Estacao", System.Environment.MachineName);
            cmd.Parameters.AddWithValue("@PROD_OBS", itens.PROD_OBS);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return 1;
        }

        public int GravaItensVENDA_4(VENDA_4 itens)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            cmd.Connection = Program.CreateManager();
            cmd.CommandText = @"INSERT INTO VENDA_4(COMPCOD,  PRODCOD,  SEQLANC,  VALOR, IDOPMESA1)
                                VALUES (@COMPCOD, @PRODCOD, @SEQLANC, @VALOR, @IDOPMESA1)";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@COMPCOD", itens.COMPCOD);
            cmd.Parameters.AddWithValue("@PRODCOD", itens.PRODCOD);
            cmd.Parameters.AddWithValue("@SEQLANC", itens.SEQLANC);
            cmd.Parameters.AddWithValue("@VALOR", itens.VALOR);
            cmd.Parameters.AddWithValue("@IDOPMESA1", itens.IDOPMESA1);

            cmd.ExecuteNonQuery();



            return 1;
        }

        public int GravaItenMeioMeio(MeioMeio itens)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            cmd.Connection = Program.CreateManager();
            cmd.CommandText = @"INSERT INTO MeioMeio(Descricao,  IdProduto,  NroOperacao,  QtdeImpresso, Quantidade, SeqProduto, TipoOperacao, ValorVenda)
                                VALUES (@Descricao, @IdProduto, @NroOperacao, @QtdeImpresso, @Quantidade, @SeqProduto, @TipoOperacao, @ValorVenda)";
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Descricao", itens.Descricao);
            cmd.Parameters.AddWithValue("@IdProduto", itens.IdProduto);
            cmd.Parameters.AddWithValue("@NroOperacao", itens.NroOperacao);
            cmd.Parameters.AddWithValue("@QtdeImpresso", itens.QtdeImpresso);
            cmd.Parameters.AddWithValue("@Quantidade", itens.Quantidade);

            cmd.Parameters.AddWithValue("@SeqProduto", itens.SeqProduto);
            cmd.Parameters.AddWithValue("@TipoOperacao", "M");//itens.TipoOperacao);
            cmd.Parameters.AddWithValue("@ValorVenda", itens.ValorVenda);

            cmd.ExecuteNonQuery();



            return 1;
        }

        public int AbreMesa(opMesa1 mesa)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            cmd.Connection = Program.CreateManager();
            cmd.CommandText = @"Insert Into opMesa1(IdGarcom,IdMesa,dthrInicial,Status, QtdePessoas, MesaTransf) Values (@IdGarcom,@IdMesa,@dthrInicial,@Status, @QtdePessoas, @MesaTransf); Select @@identity";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@IdGarcom", mesa.IdGarcom);
            cmd.Parameters.AddWithValue("@IdMesa", mesa.IdMesa);
            cmd.Parameters.AddWithValue("@dthrInicial", mesa.dthrInicial);
            cmd.Parameters.AddWithValue("@Status", mesa.Status);
            cmd.Parameters.AddWithValue("@QtdePessoas", mesa.QtdePessoas);
            cmd.Parameters.AddWithValue("@MesaTransf", !String.IsNullOrEmpty(mesa.MesaTransf.ToString()) ? mesa.MesaTransf: 0);
            try
            {
                int IdopMesa1 = Convert.ToInt32(cmd.ExecuteScalar());
                return IdopMesa1;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception("Erro ao abrir mesa!" + ex.Message);
            }
        }

        public int AlteraStatus(string Status, int IdopMesa1)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            cmd.Connection = Program.CreateManager();
            cmd.CommandText = @"UPDATE OPMESA1 SET STATUS = @Status WHERE IdOpmesa1 = @IdOpmesa1";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdOpmesa1", IdopMesa1);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.ExecuteNonQuery();

            return 1;
        }

        public int TransfereMesa(int IdMesaOrigem, int IdMesaDestino)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = Program.CreateManager();
            cmd.CommandText = @"PR_TRANSFERENCIAMESA";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdMesaOrig", IdMesaOrigem);
            cmd.Parameters.AddWithValue("@IdMesaDest", IdMesaDestino);
            int IdopMesa1 = Convert.ToInt32(cmd.ExecuteScalar());
            return IdopMesa1;
        }

        public void Gr_Impressao(int idopmesa, int tipoop)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = Program.CreateManager();
            cmd.CommandText = @"PROC_GRI_IMPRIME";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@NroOperacao", idopmesa);
            cmd.Parameters.AddWithValue("@TipoOperacao", tipoop);
            try
            {
               cmd.ExecuteScalar();
            }
            catch
            { }
        }

        public List<CadGarcom> Listagarcom()
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Data.IDataReader dr = null;
            cmd.Connection = Program.CreateManager();
            cmd.CommandText = "SELECT * FROM cadGarcom";
            cmd.CommandType = System.Data.CommandType.Text;
            dr = cmd.ExecuteReader();

            List<CadGarcom> LitResult = new List<CadGarcom>();
            CadGarcom cGarcom;

            while (dr.Read())
            {
                cGarcom = new CadGarcom();
                cGarcom.IdGarcom = Utils.FieldAsInt32(dr, "IdGarcom");
                cGarcom.Descricao = Utils.FieldAsString(dr, "Descricao");

                LitResult.Add(cGarcom);
            }
            dr.Close();

            return LitResult;
        }

        public CadGarcom ListagarcomId(int idGracom, string senha)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Data.IDataReader dr = null;
            cmd.Connection = Program.CreateManager();
            if (idGracom > 0)
                cmd.CommandText = "SELECT * FROM cadGarcom Where IdGarcom = " + idGracom;
            else if (senha != string.Empty)
                cmd.CommandText = "SELECT * FROM cadGarcom Where senha = " + senha;
            else
                return null;

            cmd.CommandType = System.Data.CommandType.Text;
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch
            {
                return null;
            }

            CadGarcom cGarcom = null;

            while (dr.Read())
            {
                cGarcom = new CadGarcom();
                cGarcom.IdGarcom = Utils.FieldAsInt32(dr, "IdGarcom");
                cGarcom.Descricao = Utils.FieldAsString(dr, "Descricao");

            }
            dr.Close();

            return cGarcom;
        }

        public int GravaAuditoria(Auditoria auditoria)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = Program.CreateManager();
            cmd.CommandText = @"Insert Into Auditoria
                                (
	                                data,	hora,		loja,	usuario,	ocorrencia,		cliente,
	                                valor,	maquina,	Venda,	motivo,		NROCX
                                )
                                Values
                                (
	                                @data,	@hora,		@loja,	@usuario,	@ocorrencia,	@cliente,
	                                @valor, @maquina,	@Venda, @motivo,	@NROCX
                                )";

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@data", auditoria.data);
            cmd.Parameters.AddWithValue("@hora", auditoria.hora);
            cmd.Parameters.AddWithValue("@loja", auditoria.loja);
            cmd.Parameters.AddWithValue("@usuario", auditoria.usuario);
            cmd.Parameters.AddWithValue("@ocorrencia", auditoria.ocorrencia);
            cmd.Parameters.AddWithValue("@cliente", auditoria.cliente);
            cmd.Parameters.AddWithValue("@valor", auditoria.valor);
            cmd.Parameters.AddWithValue("@maquina", auditoria.maquina);
            cmd.Parameters.AddWithValue("@Venda", auditoria.Venda);
            cmd.Parameters.AddWithValue("@motivo", auditoria.motivo);
            cmd.Parameters.AddWithValue("@NROCX", auditoria.NROCX);
            try
            {
                int IdopMesa1 = Convert.ToInt32(cmd.ExecuteScalar());
                return IdopMesa1;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar auditoria!" + ex.Message);
            }
        }

        public List<SugestaoObs> ListaSugestaoObs(string descricao)
        {
            if (descricao != string.Empty)
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                System.Data.IDataReader dr = null;
                cmd.Connection = Program.CreateManager();
                cmd.CommandText = "select * from SugestaoObs where Sugestao like '%" + descricao + "%'" + "order by ranking desc";
                cmd.CommandType = System.Data.CommandType.Text;
                try
                {
                    dr = cmd.ExecuteReader();
                }
                catch
                {
                    return new List<SugestaoObs>();
                }

                List<SugestaoObs> List = new List<SugestaoObs>();
                SugestaoObs Sugestao = null;
                while (dr.Read())
                {
                    Sugestao = new SugestaoObs();
                    Sugestao.IdSugestao = Utils.FieldAsInt32(dr, "IdSugestao");
                    Sugestao.Sugestao = Utils.FieldAsString(dr, "Sugestao");

                    List.Add(Sugestao);
                }
                dr.Close();

                return List;
            }
            else
                return new List<SugestaoObs>();
        }

        public void GravaSugestao(String Sugestao)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = Program.CreateManager();
            cmd.CommandText = @"Insert Into SugestaoObs (Sugestao, ranking)
                                Values (@Sugestao, @ranking)";

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Sugestao", Sugestao);
            cmd.Parameters.AddWithValue("@ranking", 1);
            try
            {
                Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar Sugestao!" + ex.Message);
            }
        }

        public void AtualizaSugestao(string Sugestao)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = Program.CreateManager();
            cmd.CommandText = @"Update SugestaoObs Set ranking = ranking + 1 Where Sugestao = @Sugestao";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Sugestao", Sugestao);
            try
            {
                Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar Sugestao!" + ex.Message);
            }
        }

    }
}
