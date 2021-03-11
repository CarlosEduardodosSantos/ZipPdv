using System;
using System.Drawing;

namespace Zip.Pdv
{
    public static class ColorHelper
    {
        private static Random rnd = new Random();
        public static Color ObterColor()
        {
            Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            return randomColor;
        }

        public static Color ObterColorFonte(Color color)
        {
            var result = (color.R * 299 + color.G * 587 + color.B * 114) / 1000;
            return result > 128 ? Color.Black : Color.White;
        }
    }
}