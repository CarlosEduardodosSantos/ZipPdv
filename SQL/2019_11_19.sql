
CREATE TABLE [dbo].[CaixaPagamentos](
	[CaixaPagamentoId] [int] IDENTITY(1,1) NOT NULL,
	[CaixaId] [int] NOT NULL,
	[CaixaItemId] [uniqueidentifier] NOT NULL,
	[CartaoRespostaGuid] [uniqueidentifier] NOT NULL,
	[EspeciePagamentoId] [int] NOT NULL,
	[Especie] [varchar](50) NOT NULL,
	[Valor] [decimal](18, 2) NOT NULL,
	[Interno] [varchar](10) NOT NULL,
 CONSTRAINT [PK_CaixaPagamentos] PRIMARY KEY CLUSTERED 
(
	[CaixaPagamentoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[CaixaFechamentos](
	[CaixaFechamentoId] [int] IDENTITY(1,1) NOT NULL,
	[CaixaId] [int] NOT NULL,
	[EspecieId] [int] NOT NULL,
	[Especie] [varchar](50) NOT NULL,
	[Valor] [decimal](18, 2) NOT NULL,
	[Divergencia] [decimal](18, 2) NOT NULL,
	[Conferido] [bit] NOT NULL,
	[UsuarioConferenciaId] [int] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CaixaFechamentos] ADD  CONSTRAINT [DF_CaixaFechamentos_Conferido]  DEFAULT ((0)) FOR [Conferido]
GO


Alter table Caixa_2 Add CaixaItemId uniqueidentifier


GO


ALTER Procedure PROC_INSERT_NFCE  
 @VendaId Int  
AS  
BEGIN  
 Declare @EmpresaId int, @Serie int, @Modelo int, @NumeroNfce int   
 Set @Modelo = 65  
  
 Set @EmpresaId = (Select Loja From Venda_1 Where Nro = @VendaId)  
  
 --Pega Numero e serie da NFCe  
 Select   
  @NumeroNfce = NF_KEY_NFNRO ,  
  @Serie = NF_KEY_NFSERIE  
 From SIEMP_NF_KEY   
 Where NF_KEY_LOJA = @EmpresaId And NF_KEY_NFMOD = @Modelo  
  
  
 --Natureza Operacao  
 Declare @NaturezaId Int, @Natureza Varchar(100)  
  
 Select Top 1  
  @NaturezaId = IdNatureza,  
  @Natureza = Operacao  
 From NaturezaOperacao Where PadraoVenda = 1  
  
 --Cliente  
 Declare @ClienteId Int, @Nome Varchar(100)  
 Set @ClienteId = (Select COD_CLI From Venda_1 Where Nro = @VendaId)  
 Declare @ValorNF Decimal(18,2) = (Select VL_COMPRA From Venda_1 Where Nro = @VendaId)  
 Declare @UsuarioId Int = (Select Vend From Venda_1 Where Nro = @VendaId)  
 --Gravar NF1   
 Insert Into NF1(NF1_NRO, NF1_SERIE, NF1_MOD, NF1_EMP, NF1_DTEMI, NF1_DTSAI, NF1_HSAI,  
  NF1_INDPGTO, NF1_NATOP, NF1_TIPOOP, NF1_TIPOEMI, NF1_FINEMI, NF1_BICMS, NF1_VICMS,  
  NF1_BICMS_ST, NF1_VICMS_ST, NF1_VPROD, NF1_VFRETE, NF1_VDESC, NF1_VIPI, NF1_VOUTRO,  
  NF1_VNF, NF1_VPIS, NF1_VCOFINS, NF1_BISS, NF1_VISS, NF1_TRANSP,NF1_MODFRETE, NF1_TIPODEST,  
  NF1_CODDEST, NF1_DADOSADIC, NF1_VSERV, NF1_DADOSFISCO, NF1_SIT_DOC, NF1_TP_EMISSAO, NF1_IND_PRES,  
  NF1_CONS_FINAL, NF1_FIN_NFE, NF1_USUARIO)  
  
 Select   
  @NumeroNfce,  
  @Serie,  
  @Modelo,  
  @EmpresaId,  
  GetDate() as NF1_DTEMI,  
  GetDate() as NF1_DTSAI,  
  GetDate() as NF1_HSAI,  
  1 as NF1_INDPGTO,  
  @NaturezaId as Natureza,  
  1 as NF1_TIPOOP,  
  1 as NF1_TIPOEMI,  
  1 as NF1_FINEMI,  
  0 as NF1_BICMS,  
  0 as NF1_VICMS,  
  0 as NF1_BICMS_ST,  
  0 as NF1_VICMS_ST,  
  @ValorNF as NF1_VPROD,  
  0 as NF1_VFRETE,  
  0 as NF1_VDESC,  
  0 as NF1_VIPI,  
  0 as NF1_VOUTRO,  
  @ValorNF as NF1_VNF,  
  0 as NF1_VPIS,  
  0 as NF1_VCOFINS,  
  0 as NF1_BISS,  
  0 as NF1_VISS,  
  0 as NF1_TRANSP,  
  9 as NF1_MODFRETE,  
  'C' as NF1_TIPODEST,  
  @ClienteId as NF1_CODDEST,  
  '' as NF1_DADOSADIC,  
  0 as NF1_VSERV,  
  '' as NF1_DADOSFISCO,  
  '00' as NF1_SIT_DOC,  
  1 as NF1_TP_EMISSAO,  
  1 as NF1_IND_PRES,  
  1 as NF1_CONS_FINAL,  
  1 as NF1_FIN_NFE,  
  @UsuarioId as NF1_USUARIO  
  
 Declare @NF_Id Int = (Select @@Identity)  
 --Produtos  
 declare @table table(id int identity(1,1), vendaItemId Int)  
 Insert @table Select INC_VENDA2 From Venda_2 Where Nro = @VendaId  
  
 Declare @Inicio Int = (Select Min(id) From @table)  
 Declare @Final Int = (Select Max(id) From @table)  
  
 While(@Inicio <= @Final)  
 Begin  
  Declare @vendaItemId int = (Select vendaItemId From @table Where id = @Inicio)  
  Declare @produtoId int = (Select Cod_prod From Venda_2 Where INC_VENDA2 = @vendaItemId)  
  Declare @Quantidade int = (Select QTDE From Venda_2 Where INC_VENDA2 = @vendaItemId)  
  Declare @Unitario decimal(18,6) = (Select UNIT From Venda_2 Where INC_VENDA2 = @vendaItemId)  
  Declare @Total decimal(18,2) = (Select (UNIT*QTDE) From Venda_2 Where INC_VENDA2 = @vendaItemId)  
  Declare @ValorDesconto decimal(18,4)  = (Select (UNIT*QTDE)- TOTAL From Venda_2 Where INC_VENDA2 = @vendaItemId)
   
  Insert Into NF2(NF1_ID, NF1_NRO, NF1_SERIE, NF1_MOD, NF1_EMP, NF2_PRODCOD, NF2_PRODNOME, NF2_NCM, NF2_UN,  
   NF2_CFOP, NF2_QTDE, NF2_VUNITARIO, NF2_VTOTAL, NF2_VFRETE, NF2_VDESC, NF2_VOUTRO, NF2_CST_ICMS, NF2_TIPO,   
   NF2_ORIGEM, NF2_CSOSN)  
  Select   
   @NF_Id As NF1_ID  
   ,@NumeroNfce  
   ,@Serie  
   ,@Modelo  
   ,@EmpresaId  
   ,PROD_COD  
   ,PROD_NOME  
   ,PROD_NCM  
   ,PROD_UN  
   ,CFOP_SAT  
   ,@Quantidade  
   ,@Unitario  
   ,@Total  
   ,0 as Frete  
   ,Isnull(@ValorDesconto,0) as Desconto  
   ,0 as Outros  
   ,PROD_CST_ICMS  
   ,'P' as NF2_TIPO  
   ,PROD_ORIG as NF2_ORIGEM  
   ,PROD_CSOSN as NF2_CSOSN
   
  From VW_NF_PRODUTOS Where PROD_COD = @produtoId  
  
  
  Declare @csIcms Varchar(3) = (Select PROD_CST_ICMS From VW_NF_PRODUTOS Where PROD_COD = @produtoId)  
  Declare @csPis Varchar(3) = (Select PROD_CSTPIS_ID From VW_NF_PRODUTOS Where PROD_COD = @produtoId)  
  Declare @csCofins Varchar(3) = (Select PROD_CSTCOFINS_ID From VW_NF_PRODUTOS Where PROD_COD = @produtoId)  
  --Update Com os calculos de impostos  
  Declare @Nf2_Id int = (Select @@Identity)  
  Update NF2 Set  
   NF2.NF2_CST_IPI  = Isnull(RECALC.NF2_CST_IPI,''),  
   NF2.NF2_BIPI  = Isnull(RECALC.NF2_BIPI,0),  
   NF2.NF2_VIPI  = Isnull(RECALC.NF2_VIPI,0),  
   NF2.NF2_PIPI  = Isnull(RECALC.NF2_PIPI,0),  
   NF2.NF2_BICMS_ST = Isnull(RECALC.NF2_BICMS_ST,0),  
   NF2.NF2_VICMS_ST = Isnull(RECALC.NF2_VICMS_ST,0),  
   NF2.NF2_MVA_ST  = Isnull(RECALC.NF2_MVA_ST,0),  
   NF2.NF2_PICMS_ST = Isnull(RECALC.NF2_PICMS_ST,0),  
   NF2.NF2_PREDBCST = Isnull(RECALC.NF2_PREDBCST,0),  
   NF2.NF2_CST_ICMS = Isnull(RECALC.NF2_CST_ICMS,'60'),  
   NF2.NF2_BICMS  = Isnull(RECALC.NF2_BICMS,0),  
   NF2.NF2_VICMS  = Isnull(RECALC.NF2_VICMS,0),  
   NF2.NF2_PICMS  = Isnull(RECALC.NF2_PICMS,0),  
   NF2.NF2_REDICMS  = Isnull(RECALC.NF2_REDICMS,0),  
   NF2.NF2_CST_PIS  = Isnull(RECALC.NF2_CST_PIS,'04'),  
   NF2.NF2_BPIS  = Isnull(RECALC.NF2_BPIS,0),  
   NF2.NF2_VPIS  = Isnull(RECALC.NF2_VPIS,0),  
   NF2.NF2_PPIS  = Isnull(RECALC.NF2_PPIS,0),  
   NF2.NF2_CST_COFINS = Isnull(RECALC.NF2_CST_COFINS,'04'),  
   NF2.NF2_BCOFINS  = Isnull(RECALC.NF2_BCOFINS,0),  
   NF2.NF2_VCOFINS  = Isnull(RECALC.NF2_VCOFINS,0),  
   NF2.NF2_PCOFINS  = Isnull(RECALC.NF2_PCOFINS,0)  
  From dbo.FUNC_CALCULA_IMPOSTO_NFE(@EmpresaId, @ClienteId, 'C', @produtoId, @Total, 0, 0, @csIcms, 0, 0, 0, 0, '', 0,   
   @csPis, 0, @csCofins, 0, 0, 0, '') RECALC  
  Inner Join NF2 On NF2.NF2_ID = @Nf2_Id  
  
  
  Set @Inicio = @Inicio+1;  
 End  
  
 --Atualiza ValoresNF1  
 Update NF1 Set   
  NF1_BICMS  = (Select Sum(NF2_BICMS) From NF2 Where NF1_ID = @NF_Id),  
  NF1_VICMS  = (Select Sum(NF2_VICMS) From NF2 Where NF1_ID = @NF_Id),  
  NF1_BICMS_ST = (Select Sum(NF2_BICMS_ST) From NF2 Where NF1_ID = @NF_Id),  
  NF1_VICMS_ST = (Select Sum(NF2_VICMS_ST) From NF2 Where NF1_ID = @NF_Id),  
  NF1_VPROD  = (Select Sum(NF2_VTOTAL) From NF2 Where NF1_ID = @NF_Id),  
  NF1_VFRETE  = (Select Sum(NF2_VFRETE) From NF2 Where NF1_ID = @NF_Id),  
  NF1_VDESC  = (Select Sum(NF2_VDESC) From NF2 Where NF1_ID = @NF_Id),  
  NF1_VIPI  = (Select Sum(NF2_VIPI) From NF2 Where NF1_ID = @NF_Id),  
  NF1_VOUTRO  = (Select Sum(NF2_VOUTRO) From NF2 Where NF1_ID = @NF_Id),  
  NF1_VNF   = (Select Sum((NF2_VTOTAL+NF2_VOUTRO+NF2_VIPI+NF2_VICMS_ST)-NF2_VDESC) From NF2 Where NF1_ID = @NF_Id),  
  NF1_VPIS  = (Select Sum(NF2_VPIS) From NF2 Where NF1_ID = @NF_Id),  
  NF1_VCOFINS  = (Select Sum(NF2_VCOFINS) From NF2 Where NF1_ID = @NF_Id),  
  NF1_BISS  = (Select Sum(NF2_BISS) From NF2 Where NF1_ID = @NF_Id),  
  NF1_VISS  = (Select Sum(NF2_VISS) From NF2 Where NF1_ID = @NF_Id)  
 Where NF1_ID = @NF_Id  
  
  --Inserta NF4
  Insert NF4
	Select 
		@NumeroNfce as NF1_NRO,
		@Serie as NF1_SERIE,
		@Modelo as NF1_MOD,
		@EmpresaId as NF1_EMP,
		@VendaId as NF4_VENDA,
		0 as NF4_OS,
		@NF_Id as NF1_ID,
		0 as NF4_CONTRATO,
		0 as NF4_DEV


 --Atualiza Numero da NF  
 Update SIEMP_NF_KEY SET  
  NF_KEY_NFNRO = NF_KEY_NFNRO+1   
 Where NF_KEY_LOJA = @EmpresaId And NF_KEY_NFMOD = @Modelo  
  
 Select   
  @NF_Id as NfceId,   
  @NumeroNfce as NumeroNfce,  
  @Serie as Serie,  
  @Modelo as Modelo  
 --NF1  
END