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
        int count = 0;

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
                
            }
            else
            {

            }
            ButtonRegister.IsVisible = true;
                DisplayAlert("Input Received", $"You entered: {textInput.Text}", "OK");
        }
        private bool IsWalletExists(string wallet)
        {
            return File.ReadLines(@"..\..\wallets.txt").Contains(wallet);
        }
    }

}
