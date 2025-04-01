using System.Runtime.InteropServices.Marshalling;

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

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            string userInput = textInput.Text;
            DisplayAlert("Input Received", $"You entered: {userInput}", "OK");
        }
        private bool IsWalletExists(string wallet)
        {
            bool exists = false;
            using (StreamReader str = new StreamReader(@"..\..\wallets.txt"))
            {
                while (!str.EndOfStream) 
                {
                    if (str.ReadLine() == wallet)
                    {
                        exists = true;
                    }
                }
            }
            return exists;
        }
    }

}
