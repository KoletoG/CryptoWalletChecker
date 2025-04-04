using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWalletChecker
{
    public interface IMethodsServices
    {
        void WriteToFile(double number, string wallet);
        double GetTransactionSum(string wallet);
        bool IsWalletExists(string wallet);
        void SetVisibilityTrueFalse(VerticalStackLayout vsl1, VerticalStackLayout vsl2);
        bool IsSolanaWallet(string wallet);
    }
}
