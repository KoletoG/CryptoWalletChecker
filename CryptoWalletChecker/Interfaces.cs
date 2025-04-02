using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWalletChecker
{
    public interface IMethodsServices
    {
        void WriteToFile(int number, string wallet);
        int GetTransactionSum(string wallet);
        bool IsWalletExists(string wallet);
        void SetVisibilityTrueFalse(VerticalStackLayout vsl1, VerticalStackLayout vsl2);
    }
}
