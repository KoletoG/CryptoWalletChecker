using System.Collections.Immutable;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Graphics.Text;
using Color = Microsoft.Maui.Graphics.Color;
using Point = Microsoft.Maui.Graphics.Point;

namespace CryptoWalletChecker
{
    public partial class MainPage : ContentPage
    {
        private IMethodsServices methodsServices;
        private ILogger<MainPage> _logger;
        public MainPage(IMethodsServices methods,ILogger<MainPage> logger)
        {
            InitializeComponent();
            using (StreamWriter streamWriter = new StreamWriter(@"..\..\wallets.txt", true))
            {
                Console.WriteLine("Wallets.txt has been created");
            }
            methodsServices = methods;
            _logger = logger;
        }
        private void OnWalletCheck(object sender, EventArgs e)
        {
            if (textInput.Text.Length < 25 || textInput.Text.Length > 103)
            {
                InvalidWalletLabel.IsVisible = true;
            }
            else
            {
                InvalidWalletLabel.IsVisible = false;
                SuccessLabel.IsVisible = false;
                if (methodsServices.IsWalletExists(textInput.Text))
                {
                    methodsServices.SetVisibilityTrueFalse(RegisterExistingWallet, CheckExistingWallet);
                    WalletExistLabel.Text = $"Wallet '{textInput.Text}' was already registered, sum of transaction:";
                    WalletExistLabelSum.Text = $"{methodsServices.GetTransactionSum(textInput.Text)} coins";
                }
                else
                {
                    methodsServices.SetVisibilityTrueFalse(RegisterNonexistentWallet, CheckExistingWallet);
                    EnterSumLabel.Text = $"Enter sum to register transaction for";
                    EnterSumLabel2.Text = $"'{textInput.Text}'";
                }
            }
        }
        private void OnYesConfirm(object sender, EventArgs e)
        {
            methodsServices.SetVisibilityTrueFalse(RegisterNonexistentWallet, RegisterExistingWallet);
            EnterSumLabel.Text = $"Enter sum to register transaction for";
            EnterSumLabel2.Text = $"'{textInput.Text}'";
        }
        private void OnNoConfirm(object sender, EventArgs e)
        {
            methodsServices.SetVisibilityTrueFalse(CheckExistingWallet, RegisterExistingWallet);
            textInput.Text = string.Empty;
        }
        private void OnRegister(object sender, EventArgs e)
        {
            methodsServices.WriteToFile(int.Parse(sumInput.Text), textInput.Text);
            methodsServices.SetVisibilityTrueFalse(CheckExistingWallet, RegisterNonexistentWallet);
            SuccessLabel.IsVisible = true;
            textInput.Text = string.Empty;
            sumInput.Text = string.Empty;
        }
    }
}
