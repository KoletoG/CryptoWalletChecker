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
        private Dictionary<string, double> _walletSums = new();
        public MethodsService(Logger<MethodsService> logger)
        {
            _logger = logger;
        }
        public void LoadWalletCache()
        {
            var lines = File.ReadAllLines(@"..\..\wallets.txt");
            for (int i = 0; i < lines.Length; i+=2) 
            {
                if(double.TryParse(lines[i + 1], out double result))
                {
                    if (_walletSums.ContainsKey(lines[i]))
                    {
                        _walletSums[lines[i]] += result;
                    }
                    else
                    {
                        _walletSums[lines[i]]= result;
                    }
                }
            }
        }
        public bool IsSolanaWallet(string wallet)
        {
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
        public void WriteToFile(double number, string wallet)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(@"..\..\wallets.txt", true))
                {
                    streamWriter.WriteLine(wallet);
                    streamWriter.WriteLine(number);
                }
                if (_walletSums.ContainsKey(wallet))
                {
                    _walletSums[wallet] += number;
                }
                else
                {
                    _walletSums[wallet] = number;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(WriteToFile));
            }
        }
        public double GetTransactionSum(string wallet)
        {
            try
            {
                return _walletSums[wallet];
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
                return _walletSums.ContainsKey(wallet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(IsWalletExists));
                throw new Exception(ex.ToString());
            }
        }
    }
}
