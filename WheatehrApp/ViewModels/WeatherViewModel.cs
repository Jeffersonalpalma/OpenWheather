using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using WheatehrApp.Models;
using Microsoft.Maui.Controls;

namespace WheatehrApp.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private string _cityInput;
        public string CityInput
        {
            get => _cityInput;
            set
            {
                _cityInput = value;
                OnPropertyChanged();
            }
        }
          public class WeatherForecast
    {
        public string DateTime { get; set; }
        public double Temperature { get; set; }
        public string WeatherDescription { get; set; }
        public double WindSpeed { get; set; }
        public int Humidity { get; set; }
    }

        private string _cityName;
        public string CityName
        {
            get => _cityName;
            set
            {
                _cityName = value;
                OnPropertyChanged();
            }
        }

        private double _temperature;
        public double Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                OnPropertyChanged();
            }
        }

        private int _humidity;
        public int Humidity
        {
            get => _humidity;
            set
            {
                _humidity = value;
                OnPropertyChanged();
            }
        }

        private string _weatherDescription;
        public string WeatherDescription
        {
            get => _weatherDescription;
            set
            {
                _weatherDescription = value;
                OnPropertyChanged();
            }
        }

        private double _windSpeed;
        public double WindSpeed
        {
            get => _windSpeed;
            set
            {
                _windSpeed = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchWeatherCommand { get; }

        public WeatherViewModel()
        {
            SearchWeatherCommand = new Command(async () => await LoadWeatherAsync());
        }

        public async Task LoadWeatherAsync()
        {
            if (string.IsNullOrWhiteSpace(CityInput))
            {
                CityName = "Por favor, insira uma cidade válida.";
                return;
            }

            try
            {
                using var client = new HttpClient();
                string apiUrl = $"https://api.openweathermap.org/data/2.5/forecast?q={CityInput}&appid=69a0f1b450aa62338039032c7506d5c5&units=metric";
                var response = await client.GetStringAsync(apiUrl);
                var weatherData = JsonConvert.DeserializeObject<Root>(response);

                if (weatherData != null)
                {
                    CityName = weatherData.city.name;
                    Temperature = weatherData.list.FirstOrDefault()?.main.temp ?? 0;
                    Humidity = weatherData.list.FirstOrDefault()?.main.humidity ?? 0;
                    WeatherDescription = weatherData.list.FirstOrDefault()?.weather.FirstOrDefault()?.description ?? "N/A";
                    WindSpeed = weatherData.list.FirstOrDefault()?.wind.speed ?? 0;
                }
            }
            catch
            {
                CityName = "Erro ao buscar dados. Verifique a cidade.";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
