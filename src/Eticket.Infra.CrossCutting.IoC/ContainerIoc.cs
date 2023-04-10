using CommonServiceLocator.SimpleInjectorAdapter;
using Eticket.Application;
using Eticket.Application.CartaoConsumo;
using Eticket.Application.Interface;
using Eticket.Domain.Interface;
using Eticket.Domain.Interface.Repository;
using Eticket.Infra.Data.Repository;
using Microsoft.Practices.ServiceLocation;
using SimpleInjector;

namespace Eticket.Infra.CrossCutting.IoC
{
    public class ContainerIoc
    {
        public ContainerIoc()
        {
            var adapter = new SimpleInjectorServiceLocatorAdapter(GetModule());
            ServiceLocator.SetLocatorProvider(() => adapter);
        }

        public Container GetModule()
        {
            // 1. Create a new Simple Injector container
            var container = new Container();

            //Domain
            container.Register<IProdutoRepository, ProdutoRepository>();
            container.Register<IProdutoGrupoRepository, ProdutoGrupoRepository>();
            container.Register<IVendaFichaRepository, VendaFichaRepository>();
            container.Register<IFichaRepository, FichaRepository>();
            container.Register<IVendedorRepository, VendedorRepository>();
            container.Register<IVendaComplementoRepository, VendaComplementoRepository>();
            container.Register<IProdutoComplementoRepository, ProdutoComplementoRepository>();
            container.Register<IUsuarioRepository, UsuarioRepository>();
            container.Register<IEspeciePagamentoRepository, EspeciePagamentoRepository>();
            container.Register<ICaixaRepository, CaixaRepository>();
            container.Register<ICaixaItemRepository, CaixaItemRepository>();
            container.Register<IVendaRepository, VendaRepository>();
            container.Register<INfceServicoRepository, NfceServicoRepository>();
            container.Register<IEmpresaRepository, EmpresaRepository>();
            container.Register<ICartaoRequisicaoRepository, CartaoRequisicaoRepository>();
            container.Register<ICartaoRespostaRepository, CartaoRespostaRepository>();
            container.Register<IFabricaRelatorioRepository, FabricaRelatorioRepository>();
            container.Register<ICaixaFechamentoRepository, CaixaFechamentoRepository>();
            container.Register<IClienteDeliveryRepository, ClienteDeliveryRepository>();
            container.Register<IEntregadorRepository, EntregadorRepository>();
            container.Register<IDeliveryRepository, DeliveryRepository>();
            container.Register<IUnidadeMedidaRepository, UnidadeMedidaRepository>();
            container.Register<IProdutoTipoRepository, ProdutoTipoRepository>();
            container.Register<IRetornoSatRepository, RetornoSatRepository>();
            container.Register<IConfiguracaoSistemaRepository, ConfiguracaoSistemaRepository>();
            container.Register<IVendaPendenteRepository, VendaPendenteRepository>();
            container.Register<IProdutoOpcaoRepository, ProdutoOpcaoRepository>();
            container.Register<IClienteRepository, ClienteRepository>();
            container.Register<ICadMesasRepository, CadMesasRepository>();
            container.Register<IOpMesa1Repository, OpMesa1Repository>();
            container.Register<IOpMesa2Repository, OpMesa2Repository>();
            container.Register<ICadGarcomRepository, CadGarcomRepository>();

            //App
            container.Register<IProdutoAppService, ProdutoAppService>();
            container.Register<IProdutoGrupoAppService, ProdutoGrupoAppService>();
            container.Register<IVendaFichaAppService, VendaFichaAppService>();
            container.Register<IFichaAppService, FichaAppService>();
            container.Register<IVendedorAppService, VendedorAppService>();
            container.Register<IVendaComplementoAppService, VendaComplementoAppService>();
            container.Register<IProdutoComplementoAppService, ProdutoComplementoAppService>();
            container.Register<IUsuarioAppService, UsuarioAppService>();
            container.Register<IEspeciePagamentoAppService, EspeciePagamentoAppService>();
            container.Register<ICaixaAppService, CaixaAppService>();
            container.Register<ICaixaItemAppService, CaixaItemAppService>();
            container.Register<IVendaAppService, VendaAppService>();
            container.Register<INfceServicoAppService, NfceServicoAppService>();
            container.Register<IEmpresaAppService, EmpresaAppService>();
            container.Register<ICartaoRequisicaoAppService, CartaoRequisicaoAppService>();
            container.Register<ICartaoRespostaAppService, CartaoRespostaAppService>();
            container.Register<IFabricaRelatorioAppService, FabricaRelatorioAppService>();
            container.Register<ICaixaFechamentoAppService, CaixaFechamentoAppService>();
            container.Register<IClienteDeliveryAppService, ClienteDeliveryAppService>();
            container.Register<IEntregadorAppService, EntregadorAppService>();
            container.Register<IDeliveryAppService, DeliveryAppService>();
            container.Register<IUnidadeMedidaAppService, UnidadeMedidaAppService>();
            container.Register<IProdutoTipoAppService, ProdutoTipoAppService>();
            container.Register<IRetornoSatAppService, RetornoSatAppService>();
            container.Register<IConfiguracaoSistemaAppService, ConfiguracaoSistemaAppService>();
            container.Register<IVendaPendenteAppService, VendaPendenteAppService>();
            container.Register<IProdutoOpcaoAppService, ProdutoOpcaoAppService>();
            container.Register<IClienteAppService, ClienteAppService>();
            container.Register<IDataHoraAtualAppService, DataHoraAtualAppService>();
            container.Register<ICadMesasAppService, CadMesasAppService>();
            container.Register<IOpMesa1AppService, OpMesa1AppService>();
            container.Register<IOpMesa2AppService, OpMesa2AppService>();
            container.Register<ICadGarcomAppService, CadGarcomAppService>();

            return container;
        }

    }
}