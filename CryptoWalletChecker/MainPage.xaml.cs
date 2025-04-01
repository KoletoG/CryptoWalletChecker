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
            if (IsWalletExists(textInput.Text))
            {
                ButtonCheck.IsVisible = false;
                textInput.IsVisible = false;
                EnterWalletLabel.IsVisible = false;
                WalletExistLabel.IsVisible = true;
                WalletExistLabel.Text = $"Wallet has had transactions registered, sum of all transactions: {GetTransactionSum(textInput.Text)}";
            }
            else
            {
                EnterSumLabel.IsVisible = true;
                ButtonRegister.IsVisible = true;
                sumInput.IsVisible = true;
                ButtonCheck.IsVisible = false;
                textInput.IsVisible = false;
                EnterWalletLabel.IsVisible = false;
                EnterSumLabel.Text = $"Do you want to register transaction for \n {textInput.Text}";
            }
            DisplayAlert("Input Received", $"You entered: {textInput.Text}", "OK");
        }
        private string GetTransactionSum(string wallet)
        {
            string[] line = File.ReadLines(@"..\..\wallets.txt").Where(x => x == wallet).Single().Split(' ');
            return line[1];
        }
        private void OnRegister(object sender, EventArgs e)
        {
        }
        private bool IsWalletExists(string wallet)
        {
            return File.ReadLines(@"..\..\wallets.txt").Contains(wallet);
        }
    }

}
