using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zip.Pdv.Cadastro.Model;

namespace Zip.Pdv.Cadastro
{
    public partial class CadastroBase : UserControl
    {
        public object DataSource;
        public bool IsEdicao;
        public ResultValidModel ResultValid;
        public event EventHandler<EventArgs> Adicionar;
        protected void adicionar(object sender, EventArgs e)
        {
            var completedEvent = Adicionar;
            if (completedEvent != null)
            {
                var item = DataSource;
                completedEvent(item, e);
            }
        }
        public CadastroBase()
        {
            InitializeComponent();
            ResultValid = new ResultValidModel();
        }
        
        public virtual object ObjetoToClass()
        {
            return null;
        }
        public virtual void ClassToObjeto(object objeto)
        {
            //
        }

        public virtual void TravaDestrava(bool operacao)
        {
            //
        }
        public virtual void LimparTudo()
        {
            //
        }
        public virtual bool ValidaCadastro()
        {
            return true;
        }

        public void AdicionaErroValidacao(string erro)
        {
            ResultValid.ResultValidItens.Add(new ResultValidItemModel(){Menssage = erro});
        }
    }
}
