using System;
using Eticket.Application.ViewModels;
using System.Threading;
using System.Windows;
using Zip.Toten.ViewModel;
using System.Diagnostics;

namespace Zip.Toten
{
    /// <summary>
    /// Interaction logic for FormProd.xaml
    /// </summary>
    public partial class FormProd : Window
    {
        public static VendaViewModel VendaViewModel;
        public FormProd()
        {
            InitializeComponent();
        }
    }
}
