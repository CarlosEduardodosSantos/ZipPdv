using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface;

namespace Eticket.Infra.Data.Repository
{
    public class VendaRepository : RepositoryBase, IVendaRepository
    {
        public Venda GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Venda> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Adicionar(Venda venda)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Insert Into Venda_1(NRO, DATA, TIPO, VEND, HORA, COD_CLI, LOJA, VL_COMPRA, pgto, nrocx, pdv, xTELE, xFrete, cpfcnpj, TipoPagamento, Senha)");
            sql.AppendLine("Values(@NRO, @DATA, @TIPO, @VEND, @HORA, @COD_CLI, @LOJA, @VL_COMPRA, @pgto, @nrocx, @pdv, @xTELE, @xFrete, @cpfcnpj, @TipoPagamento, @Senha)");

            var parms = new DynamicParameters();

            parms.Add("@NRO", venda.VendaId);
            parms.Add("@DATA", venda.DataHora.Date);
            parms.Add("@TIPO", venda.Tipo);
            parms.Add("@VEND", venda.UsuarioId);
            parms.Add("@HORA", venda.DataHora.ToString("HH:mm:ss"));
            parms.Add("@COD_CLI", venda.ClienteId);
            parms.Add("@LOJA", venda.Loja);
            parms.Add("@VL_COMPRA", venda.VendaItens.Sum(t => t.ValorTotal));
            parms.Add("@pgto", venda.DataHora);
            parms.Add("@nrocx", venda.CaixaId);
            parms.Add("@pdv", venda.Pdv);
            parms.Add("@xTELE", venda.IsDelivery ? 1 : 0);
            parms.Add("@xFrete", 1);
            parms.Add("@cpfcnpj", venda.Cnpj);
            parms.Add("@TipoPagamento", venda.TipoPagamento);
            parms.Add("@Senha", ObterSenha());


            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql.ToString(), parms);

                foreach (var vendaVendaItens in venda.VendaItens)
                {
                    var sqlItem = new StringBuilder();
                    sqlItem.AppendLine("Insert Into Venda_2 (NRO ,QTDE ,COD_PROD,UNIT ,TOTAL ,PERC ,VALOR ,LOJA ,Des_  ,VEND ,SEQLANC");
                    sqlItem.AppendLine(",DATAHORA ,GPI_IMPRIMIR ,PROD_OBS ,VlCusto)");

                    sqlItem.AppendLine("Values (@NRO ,@QTDE ,@COD_PROD, @UNIT ,@TOTAL ,@PERC ,@VALOR ,@LOJA ,@Des_, @VEND, @SEQLANC");
                    sqlItem.AppendLine(",@DATAHORA ,@GPI_IMPRIMIR ,@PROD_OBS ,@VlCusto)");

                    var itemparms = new DynamicParameters();
                    itemparms.Add("@NRO", venda.VendaId);
                    itemparms.Add("@QTDE", vendaVendaItens.Quantidade);
                    itemparms.Add("@COD_PROD", vendaVendaItens.ProdutoId);
                    itemparms.Add("@UNIT", vendaVendaItens.ValorUnitatio);
                    itemparms.Add("@TOTAL", vendaVendaItens.ValorTotal);
                    itemparms.Add("@PERC", vendaVendaItens.Desconto);
                    itemparms.Add("@VALOR", vendaVendaItens.ValorUnitatio);
                    itemparms.Add("@LOJA", venda.Loja);
                    itemparms.Add("@Des_", vendaVendaItens.DescricaoProduto);
                    itemparms.Add("@VEND", venda.UsuarioId);
                    itemparms.Add("@SEQLANC", vendaVendaItens.SeqProduto);
                    itemparms.Add("@DATAHORA", venda.DataHora);
                    itemparms.Add("@GPI_IMPRIMIR", 1);
                    itemparms.Add("@PROD_OBS", vendaVendaItens.Observacao);
                    itemparms.Add("@VlCusto", 0.01);

                    conn.Query(sqlItem.ToString(), itemparms);

                    //Baixar Estoque
                    var sqlKardex = new StringBuilder();
                    sqlKardex.AppendLine("Exec PR_INSERT_KARDEX");
                    sqlKardex.AppendLine("@kad_loja,");
                    sqlKardex.AppendLine("@kad_op,");
                    sqlKardex.AppendLine("@kad_nroop,");
                    sqlKardex.AppendLine("@kad_prod,");
                    sqlKardex.AppendLine("@kad_qtde,");
                    sqlKardex.AppendLine("@kad_obs");
                    sqlKardex.AppendLine("");
                    sqlKardex.AppendLine(
                        $"Update prod set Qtde{venda.Loja} = Qtde{venda.Loja} - @quantidade Where Codigo = {vendaVendaItens.ProdutoId}");

                    conn.Query(sqlKardex.ToString(),
                        new
                        {
                            kad_loja = venda.Loja,
                            kad_op = "V",
                            kad_nroop = venda.VendaId,
                            kad_prod = vendaVendaItens.ProdutoId,
                            kad_qtde = (-1) * vendaVendaItens.Quantidade,
                            kad_obs = "VENDA",
                            quantidade = vendaVendaItens.Quantidade
                        });

                    foreach (var vendaComplemento in vendaVendaItens.VendaComplementos)
                    {
                        var sqlCOmplemento = new StringBuilder();
                        sqlCOmplemento.AppendLine("Insert Into Venda_4(NROVENDA, PRODCOD, COMPCOD, SEQLANC, VALOR)");
                        sqlCOmplemento.AppendLine("Values(@NROVENDA, @PRODCOD, @COMPCOD, @SEQLANC, @VALOR)");

                        var complementoParms = new DynamicParameters();
                        complementoParms.Add("@NROVENDA", venda.VendaId);
                        complementoParms.Add("@PRODCOD", vendaComplemento.ProdutoId);
                        complementoParms.Add("@COMPCOD", vendaComplemento.ComplementoId);
                        complementoParms.Add("@SEQLANC", vendaVendaItens.SeqProduto);
                        complementoParms.Add("@VALOR", vendaComplemento.Valor);

                        conn.Query(sqlCOmplemento.ToString(), complementoParms);
                    }
                    foreach (var vendaProdutoOpcao in vendaVendaItens.VendaProdutoOpcoes)
                    {
                        var sqlCOmplemento = new StringBuilder();
                        sqlCOmplemento.AppendLine("Insert Into Venda_4(NROVENDA, PRODCOD, COMPCOD, SEQLANC, VALOR)");
                        sqlCOmplemento.AppendLine("Values(@NROVENDA, @PRODCOD, @COMPCOD, @SEQLANC, @VALOR)");

                        var complementoParms = new DynamicParameters();
                        complementoParms.Add("@NROVENDA", venda.VendaId);
                        complementoParms.Add("@PRODCOD", vendaProdutoOpcao.ProdutoId);
                        complementoParms.Add("@COMPCOD", vendaProdutoOpcao.ProdutosOpcaoTipoId);
                        complementoParms.Add("@SEQLANC", vendaVendaItens.SeqProduto);
                        complementoParms.Add("@VALOR", vendaProdutoOpcao.Valor);

                        conn.Query(sqlCOmplemento.ToString(), complementoParms);
                    }
                }


