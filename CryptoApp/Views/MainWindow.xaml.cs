using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using CryptoApp.Helpers;
using CryptoApp.ViewModels;
using CryptoApp.Views;

namespace CryptoApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Application.Current.Resources["CryptoViewModel"];
            MyFrame.Navigate(new HomePage());
        }
    }
}
