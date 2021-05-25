using System;
using System.Configuration;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Infra.CrossCutting.IoC;
using SimpleInjector;
using Zip.Sat;

namespace Zip.Toten
{
    static class Program
    {
        public static Container Container;

        public static InicializacaoViewAux InicializacaoViewAux;
        public static UsuarioViewModel Usuario;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
    }
}
