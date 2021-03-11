
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
