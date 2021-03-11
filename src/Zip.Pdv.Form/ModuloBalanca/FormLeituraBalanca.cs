using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zip.Pdv.ModuloBalanca
{
    public partial class FormLeituraBalanca : Form
    {
        private double _peso;
        public FormLeituraBalanca()
        {
            InitializeComponent();
        }

        public static double ObterPeso()
        {
            using (var form = new FormLeituraBalanca())
            {
                form.ShowDialog();
                return form._peso;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var balancaPeso = new Leitura();
            _peso = balancaPeso.ObterPeso();
            txtValor.ValueNumeric = (decimal)_peso;

            Thread.Sleep(2000);

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        private void FormLeituraBalanca_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }
    }
}
