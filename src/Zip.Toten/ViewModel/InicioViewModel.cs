using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Toten.Services;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace Zip.Toten.ViewModel
{
    public class InicioViewModel : INotifyPropertyChanged
    {

        public static VendaViewModel VendaViewModel;

        public event PropertyChangedEventHandler PropertyChanged;

        //Propriedades
        public ProdutoGrupoViewModel Grupo { get; set; }
        public ProdutoViewModel Produto { get; set; }

        public ObservableCollection<VendaItemViewModel> Carrinho { get; set; }
        public List<ProdutoGrupoViewModel> Grupos { get; set; }
        public List<ProdutoViewModel> Produtos { get; set; }

        public List<ProdutoViewModel> ProdFiltrados { get; set; }
        public ICommand OpenWindow { get; set; }
        public ICommand Power { get; set; }
        public ICommand Close { get; set; }
        public ICommand Plus { get; set; }
        public ICommand Minus { get; set; }
        public ICommand AddCart { get; set; }
        public ICommand RunCommand { get; set; }

        public ICommand DeleteProd { get; set; }

        public int Quantidade { get; set; } 


        private ProdutoGrupoViewModel _selectedGroup;
        private ProdutoViewModel _selectedProd;
        private VendaItemViewModel _selectedDelete;

        public int TotalCarrinho;
        private string productSearchPattern;

        public string ProductSearchPattern
        {
            get { return productSearchPattern; }
            set
            {
                if (productSearchPattern == "")
                {
                    ProdFiltrados = Produtos;
                }
                else
                productSearchPattern = value;
                RaiseChange("ProdFiltrados");
                FiltrarProdutos(value);
                productSearchPattern = value;
            }
        }

        //Comandos via Binding da View
        public ProdutoGrupoViewModel SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (value == _selectedGroup) return;
                _selectedGroup = value;
                CarregaProdutos(_selectedGroup.GrupoId);
                RaiseChange("SelectedGroup");
            }
        }
        public VendaItemViewModel SelectedDelete
        {
            get { return _selectedDelete; }
            set
            {
                if (value == _selectedDelete) return;
                _selectedDelete = value;
                RaiseChange("SelectedDelete");
            }
        }


        public ProdutoViewModel SelectedProd
        {
            get { return _selectedProd; }
            set
            {
                if (value == _selectedProd) 
                    return;
                if (value == null)
                {
                    _selectedProd = value;
                    RaiseChange("SelectedProd");
                }
                else
                {
                    _selectedProd = value;
                    RaiseChange("SelectedProd");
                    OpenAba(_selectedProd);
                }

            }
        }

        public InicioViewModel()
        {
            BuscaGrupos();
            OpenWindow = new RelayCommand(OpenAba);
            Power = new RelayCommand(Shutdown);
            Close = new RelayCommand(CloseThis);
            AddCart = new RelayCommand(AddItensCarrinho);
            Plus = new RelayCommand(AddQntd);
            Minus = new RelayCommand(LessQntd);
            DeleteProd = new RelayCommand<VendaItemViewModel>(RemoveItemCarrinho);
            TotalCarrinho = 0;
        }
        private void LessQntd(object obj)
        {
            if (Quantidade == 0)
                return;
            else
            Quantidade -= 1;
            RaiseChange("Quantidade");
        }

        private void AddQntd(object obj)
        {
            Quantidade += 1;
            RaiseChange("Quantidade");
        }

        private void FiltrarProdutos(string text)
        {
            if (text == "")
            {
                ProdFiltrados = Produtos;
            }
            else
            ProdFiltrados = Produtos.Where(f => f.Descricao.Contains(text)).ToList();
            RaiseChange("ProductSearchPattern");
            RaiseChange("ProdFiltrados");
        }

        private void CloseThis(object obj)
        {
            Application.Current.Windows[1].Close();
            Application.Current.Windows[1].Close();
            this.SelectedProd = null;
            RaiseChange("Close");
        }

        void OpenAba(object obj)
        {
            var pw = new FormProd();
            pw.DataContext = this;
            Quantidade = 1;
            pw.ShowDialog();
            RaiseChange("OpenWindow");
        }

        private void BuscaGrupos()
        { 
            using (var appServer = App.Container.GetInstance<IProdutoGrupoAppService>())
            {
                Grupos = appServer.ObterTodos().ToList();
                RaiseChange("Grupos");
            }
        }

        private void CarregaProdutos(int grupoId)
        {
            using (var appServer = App.Container.GetInstance<IProdutoAppService>())
            {                
                Produtos = appServer.ObterPorGrupoId(int.Parse(grupoId.ToString())).ToList();
            }
            ProdFiltrados = Produtos;
            RaiseChange("Produtos");
            RaiseChange("ProdFiltrados");
            ProductSearchPattern = "";

        }

        private void Shutdown(object obj)
        {
            Application.Current.Shutdown();
            RaiseChange("Power");
        }

        private void CarregaCarrinho()
        {
            Carrinho = new ObservableCollection<VendaItemViewModel>(App.VendaViewModel.VendaItens);
            RaiseChange("Carrinho");
        }

        private void AddItensCarrinho(object obj)
        {
            App.VendaViewModel.VendaItens.Add(new Eticket.Application.ViewModels.VendaItemViewModel()
            {
                ValorUnitatio = _selectedProd.ValorVenda,
                ProdutoViewModel = _selectedProd,
                ProdutoId = _selectedProd.ProdutoId,
                Quantidade = Quantidade,
            });
            CarregaCarrinho();
        }

        private void RemoveItemCarrinho(VendaItemViewModel model)
        {
            App.VendaViewModel.VendaItens.Remove(model);
            CarregaCarrinho();
        }

        //Comunica a View de alguma mudança, pode ser adaptado para receber um object com o 
        //nome do método ao invés da string com o nome da propriedade, questão de gosto.
        public void RaiseChange(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
