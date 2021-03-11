using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toch
{
    class clsProdutos
    {
        Entities _db;
        List<PROD> ListaPrdutos;
        List<GRUPO> ListaGrupos;
        List<CadMesas> ListMesas;
        List<opMesa1> ListaOpMesa1;
        List<Complemento> ListaComplementos;

        public List<PROD> ListaProdutosGrupo(string idGrupo)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Data.IDataReader dr = null;
            cmd.Connection = Program.CreateManager();
            cmd.CommandText = @"select Prod.Codigo, Prod.DES_, Isnull(Prod.VLVENDA,0) As VLVENDA, Tipo from pdvGrupoItens Inner Join Prod On  CodProduto = Prod.Codigo  Where idPdvGrupo = @grupo";
            cmd.Parameters.AddWithValue("@grupo", idGrupo);
            try
            {
                dr = cmd.ExecuteReader();
                ListaPrdutos = new List<PROD>();
                while (dr.Read())
                {
                    PROD prod = new PROD();
                    prod.CODIGO = Utils.FieldAsInt32(dr, "CODIGO");
                    prod.DES_ = Utils.FieldAsString(dr, "DES_");
                    prod.VLVENDA = (double)Utils.FieldAsDouble(dr, "VLVENDA");
                    prod.TIPO = (string)Utils.FieldAsString(dr, "Tipo");
                    ListaPrdutos.Add(prod);
                }

                return ListaPrdutos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PROD> ListaProdutosMeioMeio()
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Data.IDataReader dr = null;
            cmd.Connection = Program.CreateManager();
            cmd.CommandText = @"select Prod.Codigo, Prod.DES_, Isnull(Prod.VLVENDA,0) As VLVENDA, Tipo from pdvMeio Inner Join Prod On  CodProduto = Prod.Codigo";
            try
            {
                dr = cmd.ExecuteReader();
                ListaPrdutos = new List<PROD>();
                while (dr.Read())
                {
                    PROD prod = new PROD();
                    prod.CODIGO = Utils.FieldAsInt32(dr, "CODIGO");
                    prod.DES_ = Utils.FieldAsString(dr, "DES_");
                    prod.VLVENDA = (double)Utils.FieldAsDouble(dr, "VLVENDA");
                    prod.TIPO = (string)Utils.FieldAsString(dr, "Tipo");
                    ListaPrdutos.Add(prod);
                }

                return ListaPrdutos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GRUPO> ListaGrupo()
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Data.IDataReader dr = null;
            cmd.Connection = Program.CreateManager();
            cmd.CommandText = "Select IdPdvGrupo as Grupo, Descricao as DES_ from pdvGrupos";
            dr = cmd.ExecuteReader();
            ListaGrupos = new List<GRUPO>();
            while (dr.Read())
            {
                GRUPO grupo = new GRUPO();
                grupo.GRUPO1 = Utils.FieldAsInt32(dr, "GRUPO");
                grupo.DES_ = Utils.FieldAsString(dr, "DES_");
                ListaGrupos.Add(grupo);
            }

            //_db = new Entities();
            //ListaGrupos = new List<GRUPO>();SELECT DESCRICAO,IDMESA FROM CADMESAS

            //ListaGrupos = (from p in _db.GRUPO SELECT * FROM OPMESA1 WHERE (STATUS <> ''B'') ' +' AND IDMESA = 
            //               select p).ToList<GRUPO>();

            return ListaGrupos;
        }

        public List<CadMesas> ListaMesas()
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Data.IDataReader dr = null;
            cmd.Connection = Program.CreateManager();
            cmd.CommandText = "PR_SITUACAO_MESAS";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                dr = cmd.ExecuteReader();
                ListMesas = new List<CadMesas>();

                while (dr.Read())
                {
                    CadMesas mesas = new CadMesas();
                    mesas.IdMesa = Utils.FieldAsInt32(dr, "IdMesa");
                    mesas.Descricao = Utils.FieldAsString(dr, "Descricao");
                    mesas.Situacao = Utils.FieldAsString(dr, "Situacao");
                    ListMesas.Add(mesas);
                }
                return ListMesas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
        }

        public DateTime MesaSemAtendimento(int IdMesaOrigem)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            try
            {
                cmd.Connection = Program.CreateManager();
                cmd.CommandText = @"PROC_MESA_ATENDIDA";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idMesa", IdMesaOrigem);
                DateTime DataUltimoAtendimento = Convert.ToDateTime(cmd.ExecuteScalar());
                return DataUltimoAtendimento;
            }
            finally
            {
                Program.DisposeManager(cmd.Connection);
                cmd.Dispose();
            }
        }

        public opMesa1 opaMesas(int idMesa)
        {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                System.Data.IDataReader dr = null;
                cmd.Connection = Program.CreateManager();
                cmd.CommandText = "SELECT * FROM OPMESA1 WHERE (STATUS <> 'B') AND IDMESA = @IDMESA";
                cmd.Parameters.AddWithValue("@IDMESA", idMesa);
                cmd.CommandType = System.Data.CommandType.Text;

                opMesa1 OpMesa1 = new opMesa1();
                try
                {
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        OpMesa1 = new opMesa1();
                        OpMesa1.IdMesa = Utils.FieldAsInt32(dr, "IdMesa");
                        OpMesa1.IdopMesa1 = Utils.FieldAsInt32(dr, "IdopMesa1");
                        OpMesa1.IdGarcom = Utils.FieldAsInt32(dr, "IdGarcom");
                        OpMesa1.dthrInicial = Utils.FieldAsDateTime(dr, "dthrInicial");
                        OpMesa1.QtdePessoas = (short)Utils.FieldAsInt16(dr, "QtdePessoas");

                        OpMesa1.IdMesa = Utils.FieldAsInt32(dr, "IdMesa");

                        OpMesa1.Status = Utils.FieldAsString(dr, "Status");

                    }
                    dr.Close();

                    return OpMesa1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    dr.Close();
                }
            
        }

        public List<Complemento> ListaComplemento(int IdGrupo)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                System.Data.IDataReader dr = null;
                cmd.Connection = Program.CreateManager();
                cmd.CommandText = "Select * From Complemento Where PdvGrupo = @Grupo";
                cmd.Parameters.AddWithValue("@Grupo", IdGrupo);
                cmd.CommandType = System.Data.CommandType.Text;
                dr = cmd.ExecuteReader();

                ListaComplementos = new List<Complemento>();

                while (dr.Read())
                {
                    Complemento comp = new Complemento();
                    comp.GRUPO_PROD = Utils.FieldAsInt32(dr, "GRUPO_PROD");
                    comp.des_ = Utils.FieldAsString(dr, "des_");
                    comp.valor = Utils.FieldAsDecimal(dr, "valor");
                    comp.inc_compto = Utils.FieldAsInt32(dr, "inc_compto");

                    ListaComplementos.Add(comp);
                }
                dr.Close();

                return ListaComplementos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public decimal ValorPromocao(int IdProduto, ref int IdPromocao, ref decimal qtdePromocao)
        {
            //Pr_ProdutoEmPromocao parametros = IdProduto, data, situacao 1 ativo
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Data.IDataReader dr = null;
            cmd.Connection = Program.CreateManager();
            cmd.CommandText = "PR_PRODUTOEMPROMOCAO";
            cmd.Parameters.AddWithValue("@int_CodProd", IdProduto);
            cmd.Parameters.AddWithValue("@dtt_IniVnd", DateTime.Now);
            cmd.Parameters.AddWithValue("@int_situacao", 1);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();

                decimal ValorPromocao = decimal.Zero;
                while (dr.Read())
                {
                    ValorPromocao = Utils.FieldAsDecimal(dr, "valor");
                    IdPromocao = Utils.FieldAsInt32(dr, "idpromocao");
                    qtdePromocao = Utils.FieldAsDecimal(dr, "quantidade");
                }
                return ValorPromocao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public decimal ValorProd(int IdProduto, ref decimal ValorPromocao)
        {
            //Pr_ProdutoEmPromocao parametros = IdProduto, data, situacao 1 ativo
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Data.IDataReader dr = null;
            try
            {
                cmd.Connection = Program.CreateManager();
                cmd.CommandText = "Select cast(vlvenda2 as decimal(18,2)) as vlvenda2,  cast(VLMESA as decimal(18,2)) as VLMESA from Prod Where Codigo = @int_CodProd";
                cmd.Parameters.AddWithValue("@int_CodProd", IdProduto);


                cmd.CommandType = System.Data.CommandType.Text;
                dr = cmd.ExecuteReader();

                decimal ValorMesa = decimal.Zero;
                while (dr.Read())
                {
                    ValorMesa = Utils.FieldAsDecimal(dr, "VLMESA");
                    ValorPromocao = Utils.FieldAsDecimal(dr, "vlvenda2");
                    break;
                }
                return ValorMesa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            
        }
    }

}
