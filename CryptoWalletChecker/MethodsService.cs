using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Maui;

namespace CryptoWalletChecker
{
    public class MethodsService : IMethodsServices
    {
        private ILogger<MethodsService> _logger;
        public MethodsService(Logger<MethodsService> logger) {
            _logger = logger;
        }
        public void WriteToFile(int number, string wallet)
        {
            using (StreamWriter streamWriter = new StreamWriter(@"..\..\wallets.txt", true))
            {
                streamWriter.WriteLine(wallet);
                streamWriter.WriteLine(number);
            }
        }
        public int GetTransactionSum(string wallet)
        {
            int sum = 0;
            using (StreamReader streamReader = new StreamReader(@"..\..\wallets.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    if (streamReader.ReadLine() == wallet)
                    {
                        sum += int.Parse(streamReader.ReadLine());
                    }
                }
                return sum;
            }
        }
        public void SetVisibilityTrueFalse(VerticalStackLayout vsl1, VerticalStackLayout vsl2)
        {
            vsl1.IsVisible = true;
            vsl2.IsVisible = false;
        }
        public bool IsWalletExists(string wallet)
        {
            return File.ReadLines(@"..\..\wallets.txt").Contains(wallet);
        }
    }
}
