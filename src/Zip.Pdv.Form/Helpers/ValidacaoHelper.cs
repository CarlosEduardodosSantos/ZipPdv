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

        public static bool ValidarCpf(string cpf)
        {
            if (cpf.Length > 11)
                return false;

            while (cpf.Length != 11)
                cpf = '0' + cpf;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }

        public static bool ValidarCNPJ(string cnpj)
        {
            var mt1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var mt2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            if (cnpj == "00000000000000" || cnpj == "11111111111111" ||
             cnpj == "22222222222222" || cnpj == "33333333333333" ||
             cnpj == "44444444444444" || cnpj == "55555555555555" ||
             cnpj == "66666666666666" || cnpj == "77777777777777" ||
             cnpj == "88888888888888" || cnpj == "99999999999999")
                return false;

            var TempCNPJ = cnpj.Substring(0, 12);
            var soma = 0;

            for (var i = 0; i < 12; i++)
                soma += int.Parse(TempCNPJ[i].ToString()) * mt1[i];

            var resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var digito = resto.ToString();

            TempCNPJ = TempCNPJ + digito;
            soma = 0;
            for (var i = 0; i < 13; i++)
                soma += int.Parse(TempCNPJ[i].ToString()) * mt2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto;

            return cnpj.EndsWith(digito);
        }
    }
}