using System.Collections.Immutable;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.Maui.Graphics.Text;
using Color = Microsoft.Maui.Graphics.Color;
using Point = Microsoft.Maui.Graphics.Point;

namespace CryptoWalletChecker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            using (StreamWriter streamWriter = new StreamWriter(@"..\..\wallets.txt", true))
            {
                Console.WriteLine("Wallets.txt has been created");
            }
        }
        private void OnWalletCheck(object sender, EventArgs e)
        {
            SuccessLabel.IsVisible = false;
            if (IsWalletExists(textInput.Text))
            {
                RegisterExistingWallet.IsVisible = true;
                WalletExistLabel.Text = $"Wallet '{textInput.Text}' was already registered, sum of transaction:";
                WalletExistLabelSum.Text=$"{GetTransactionSum(textInput.Text)} coins";
                RegisterNonexistentWallet.IsVisible = false;
                CheckExistingWallet.IsVisible = false;
            }
            else
            {
                CheckExistingWallet.IsVisible = false;
                RegisterNonexistentWallet.IsVisible = true;
                EnterSumLabel.Text = $"Enter sum to register transaction for";
                EnterSumLabel2.Text = $"'{textInput.Text}'";
            }
        }
        private void OnYesConfirm(object sender, EventArgs e)
        {
            RegisterExistingWallet.IsVisible = false;
            RegisterNonexistentWallet.IsVisible = true;
            EnterSumLabel.Text = $"Enter sum to register transaction for";
            EnterSumLabel2.Text = $"'{textInput.Text}'";
        }
        private void OnNoConfirm(object sender, EventArgs e)
        {
            CheckExistingWallet.IsVisible = true;
            RegisterExistingWallet.IsVisible = false;
            textInput.Text = string.Empty;
        }
        private int GetTransactionSum(string wallet)
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
        private void OnRegister(object sender, EventArgs e)
        {
            WriteToFile(sumInput.Text, textInput.Text);
            CheckExistingWallet.IsVisible = true;
            RegisterNonexistentWallet.IsVisible = false;
            textInput.Text = string.Empty;
            sumInput.Text = string.Empty;
            SuccessLabel.IsVisible = true;
            
        }
        private bool IsWalletExists(string wallet)
        {
            return File.ReadLines(@"..\..\wallets.txt").Contains(wallet);
        }
        private void WriteToFile(string number, string wallet)
        {
            using (StreamWriter streamWriter = new StreamWriter(@"..\..\wallets.txt",true))
            {
                streamWriter.WriteLine(wallet);
                streamWriter.WriteLine(number);
            }
        }
    }
}
