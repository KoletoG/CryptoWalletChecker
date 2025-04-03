using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Maui;
using Solnet.Wallet;

namespace CryptoWalletChecker
{
    public class MethodsService : IMethodsServices
    {
        private ILogger<MethodsService> _logger;
        public MethodsService(Logger<MethodsService> logger)
        {
            _logger = logger;
        }
        public bool IsSolanaWallet(string wallet)
        {
            if (wallet.Length != 44 || String.IsNullOrEmpty(wallet))
            {
                return false;
            }
            try
            {
                PublicKey publicKey = new PublicKey(wallet);
                return publicKey.IsValid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(IsSolanaWallet));
            }
            return false;
        }
        public void WriteToFile(int number, string wallet)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(@"..\..\wallets.txt", true))
                {
                    streamWriter.WriteLine(wallet);
                    streamWriter.WriteLine(number);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(WriteToFile));
            }
        }
        public int GetTransactionSum(string wallet)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(GetTransactionSum));
                throw new Exception(ex.ToString());
            }
        }
        public void SetVisibilityTrueFalse(VerticalStackLayout vsl1, VerticalStackLayout vsl2)
        {
            try
            {
                vsl1.IsVisible = true;
                vsl2.IsVisible = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
        public bool IsWalletExists(string wallet)
        {
            try
            {
                return File.ReadLines(@"..\..\wallets.txt").Contains(wallet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(IsWalletExists));
                throw new Exception(ex.ToString());
            }
        }
    }
}
