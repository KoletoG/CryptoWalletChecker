using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui;

namespace CryptoWalletChecker
{
    public class MethodsService : IMethodsServices
    {
        private MainPage _mainPage;
        public MethodsService(MainPage mainPage) {
        _mainPage = mainPage;
        }
        public void WalletExistsLogic()
        { 
        }

    }
}
