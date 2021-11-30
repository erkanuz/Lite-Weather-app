using System;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LiteWeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Daily();
            Weekly();
        }
        //We set some location on default location
        private string Location = "Boston";

        private async void Daily()
        {
            //we enter api code
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={Location}&appid={key}&units=metric";
            var result = await Helper.Get(url);

            //check if result Sucessful
            if (result.Succesful)
            {
                //Deserialize the JSON and we take our values and replace them with real values from api 
                var weatherinfo = JsonConvert.DeserializeObject<Root>(result.Response);
                description.Text = weatherinfo.weather[0].description.ToUpper();
                Wicon.Source = $"w{weatherinfo.weather[0].icon}";
                CityName.Text = weatherinfo.name.ToUpper();//we write the cityName in capital letters
                Temperature.Text = weatherinfo.main.temp.ToString("0"); //convert a T-number to a string format
                dropT.Text = $"{weatherinfo.main.humidity}%";
                windT.Text = $"{weatherinfo.wind.speed} m/s";
                umbrellaT.Text = $"{weatherinfo.main.pressure} mm";

                //create DateTime
                var dt = new DateTime().ToUniversalTime().AddSeconds(weatherinfo.dt);
                dateH.Text = dt.ToString("MMM dd").ToUpper();
            }
        }

        private async void Weekly()
        {
            //we enter api code
            var url = $"http://api.openweathermap.org/data/2.5/forecast?q={Location}&appid={key}&units=metric";
            var result = await Helper.Get(url);

            if (result.Succesful)
            {
                //Deserialize the JSON and we take our values and replace them with real values from api
                //We create an "elements" who will include on self all items of the class List
                var forecast = JsonConvert.DeserializeObject<Root>(result.Response);
                List<List> elements = new List<List>();

                foreach (List list in forecast.list)
                {
                    var date = DateTime.Parse(list.dt_txt);

                    //check for new day
                    if (date > DateTime.Now && date.Hour == 0 && date.Minute == 0 && date.Second == 0)
                        elements.Add(list);
                }

                firstday.Text = DateTime.Parse(elements[0].dt_txt).ToString("dddd");
                firstwicon.Source = $"w{elements[0].weather[0].icon}";
                firstTemp.Text = elements[0].main.temp.ToString("0");

                secounday.Text = DateTime.Parse(elements[1].dt_txt).ToString("dddd");
                secoundwicon.Source = $"w{elements[1].weather[0].icon}";
                secoundTemp.Text = elements[1].main.temp.ToString("0");

                thirdday.Text = DateTime.Parse(elements[2].dt_txt).ToString("dddd");
                thirdwicon.Source = $"w{elements[2].weather[0].icon}";
                thirdTemp.Text = elements[2].main.temp.ToString("0");

                fourthday.Text = DateTime.Parse(elements[3].dt_txt).ToString("dddd");
                fourthwicon.Source = $"w{elements[3].weather[0].icon}";
                fourthTemp.Text = elements[3].main.temp.ToString("0");

                fivethday.Text = DateTime.Parse(elements[4].dt_txt).ToString("dddd");
                fivethwicon.Source = $"w{elements[4].weather[0].icon}";
                fivethTemp.Text = elements[4].main.temp.ToString("0");
            }
        }
    }
}
