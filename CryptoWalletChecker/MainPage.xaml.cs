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
            WalletExistLabel.IsVisible = false;
            if (IsWalletExists(textInput.Text))
            {
                EnterSumLabel.IsVisible = false;
                ButtonRegister.IsVisible = false;
                sumInput.IsVisible = false;
                WalletExistLabel.IsVisible = true;
                WalletExistLabel.Text = $"Wallet has had transactions registered, sum of transaction: \n {GetTransactionSum(textInput.Text)}";
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
        }
        private string GetTransactionSum(string wallet)
        {
            using (StreamReader streamReader = new StreamReader(@"..\..\wallets.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    if (streamReader.ReadLine() == wallet)
                    {
                        return streamReader.ReadLine() ?? "0";
                    }
                }
                return null;
            }
        }
        private void OnRegister(object sender, EventArgs e)
        {
            WriteToFile(sumInput.Text, textInput.Text);
            ButtonCheck.IsVisible = true;
            textInput.IsVisible = true;
            EnterWalletLabel.IsVisible = true;
            EnterSumLabel.IsVisible = false;
            ButtonRegister.IsVisible = false;
            sumInput.IsVisible = false;
            textInput.Text = "";
            DisplayAlert("TEST","TEST","Test");
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
