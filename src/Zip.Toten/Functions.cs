using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zip.Toten
{
    public static class Functions
    {
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
    }
}
