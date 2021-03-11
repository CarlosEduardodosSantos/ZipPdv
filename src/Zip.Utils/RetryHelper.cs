using System;
using System.Threading.Tasks;

namespace Zip.Utils
{
    public class RetryHelper
    {

        public static void RetryOnException(int times, TimeSpan delay, Action operation)
        {
            var attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    operation();
                    break; // Sucess! Lets exit the loop!
                }
                catch (Exception ex)
                {
                    if (attempts == times)
                        throw;

                    //Funcoes.Mensagem($"Ocorreu um erro na tentativa de {attempts}\n  uma nova tentativa sera feita em {delay} \n{ex}", "", MessageBoxButton.OK);

                    Task.Delay(delay).Wait();
                }
            } while (true);
        }
    }
}