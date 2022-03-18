using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using Zip.Pdv.Component;
using MessageBox = System.Windows.MessageBox;


namespace Zip.Pdv
{
    public static class Funcoes
    {
        public static DataTable ConverteListParaDataTable<T>(List<T> list, string tableNome)
        {
            DataTable dt = new DataTable(tableNome);

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                Type pt = info.PropertyType;
                if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>))
                    pt = Nullable.GetUnderlyingType(pt);

                dt.Columns.Add(new DataColumn(info.Name, pt));
            }
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    object value = info.GetValue(t, null);
                    if (value != null)
                        row[info.Name] = value;

                    //row[info.Name] = info.GetValue(t, null);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        ///     Exibe um diálogo com uma mensagem para o usuário, utilizando um ModernDialog
        /// </summary>
        /// <param name="mensagem"></param>
        /// <param name="titulo"></param>
        /// <param name="botoes"></param>
        /// <param name="imagem"></param>
        public static MessageBoxResult Mensagem(string mensagem, string titulo, MessageBoxButton botoes, MessageBoxImage imagem = MessageBoxImage.None)
        {
            return MessageBox.Show(mensagem, titulo, botoes, imagem);
        }
        public static void MensagemInformation(string mensagem)
        {
            TouchMessageBox.Show(mensagem, "Zip Pdv", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void MensagemError(string mensagem)
        {
            TouchMessageBox.Show(mensagem, "Zip Pdv", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static DialogResult MensagemQuestao(string mensagem)
        {
            return TouchMessageBox.Show(mensagem, "Zip Pdv", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }
        public static Image Base64ToImage(string base64String)

        {

            // Convert Base64 String to byte[]

            byte[] imageBytes = Convert.FromBase64String(base64String);

            MemoryStream ms = new MemoryStream(imageBytes, 0,

                imageBytes.Length);


            // Convert byte[] to Image

            ms.Write(imageBytes, 0, imageBytes.Length);

            Image image = Image.FromStream(ms, true);

            return image;

        }

        public static string OnlyNumeric(string texto)
        {
            return String.Join("", System.Text.RegularExpressions.Regex.Split(texto, @"[^\d]"));
        }
    }
}