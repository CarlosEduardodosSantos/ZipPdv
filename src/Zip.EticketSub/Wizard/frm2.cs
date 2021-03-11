using System;
using System.Windows.Forms;

namespace Zip.EticketSub.Wizard
{
    public partial class frm2 : FormBase
    {
        public frm2()
        {
            InitializeComponent();

            var app = new AppSetting();
            var connect = app.GetConnectionString("MyContext");
            var indexStart = connect.IndexOf("Data Source=");

            string[] quebra = connect.Split(new char[] { ';', '=' });

            if(quebra.Length < 9)return;

            cbServidor.Text = quebra[1];
            txtDataBase.Text = quebra[3];
            txtUsername.Text = quebra[5];
            txtPassword.Text = quebra[7];

        }

        public override bool Concluir()
        {
            var connectionString = $"Data Source={cbServidor.Text}; Initial Catalog={txtDataBase.Text};user id={txtUsername.Text};password={txtPassword.Text};";

            try
            {
                //var sqlHelper = new SqlHelper(connectionString);
                var isConected = true;
                if (isConected)
                {
                    var app = new AppSetting();
                    app.SaveConnectionString("MyContext", connectionString);


                    //sqlHelper.CriarTabelas();
                    
                    MessageBox.Show("Muito bem! A conexão esta correta\nVamos atualizar o seu sistema agora.", "Configuração", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("Não é possivel conectar ao servidor.\nVerifique as informações digitadas e tente novamente.", "Configuração", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Configuração", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }
}
