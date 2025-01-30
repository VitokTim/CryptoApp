using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CryptoApp.Models;

namespace CryptoApp.ViewModels
{
    public class CryptoViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Crypto> _cryptos;
        public ObservableCollection<Crypto> Cryptos
        {
            get => _cryptos;
            set
            {
                _cryptos = value;
                OnPropertyChanged(nameof(Cryptos));
            }
        }

        public CryptoViewModel()
        {
            Cryptos = new ObservableCollection<Crypto>();
            LoadCryptoData();
        }

        private async void LoadCryptoData()
        {
            string apiUrl =
            "https://api.coincap.io/v2/assets?ids=bitcoin,ethereum,binance-coin,tether,solana,cardano,xrp,polkadot,dogecoin,shiba-inu";

            try
            {
                using HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(apiUrl);
                Debug.WriteLine($"RESPONSE: {response}");

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
                    Debug.WriteLine("JSON десериализован, но данных нет.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
