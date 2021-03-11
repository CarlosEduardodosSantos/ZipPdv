namespace Zip.Pdv
{
    public static class ValidacaoHelper
    {
        public static bool ValidarEan13(this string CodigoEAN13)
        {
            bool result = (CodigoEAN13.Length == 13);
            if (result)
            {
                const string checkSum = "131313131313";

                int digito = int.Parse(CodigoEAN13[CodigoEAN13.Length - 1].ToString());
                string ean = CodigoEAN13.Substring(0, CodigoEAN13.Length - 1);

                int sum = 0;
                for (int i = 0; i <= ean.Length - 1; i++)
                {
                    sum += int.Parse(ean[i].ToString()) * int.Parse(checkSum[i].ToString());
                }
                int calculo = 10 - (sum % 10);
                result = (digito == calculo);
            }
            return result;
        }
        /// <summary>
        /// Se todos os caracters forem digitos então é numerico
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string data)
        {
            bool isnumeric = false;
            char[] datachars = data.ToCharArray();

            foreach (var datachar in datachars)
                isnumeric = isnumeric ? char.IsDigit(datachar) : isnumeric;


            return isnumeric;
        }
    }
}