                //Televendas
                if (venda.IsDelivery)
                {
                    var sqlCliente = new StringBuilder();
                    var plienteParm = new DynamicParameters();
                    plienteParm.Add("@Fone", venda.Delivery.ClienteDelivery.Telefone);
                    plienteParm.Add("@Nome", venda.Delivery.ClienteDelivery.Nome);
                    plienteParm.Add("@Endereco", venda.Delivery.ClienteDelivery.Endereco);
                    plienteParm.Add("@Bairro", venda.Delivery.ClienteDelivery.Bairro);
                    plienteParm.Add("@Cep", venda.Delivery.ClienteDelivery.Cep);
                    plienteParm.Add("@Cidade", venda.Delivery.ClienteDelivery.Cidade);
                    plienteParm.Add("@UF", venda.Delivery.ClienteDelivery.Uf);
                    plienteParm.Add("@Limite", 0);
                    plienteParm.Add("@Numero", venda.Delivery.ClienteDelivery.Numero);
                    plienteParm.Add("@Obs1", venda.Delivery.ClienteDelivery.Observacao);
                    plienteParm.Add("@ULTIMA_TAXA_ENTREGA", venda.Delivery.TaxaEntrega);

                    if (venda.Delivery.ClienteDeliveryId == 0)
                    {
                        sqlCliente.AppendLine("Insert Into Televenda_2(Fone, Nome, Endereco, Bairro, Cep, Cidade, UF, Limite, Obs1, ULTIMA_TAXA_ENTREGA, Numero)");
                        sqlCliente.AppendLine(
                            "Values(@Fone, @Nome, @Endereco, @Bairro, @Cep, @Cidade, @UF, @Limite, @Obs1, @ULTIMA_TAXA_ENTREGA, @Numero);");
                        sqlCliente.AppendLine("Select @@Identity;");


                        venda.Delivery.ClienteDeliveryId = conn.Query<int>(sqlCliente.ToString(), plienteParm).FirstOrDefault();
                    }
                    else
                    {
                        plienteParm.Add("@Codigo", venda.Delivery.ClienteDeliveryId);
                        sqlCliente.AppendLine("Update Televenda_2 Set");
                        sqlCliente.AppendLine("Fone = @Fone,");
                        sqlCliente.AppendLine("Nome = @Nome,");
                        sqlCliente.AppendLine("Endereco = @Endereco,");
                        sqlCliente.AppendLine("Bairro = @Bairro,");
                        sqlCliente.AppendLine("Cep = @Cep,");
                        sqlCliente.AppendLine("Cidade = @Cidade,");
                        sqlCliente.AppendLine("UF = @UF,");
                        sqlCliente.AppendLine("Limite = @Limite,");
                        sqlCliente.AppendLine("Obs1 = @Obs1,");
                        sqlCliente.AppendLine("Numero = @Numero,");
                        sqlCliente.AppendLine("ULTIMA_TAXA_ENTREGA = @ULTIMA_TAXA_ENTREGA");
                        sqlCliente.AppendLine("Where Codigo = @Codigo");

                        conn.Query(sqlCliente.ToString(), plienteParm);
                    }


                    var sqlDelivery = new StringBuilder();
                    sqlDelivery.AppendLine("Insert Into Televenda_1(Nro_Venda, Cod_Cliente, Condicao, Ped_Data, Ped_Hora, Ped_Func, Troco, valor, Loja,");
                    sqlDelivery.AppendLine("Obs, Taxa_Adicional)");
                    sqlDelivery.AppendLine("Values (@Nro_Venda, @Cod_Cliente, @Condicao, @Ped_Data, @Ped_Hora, @Ped_Func, @Troco, @valor, @Loja,");
                    sqlDelivery.AppendLine("@Obs, @Taxa_Adicional)");

                    var deliveryparms = new DynamicParameters();
                    deliveryparms.Add("@Nro_Venda", venda.VendaId);
                    deliveryparms.Add("@Cod_Cliente", venda.Delivery.ClienteDeliveryId);
                    deliveryparms.Add("@Condicao", "V");
                    deliveryparms.Add("@Ped_Data", venda.Delivery.DataHora.Date);
                    deliveryparms.Add("@Ped_Hora", venda.Delivery.DataHora.ToString("HH:mm"));
                    deliveryparms.Add("@Ped_Func", venda.UsuarioId);
                    deliveryparms.Add("@Troco", venda.Delivery.Troco);
                    deliveryparms.Add("@valor", venda.Delivery.Valor);
                    deliveryparms.Add("@Loja", venda.Loja);
                    deliveryparms.Add("@Obs", "");
                    deliveryparms.Add("@Taxa_Adicional", venda.Delivery.TaxaEntrega);

                    conn.Query(sqlDelivery.ToString(), deliveryparms);
                }


