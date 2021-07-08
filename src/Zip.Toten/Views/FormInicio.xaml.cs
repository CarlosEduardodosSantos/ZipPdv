using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Zip.Toten.ViewModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zip.Toten.Views
{
    /// <summary>
    /// Interaction logic for FormInicio1.xaml
    /// </summary>
    public partial class FormInicio : Window
    {
        public FormInicio()
        {
            InitializeComponent();
            this.DataContext = new InicioViewModel();
            App.VendaViewModel = new Eticket.Application.ViewModels.VendaViewModel();
        }
    }
}