namespace CryptoWalletChecker
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            string userInput = textInput.Text;
            DisplayAlert("Input Received", $"You entered: {userInput}", "OK");
        }
    }

}