                conn.Close();
            }

            //AtualizaVendaId();
            //var tipoOperacao = venda.IsDelivery ? 5 : 4;
            //GeraImpressaoFechamento(venda.VendaId, tipoOperacao);

        }
        public void AtualizaFiscal(Venda venda)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Update Venda_1 Set Cupom_Fiscal = @CupomFiscal, ModeloFiscal = @ModeloFiscal Where Nro = @VendaId");

            var parms = new DynamicParameters();
            parms.Add("@CupomFiscal", venda.CupomFiscal);
            parms.Add("@ModeloFiscal", venda.ModeloFiscal);
            parms.Add("@VendaId", venda.VendaId);

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql.ToString(), parms);
                conn.Close();
            }

        }

        public void Cancelar(Venda venda)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Delete From Venda_2 Where Nro = @vendaId");
            sql.AppendLine("Delete From Venda_1 Where Nro = @vendaId");
            sql.AppendLine("Delete From CAIXA_2 Where NROVENDA = @vendaId");
            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql.ToString(), new { vendaId = venda.VendaId });

                foreach (var vendaVendaIten in venda.VendaItens)
                {
                    //Baixar Estoque
                    var sqlKardex = new StringBuilder();
                    sqlKardex.AppendLine("Exec PR_INSERT_KARDEX");
                    sqlKardex.AppendLine("@kad_loja,");
                    sqlKardex.AppendLine("@kad_op,");
                    sqlKardex.AppendLine("@kad_nroop,");
                    sqlKardex.AppendLine("@kad_prod,");
                    sqlKardex.AppendLine("@kad_qtde,");
                    sqlKardex.AppendLine("@kad_obs");
                    sqlKardex.AppendLine("");
                    sqlKardex.AppendLine(
                        $"Update prod set Qtde{venda.Loja} = Qtde{venda.Loja} + {vendaVendaIten.Quantidade} Where Codigo = {vendaVendaIten.ProdutoId}");

                    conn.Query(sqlKardex.ToString(),
                        new
                        {
                            kad_loja = venda.Loja,
                            kad_op = "V",
                            kad_nroop = venda.VendaId,
                            kad_prod = vendaVendaIten.ProdutoId,
                            kad_qtde = vendaVendaIten.Quantidade,
                            kad_obs = "VENDA (+) (EXCLUSÃO)"
                        });

                }

                conn.Close();
            }
        }

        public int VendaId()
        {
            var sql = "Select SEQUENCIA from SEQ_TABELA Where TABELA = 'VENDA' And COLUNA = 'NRO'";
            using (var conn = Connection)
            {
                conn.Open();
                var sequencia = conn.Query<int>(sql).FirstOrDefault();
                conn.Close();

                AtualizaVendaId();

                return sequencia;

            }
        }

        private void AtualizaVendaId()
        {
            var sql = "Update SEQ_TABELA Set Sequencia = Sequencia+1 Where TABELA = 'VENDA' And COLUNA = 'NRO'";

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql);
                conn.Close();
            }

        }
        public bool GeraImpressaoFechamento(int vendaId, int tipoOperacao)
        {
            var sql = "Exec Dbo.PROC_GRI_IMPRIME_FECHAMENTO @vendaId, @tipoOperacao, 'TOUCH', 0";

            using (var conn = Connection)
            {
                try
                {
                    conn.Open();
                    conn.Query(sql, new { vendaId, tipoOperacao });
                    conn.Close();

                    return true;
                }
                catch
                {
                    return false;
                }

            }

        }

        public IEnumerable<Venda> ObterEntregaPendentes()
        {
            var sql = @"Select 
	                            venda.nro as VendaId,
	                            venda.LOJA as Loja,
	                            venda.pdv as Pdv,
	                            venda.nrocx as CaixaId,
	                            venda.COD_CLI as ClienteId,
	                            venda.DATA as DataHora,
	                            venda.TIPO as Tipo,
	                            venda.VEND as UsuarioId,
	                            venda.VL_COMPRA as ValorCompra,
	                            venda.cpfcnpj as Cnpj,
	                            venda.OBS as Observacao,
	                            venda.Senha as Senha,
	                            Isnull(venda.xTELE,0) as IsDelivery,
                                Isnull(venda.TipoPagamento, '') as TipoPagamento,

	                            --Venda Itens
	                            vendaItem.INC_VENDA2 as VendaItemId,
	                            vendaItem.NRO as VendaId,
	                            vendaItem.SEQLANC as SeqProduto,
	                            vendaItem.Cod_Prod as ProdutoId,
	                            vendaItem.Des_ as Produto,
	                            vendaItem.UNIT as ValorUnitatio,
	                            vendaItem.QTDE as Quantidade,
	                            vendaItem.VALOR as ValorTotal,
	                            vendaItem.PROD_OBS as Observacao,

	                            --Delivery
	                            delivery.Nro as DeliveryId,
	                            delivery.Nro_Venda as VendaId,
	                            delivery.Ped_Data as DataHora,
                                delivery.Sai_Data as DataHoraSaida,	
	                            delivery.Ret_Data as DataHoraRetorno, 
	                            delivery.Cod_Cliente as ClienteDeliveryId,
                                Isnull(delivery.Troco,0) as Troco,                                
                                Isnull(delivery.valor,0) as Valor,
                                Isnull(delivery.Taxa_Adicional,0) as TaxaEntrega,
                                --Entregador
	                            delivery.Moto as EntregadorId,
	                            entregador.Nome as Entregador,
	                            
                                --Cliente
	                            cliente.Codigo as ClienteDeliveryId,
	                            cliente.Nome,
	                            cliente.Fone as Telefone,
	                            cliente.Endereco as Endereco,
	                            cliente.Numero as Numero,
	                            cliente.Bairro as Bairro,
	                            cliente.Cidade as Cidade,
	                            cliente.Cidade as Cep,
	                            cliente.Obs1 as Observacao,
	                            cliente.UF as Uf
                                
                            From Venda_1 venda
                            Left Join Televenda_1 delivery On venda.NRO = delivery.Nro_Venda
                            Left Join Televenda_2 cliente On delivery.Cod_Cliente = cliente.Codigo
                            Left Join Televenda_3 entregador On delivery.Moto = entregador.Codigo
                            Inner Join Venda_2 vendaItem On venda.NRO = vendaItem.NRO
                            Where  venda.xTELE = 1 And  (Sai_Data is null Or Ret_Data is null) ";

            using (var conn = Connection)
            {
                conn.Open();

                var identityMap = new Dictionary<int, Venda>();

                var vendas = conn.Query<Venda, VendaItem, Delivery, ClienteDelivery, Venda>(sql,
                    (v1, v2, d1, d2) =>
                    {
                        Venda master;
                        if (!identityMap.TryGetValue(v1.VendaId, out master))
                        {
                            identityMap[v1.VendaId] = master = v1;
                        }


                        var list = (List<VendaItem>)master.VendaItens;
                        if (list == null)
                        {
                            master.VendaItens = list = new List<VendaItem>();
                        }
                        list.Add(v2);

                        master.Delivery = d1 ?? new Delivery();
                        master.Delivery.ClienteDelivery = d2 ?? new ClienteDelivery();

                        return master;
                    }, splitOn: "VendaId, VendaItemId, DeliveryId, ClienteDeliveryId").Distinct();

                conn.Close();

                return vendas;
            }
        }

        public IEnumerable<Venda> ObterPendenteSat(DateTime dataInicio, DateTime dataFinal, int pdv)
        {
            var sql = @"Select 
	                            venda.nro as VendaId,
	                            venda.LOJA as Loja,
	                            venda.pdv as Pdv,
	                            venda.nrocx as CaixaId,
	                            venda.COD_CLI as ClienteId,
	                            venda.DATA + ' ' + venda.hora as DataHora,
	                            venda.TIPO as Tipo,
	                            venda.VEND as UsuarioId,
	                            venda.VL_COMPRA as ValorCompra,
	                            venda.cpfcnpj as Cnpj,
	                            venda.OBS as Observacao,
                                CupomFiscal = isnull((Select top 1 NUMDOCFISCAL From SAT_TAB Where sat_tab.NROVENDA = venda.NRO Order By dataHora desc),''),
                                MenssagemSat = isnull((Select top 1 MENSAGEM From SAT_TAB Where sat_tab.NROVENDA = venda.NRO Order By dataHora desc),''),
	                            venda.Senha as Senha,
	                            Isnull(venda.xTELE,0) as IsDelivery,
                                iSNULL(venda.TipoPagamento, '') as TipoPagamento,
                                

	                            --Venda Itens
	                            vendaItem.INC_VENDA2 as VendaItemId,
	                            vendaItem.NRO as VendaId,
	                            vendaItem.SEQLANC as SeqProduto,
	                            vendaItem.Cod_Prod as ProdutoId,
	                            vendaItem.Des_ as Produto,
	                            vendaItem.UNIT as ValorUnitatio,
	                            vendaItem.QTDE as Quantidade,
	                            vendaItem.VALOR as ValorTotal,
	                            vendaItem.PROD_OBS as Observacao,

	                            --Delivery
	                            delivery.Nro as DeliveryId,
	                            delivery.Nro_Venda as VendaId,
	                            delivery.Ped_Data as DataHora,
                                delivery.Sai_Data as DataHoraSaida,	
	                            delivery.Ret_Data as DataHoraRetorno, 
	                            delivery.Cod_Cliente as ClienteDeliveryId,
                                Isnull(delivery.Troco,0) as Troco,                                
                                Isnull(delivery.valor,0) as Valor,
                                Isnull(delivery.Taxa_Adicional,0) as TaxaEntrega,
                                --Entregador
	                            delivery.Moto as EntregadorId,
	                            entregador.Nome as Entregador,
	                            
                                --Cliente
	                            cliente.Codigo as ClienteDeliveryId,
	                            cliente.Nome,
	                            cliente.Fone as Telefone,
	                            cliente.Endereco as Endereco,
	                            --cliente.Numero as Numero,
	                            cliente.Bairro as Bairro,
	                            cliente.Cidade as Cidade,
	                            cliente.Cidade as Cep,
	                            cliente.Obs1 as Observacao,
	                            cliente.UF as Uf
                                
                            From Venda_1 venda
                            Left Join Televenda_1 delivery On venda.NRO = delivery.Nro_Venda
                            Left Join Televenda_2 cliente On delivery.Cod_Cliente = cliente.Codigo
                            Left Join Televenda_3 entregador On delivery.Moto = entregador.Codigo
                            Inner Join Venda_2 vendaItem On venda.NRO = vendaItem.NRO
                            Where Data Between @dataInicio And @dataFinal And venda.Pdv = @pdv And Isnull(CUPOM_FISCAL,'') = '' Or Isnull(CUPOM_FISCAL,'') = '0'";

            using (var conn = Connection)
            {
                conn.Open();

                var identityMap = new Dictionary<int, Venda>();

                var vendas = conn.Query<Venda, VendaItem, Delivery, ClienteDelivery, Venda>(sql,
                    (v1, v2, d1, d2) =>
                    {
                        Venda master;
                        if (!identityMap.TryGetValue(v1.VendaId, out master))
                        {
                            identityMap[v1.VendaId] = master = v1;
                        }


                        var list = (List<VendaItem>)master.VendaItens;
                        if (list == null)
                        {
                            master.VendaItens = list = new List<VendaItem>();
                        }
                        list.Add(v2);

                        master.Delivery = d1 ?? new Delivery();
                        master.Delivery.ClienteDelivery = d2 ?? new ClienteDelivery();

                        return master;
                    }, new { dataInicio, dataFinal, pdv }, splitOn: "VendaId, VendaItemId, DeliveryId, ClienteDeliveryId").Distinct();

                conn.Close();

                return vendas.Where(t => string.IsNullOrEmpty(t.CupomFiscal));
            }
        }

        public IEnumerable<Venda> ObterNroSat(string nroSat)
        {
            var sql = @"Select 
	                            venda.nro as VendaId,
	                            venda.LOJA as Loja,
	                            venda.pdv as Pdv,
	                            venda.nrocx as CaixaId,
	                            venda.COD_CLI as ClienteId,
	                            venda.DATA + ' ' + venda.hora as DataHora,
	                            venda.TIPO as Tipo,
	                            venda.VEND as UsuarioId,
	                            venda.VL_COMPRA as ValorCompra,
	                            venda.cpfcnpj as Cnpj,
	                            venda.OBS as Observacao,
                                CupomFiscal = isnull((Select top 1 NUMDOCFISCAL From SAT_TAB Where sat_tab.NROVENDA = venda.NRO Order By dataHora desc),''),
                                MenssagemSat = isnull((Select top 1 MENSAGEM From SAT_TAB Where sat_tab.NROVENDA = venda.NRO Order By dataHora desc),''),
	                            venda.Senha as Senha,
	                            Isnull(venda.xTELE,0) as IsDelivery,
                                iSNULL(venda.TipoPagamento, '') as TipoPagamento,

	                            --Venda Itens
	                            vendaItem.INC_VENDA2 as VendaItemId,
	                            vendaItem.NRO as VendaId,
	                            vendaItem.SEQLANC as SeqProduto,
	                            vendaItem.Cod_Prod as ProdutoId,
	                            vendaItem.Des_ as Produto,
	                            vendaItem.UNIT as ValorUnitatio,
	                            vendaItem.QTDE as Quantidade,
	                            vendaItem.VALOR as ValorTotal,
	                            vendaItem.PROD_OBS as Observacao,

	                            --Delivery
	                            delivery.Nro as DeliveryId,
	                            delivery.Nro_Venda as VendaId,
	                            delivery.Ped_Data as DataHora,
                                delivery.Sai_Data as DataHoraSaida,	
	                            delivery.Ret_Data as DataHoraRetorno, 
	                            delivery.Cod_Cliente as ClienteDeliveryId,
                                Isnull(delivery.Troco,0) as Troco,                                
                                Isnull(delivery.valor,0) as Valor,
                                Isnull(delivery.Taxa_Adicional,0) as TaxaEntrega,
                                --Entregador
	                            delivery.Moto as EntregadorId,
	                            entregador.Nome as Entregador,
	                            
                                --Cliente
	                            cliente.Codigo as ClienteDeliveryId,
	                            cliente.Nome,
	                            cliente.Fone as Telefone,
	                            cliente.Endereco as Endereco,
	                            --cliente.Numero as Numero,
	                            cliente.Bairro as Bairro,
	                            cliente.Cidade as Cidade,
	                            cliente.Cidade as Cep,
	                            cliente.Obs1 as Observacao,
	                            cliente.UF as Uf
                                
                            From Venda_1 venda
                            Left Join Televenda_1 delivery On venda.NRO = delivery.Nro_Venda
                            Left Join Televenda_2 cliente On delivery.Cod_Cliente = cliente.Codigo
                            Left Join Televenda_3 entregador On delivery.Moto = entregador.Codigo
                            Inner Join Venda_2 vendaItem On venda.NRO = vendaItem.NRO
                            Where Isnull(CUPOM_FISCAL,'') Like @nroSat";

            using (var conn = Connection)
            {
                conn.Open();

                var identityMap = new Dictionary<int, Venda>();

                var vendas = conn.Query<Venda, VendaItem, Delivery, ClienteDelivery, Venda>(sql,
                    (v1, v2, d1, d2) =>
                    {
                        Venda master;
                        if (!identityMap.TryGetValue(v1.VendaId, out master))
                        {
                            identityMap[v1.VendaId] = master = v1;
                        }


                        var list = (List<VendaItem>)master.VendaItens;
                        if (list == null)
                        {
                            master.VendaItens = list = new List<VendaItem>();
                        }
                        list.Add(v2);

                        master.Delivery = d1 ?? new Delivery();
                        master.Delivery.ClienteDelivery = d2 ?? new ClienteDelivery();

                        return master;
                    }, new { nroSat }, splitOn: "VendaId, VendaItemId, DeliveryId, ClienteDeliveryId").Distinct();

                conn.Close();

                return vendas;
            }
        }

        public IEnumerable<Venda> ObterData(DateTime dataInicio, DateTime dataFinal)
        {
            var sql = @"Select 
	                            venda.nro as VendaId,
	                            venda.LOJA as Loja,
	                            venda.pdv as Pdv,
	                            venda.nrocx as CaixaId,
	                            venda.COD_CLI as ClienteId,
	                            venda.DATA + ' ' + venda.hora as DataHora,
	                            venda.TIPO as Tipo,
	                            venda.VEND as UsuarioId,
	                            venda.VL_COMPRA as ValorCompra,
	                            venda.cpfcnpj as Cnpj,
	                            venda.OBS as Observacao,
                                venda.Cupom_fiscal as CupomFiscal,
	                            venda.Senha as Senha,
	                            Isnull(venda.xTELE,0) as IsDelivery,
                                iSNULL(venda.TipoPagamento, '') as TipoPagamento,

	                            --Venda Itens
	                            vendaItem.INC_VENDA2 as VendaItemId,
	                            vendaItem.NRO as VendaId,
	                            vendaItem.SEQLANC as SeqProduto,
	                            vendaItem.Cod_Prod as ProdutoId,
	                            vendaItem.Des_ as Produto,
	                            vendaItem.UNIT as ValorUnitatio,
	                            vendaItem.QTDE as Quantidade,
	                            vendaItem.VALOR as ValorTotal,
	                            vendaItem.PROD_OBS as Observacao,

	                            --Delivery
	                            delivery.Nro as DeliveryId,
	                            delivery.Nro_Venda as VendaId,
	                            delivery.Ped_Data as DataHora,
                                delivery.Sai_Data as DataHoraSaida,	
	                            delivery.Ret_Data as DataHoraRetorno, 
	                            delivery.Cod_Cliente as ClienteDeliveryId,
                                Isnull(delivery.Troco,0) as Troco,                                
                                Isnull(delivery.valor,0) as Valor,
                                Isnull(delivery.Taxa_Adicional,0) as TaxaEntrega,
                                --Entregador
	                            delivery.Moto as EntregadorId,
	                            entregador.Nome as Entregador,
	                            
                                --Cliente
	                            cliente.Codigo as ClienteDeliveryId,
	                            cliente.Nome,
	                            cliente.Fone as Telefone,
	                            cliente.Endereco as Endereco,
	                            --cliente.Numero as Numero,
	                            cliente.Bairro as Bairro,
	                            cliente.Cidade as Cidade,
	                            cliente.Cidade as Cep,
	                            cliente.Obs1 as Observacao,
	                            cliente.UF as Uf
                                
                            From Venda_1 venda
                            Left Join Televenda_1 delivery On venda.NRO = delivery.Nro_Venda
                            Left Join Televenda_2 cliente On delivery.Cod_Cliente = cliente.Codigo
                            Left Join Televenda_3 entregador On delivery.Moto = entregador.Codigo
                            Inner Join Venda_2 vendaItem On venda.NRO = vendaItem.NRO
                            Where Data Between @dataInicio And @dataFinal ";

            using (var conn = Connection)
            {
                conn.Open();

                var identityMap = new Dictionary<int, Venda>();

                var vendas = conn.Query<Venda, VendaItem, Delivery, ClienteDelivery, Venda>(sql,
                    (v1, v2, d1, d2) =>
                    {
                        Venda master;
                        if (!identityMap.TryGetValue(v1.VendaId, out master))
                        {
                            identityMap[v1.VendaId] = master = v1;
                        }


                        var list = (List<VendaItem>)master.VendaItens;
                        if (list == null)
                        {
                            master.VendaItens = list = new List<VendaItem>();
                        }
                        list.Add(v2);

                        master.Delivery = d1 ?? new Delivery();
                        master.Delivery.ClienteDelivery = d2 ?? new ClienteDelivery();

                        return master;
                    }, new { dataInicio, dataFinal }, splitOn: "VendaId, VendaItemId, DeliveryId, ClienteDeliveryId").Distinct();

                conn.Close();

                return vendas;
            }
        }

        public bool GeraImpressaoItens(int vendaId, int tipoOperacao)
        {
            var sql = "Exec Dbo.PROC_GRI_IMPRIME @vendaId, 0";

            using (var conn = Connection)
            {
                try
                {
                    conn.Open();
                    conn.Query(sql, new { vendaId });
                    conn.Close();

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }

            }

        }

        private int ObterSenha()
        {
            var sql = "select valor from configuracoes where variavel like 'senha'";

            using (var conn = Connection)
            {
                try
                {
                    conn.Open();
                    var senha = conn.Query<int>(sql).FirstOrDefault();

                    senha += 1;

                    //Incrementa valor
                    conn.Execute("Update configuracoes Set valor = @novaSenha where variavel like 'senha'", new { novaSenha = senha });
                    
                    conn.Close();


                    return senha;

                }
                catch
                {
                    return 0;
                }
            }
        }

        public Venda ObterPorId(int vendaId)
        {
            var sql = @"Select 
	                            venda.nro as VendaId,
	                            venda.LOJA as Loja,
	                            venda.pdv as Pdv,
	                            venda.nrocx as CaixaId,
	                            venda.COD_CLI as ClienteId,
	                            venda.DATA + ' ' + venda.hora as DataHora,
	                            venda.TIPO as Tipo,
	                            venda.VEND as UsuarioId,
	                            venda.VL_COMPRA as ValorCompra,
	                            venda.cpfcnpj as Cnpj,
	                            venda.OBS as Observacao,
                                CupomFiscal = isnull((Select top 1 NUMDOCFISCAL From SAT_TAB Where sat_tab.NROVENDA = venda.NRO Order By dataHora desc),''),
                                MenssagemSat = isnull((Select top 1 MENSAGEM From SAT_TAB Where sat_tab.NROVENDA = venda.NRO Order By dataHora desc),''),
	                            venda.Senha as Senha,
	                            Isnull(venda.xTELE,0) as IsDelivery,
                                iSNULL(venda.TipoPagamento, '') as TipoPagamento,

	                            --Venda Itens
	                            vendaItem.INC_VENDA2 as VendaItemId,
	                            vendaItem.NRO as VendaId,
	                            vendaItem.SEQLANC as SeqProduto,
	                            vendaItem.Cod_Prod as ProdutoId,
	                            vendaItem.Des_ as Produto,
	                            vendaItem.UNIT as ValorUnitatio,
	                            vendaItem.QTDE as Quantidade,
	                            vendaItem.VALOR as ValorTotal,
	                            vendaItem.PROD_OBS as Observacao,

	                            --Delivery
	                            delivery.Nro as DeliveryId,
	                            delivery.Nro_Venda as VendaId,
	                            delivery.Ped_Data as DataHora,
                                delivery.Sai_Data as DataHoraSaida,	
	                            delivery.Ret_Data as DataHoraRetorno, 
	                            delivery.Cod_Cliente as ClienteDeliveryId,
                                Isnull(delivery.Troco,0) as Troco,                                
                                Isnull(delivery.valor,0) as Valor,
                                Isnull(delivery.Taxa_Adicional,0) as TaxaEntrega,
                                --Entregador
	                            delivery.Moto as EntregadorId,
	                            entregador.Nome as Entregador,
	                            
                                --Cliente
	                            cliente.Codigo as ClienteDeliveryId,
	                            cliente.Nome,
	                            cliente.Fone as Telefone,
	                            cliente.Endereco as Endereco,
	                            --cliente.Numero as Numero,
	                            cliente.Bairro as Bairro,
	                            cliente.Cidade as Cidade,
	                            cliente.Cidade as Cep,
	                            cliente.Obs1 as Observacao,
	                            cliente.UF as Uf
                                
                            From Venda_1 venda
                            Left Join Televenda_1 delivery On venda.NRO = delivery.Nro_Venda
                            Left Join Televenda_2 cliente On delivery.Cod_Cliente = cliente.Codigo
                            Left Join Televenda_3 entregador On delivery.Moto = entregador.Codigo
                            Inner Join Venda_2 vendaItem On venda.NRO = vendaItem.NRO
                            Where venda.nro = @vendaId";

            using (var conn = Connection)
            {
                conn.Open();

                var identityMap = new Dictionary<int, Venda>();

                var vendas = conn.Query<Venda, VendaItem, Delivery, ClienteDelivery, Venda>(sql,
                    (v1, v2, d1, d2) =>
                    {
                        Venda master;
                        if (!identityMap.TryGetValue(v1.VendaId, out master))
                        {
                            identityMap[v1.VendaId] = master = v1;
                        }


                        var list = (List<VendaItem>)master.VendaItens;
                        if (list == null)
                        {
                            master.VendaItens = list = new List<VendaItem>();
                        }
                        list.Add(v2);

                        master.Delivery = d1 ?? new Delivery();
                        master.Delivery.ClienteDelivery = d2 ?? new ClienteDelivery();

                        return master;
                    }, new { vendaId }, splitOn: "VendaId, VendaItemId, DeliveryId, ClienteDeliveryId").FirstOrDefault();

                conn.Close();

                return vendas;
            }
        }
    }
}