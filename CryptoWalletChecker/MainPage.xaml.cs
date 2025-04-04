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
        public MainPage(IMethodsServices methods, ILogger<MainPage> logger)
        {
            InitializeComponent();
            if (!File.Exists(@"..\..\wallets.txt"))
            {
                using (StreamWriter streamWriter = new(@"..\..\wallets.txt", true))
                {
                    Console.WriteLine("Wallets.txt has been created");
                }
            }
            methodsServices = methods;
            _logger = logger;
            _logger.LogInformation("Application has started.");
        }
        private void OnWalletCheck(object sender, EventArgs e)
        {
            try
            {
                string input = textInput.Text;
                if (!methodsServices.IsSolanaWallet(input))
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
                        WalletExistLabel.Text = $"Wallet '{input}' was already registered, sum of transaction:";
                        WalletExistLabelSum.Text = $"{methodsServices.GetTransactionSum(input)} coins";
                    }
                    else
                    {
                        methodsServices.SetVisibilityTrueFalse(RegisterNonexistentWallet, CheckExistingWallet);
                        EnterSumLabel.Text = $"Enter sum to register transaction for";
                        EnterSumLabel2.Text = $"'{input}'";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(OnWalletCheck));
            }
        }
        private void OnYesConfirm(object sender, EventArgs e)
        {
            try
            {
                methodsServices.SetVisibilityTrueFalse(RegisterNonexistentWallet, RegisterExistingWallet);
                EnterSumLabel.Text = $"Enter sum to register transaction for";
                EnterSumLabel2.Text = $"'{textInput.Text}'";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(OnYesConfirm));
            }
        }
        private void OnNoConfirm(object sender, EventArgs e)
        {
            try
            {
                methodsServices.SetVisibilityTrueFalse(CheckExistingWallet, RegisterExistingWallet);
                textInput.Text = string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(OnNoConfirm));
            }
        }
        private void OnRegister(object sender, EventArgs e)
        {
            try
            {
                if(double.TryParse(textInput.Text, out double result))
                {
                    methodsServices.WriteToFile(result, textInput.Text);
                    methodsServices.SetVisibilityTrueFalse(CheckExistingWallet, RegisterNonexistentWallet);
                    SuccessLabel.IsVisible = true;
                    textInput.Text = string.Empty;
                    sumInput.Text = string.Empty;
                }
                else
                {
                    _logger.LogWarning("Invalid sum input: {SumInput}", sumInput.Text);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(OnRegister));
            }
        }
    }
}
