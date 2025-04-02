﻿using System.Collections.Immutable;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.Maui.Graphics.Text;
using Color = Microsoft.Maui.Graphics.Color;
using Point = Microsoft.Maui.Graphics.Point;

namespace CryptoWalletChecker
{
    public partial class MainPage : ContentPage
    {
        IMethodsServices methodsServices;
        public MainPage(IMethodsServices methods)
        {
            InitializeComponent();
            using (StreamWriter streamWriter = new StreamWriter(@"..\..\wallets.txt", true))
            {
                Console.WriteLine("Wallets.txt has been created");
            }
            methodsServices = methods;
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
                    RegisterExistingWallet.IsVisible = true;
                    CheckExistingWallet.IsVisible = false;
                    WalletExistLabel.Text = $"Wallet '{textInput.Text}' was already registered, sum of transaction:";
                    WalletExistLabelSum.Text = $"{methodsServices.GetTransactionSum(textInput.Text)} coins";
                }
                else
                {
                    CheckExistingWallet.IsVisible = false;
                    RegisterNonexistentWallet.IsVisible = true;
                    EnterSumLabel.Text = $"Enter sum to register transaction for";
                    EnterSumLabel2.Text = $"'{textInput.Text}'";
                }
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
        private void OnRegister(object sender, EventArgs e)
        {
            methodsServices.WriteToFile(int.Parse(sumInput.Text), textInput.Text);
            CheckExistingWallet.IsVisible = true;
            RegisterNonexistentWallet.IsVisible = false;
            SuccessLabel.IsVisible = true;
            textInput.Text = string.Empty;
            sumInput.Text = string.Empty;
        }
    }
}
