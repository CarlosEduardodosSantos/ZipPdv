using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zip.EticketSub
{
    public static class VerificaArquivo
    {
        public static string[] ConsultaArquivos(string folderProcessar)
        {
            var dir = !Directory.Exists(folderProcessar)
                ? Directory.CreateDirectory(folderProcessar)
                : new DirectoryInfo(folderProcessar);

            return dir.GetFiles("*.txt").Select(fileName => fileName.Name).ToArray();
        }

    }
}