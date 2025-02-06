using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using CryptoApp.Helpers;
using CryptoApp.Models;
using CryptoApp.Views;

namespace CryptoApp.ViewModels
{
    public class CryptoViewModel : INotifyPropertyChanged
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private ObservableCollection<Crypto> _cryptos;
        private string apiUrlTopPopular = "https://api.coincap.io/v2/assets?ids=bitcoin,ethereum,binance-coin,tether,solana,cardano,xrp,polkadot,dogecoin,shiba-inu";
        private string apiSearch = "https://api.coincap.io/v2/assets?ids=";
        private string _searchTextCrypto;
        public string SearchTextCrypto
        {
            get => _searchTextCrypto;
            set
            {
                _searchTextCrypto = value;
                OnPropertyChanged(nameof(SearchTextCrypto));
            }
        }
        public ObservableCollection<Crypto> Cryptos
        {
            get => _cryptos;
            set
            {
                _cryptos = value;
                OnPropertyChanged(nameof(Cryptos));
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand ShowDetailsCommand { get; }
        public ICommand OpenHomePageCommand { get; }

        public CryptoViewModel()
        {
            Cryptos = new ObservableCollection<Crypto>();
            LoadCryptoData(apiUrlTopPopular);
            SearchCommand = new RelayCommand<string>(ExecuteSearch, CanExecuteSearch);
            ShowDetailsCommand = new RelayCommand<Crypto>(ShowDetails);
            OpenHomePageCommand = new RelayCommand(OpenHomePage);
        }

        private void ExecuteSearch(string searchText)
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
               LoadCryptoDataSearch(searchText);
            }
        }

        private bool CanExecuteSearch(string searchText)
        {
            return !string.IsNullOrWhiteSpace(searchText);
        }

        private async void LoadCryptoData(string apiUrl)
        {
            try
            {
                var response = await _httpClient.GetStringAsync(apiUrl);

                var cryptoResponse = JsonSerializer.Deserialize<CryptoListResponse>(response);

                if (cryptoResponse?.Data != null)
                {
                    Cryptos.Clear();
                    foreach (var crypto in cryptoResponse.Data)
                    {
                        Cryptos.Add(crypto);
                    }
                }
                else
                {
                    Debug.WriteLine("JSON deserialized, but no data.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        private void LoadCryptoDataSearch(string textCryptoSearch)
        {
            string text = textCryptoSearch.ToLower();
            LoadCryptoData(apiSearch + text);
        }

        private void ShowDetails(Crypto crypto)
        {
            if (crypto != null)
            {
                var frame = Application.Current.MainWindow.FindName("MyFrame") as Frame;
                if (frame == null)
                {
                    Debug.WriteLine("Error: Frame not found!");
                    return;
                }
                Debug.WriteLine($"Page navigation: {crypto.Name}");
                frame.Navigate(new CryptoDetailsPage(crypto));
            }
        }

        private void OpenHomePage()
        {
            var frame = Application.Current.MainWindow.FindName("MyFrame") as Frame;
            if (frame == null)
            {
                Debug.WriteLine("Error: Frame not found!");
                return;
            }
            frame.Navigate(new HomePage());
            LoadCryptoData(apiUrlTopPopular);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

