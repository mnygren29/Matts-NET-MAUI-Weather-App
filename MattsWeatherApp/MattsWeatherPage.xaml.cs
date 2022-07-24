using MattsWeatherApp.Services;

namespace MattsWeatherApp;

public partial class MattsWeatherPage : ContentPage
{
	public List<Models.List> WeatherList;
	public double latitude;
	public double longitude;
	public MattsWeatherPage()
	{
		InitializeComponent();
		WeatherList = new List<Models.List>();
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();
		await GetLocation();
		var result = await ApiServices.GetMattsWeather(latitude, longitude);
		foreach(var item in result.list)
		{
			WeatherList.Add(item);

		}
		CvWeather.ItemsSource = WeatherList;

		LblCity.Text = result.city.name;
		LblWeatherDescription.Text = result.list[0].weather[0].description;
		LblTemperature.Text=result.list[0].main.temperaturecelsius + " C";
        LblHumidy.Text=result.list[0].main.humidity + " %";
		LblWind.Text = result.list[0].wind.speed + "km/h";
		ImgWeatherIcon.Source=result.list[0].weather[0].fullIconUrl;
	}

	public async Task GetLocation()
	{
		var location = await Geolocation.GetLocationAsync();
		latitude = location.Latitude;
		longitude = location.Longitude;

	}

	private async void TapLocation_Tapped(object sender, EventArgs e)
	{
		await GetLocation();
		await GetWeatherDataByLocation(latitude, longitude);
	}

	public async Task GetWeatherDataByLocation(double latitude,double longitude)
	{
        var result = await ApiServices.GetMattsWeather(latitude, longitude);
		UpdateUI(result);


}

    public async Task GetWeatherDataByCity(string city)
    {
        var result = await ApiServices.GetMattsWeatherByCity(city);
        UpdateUI(result);

    }

	public void UpdateUI(dynamic result)
	{
        foreach (var item in result.list)
        {
            WeatherList.Add(item);

        }
        CvWeather.ItemsSource = WeatherList;

        LblCity.Text = result.city.name;
        LblWeatherDescription.Text = result.list[0].weather[0].description;
        LblTemperature.Text = result.list[0].main.temperaturecelsius + " C";
        LblHumidy.Text = result.list[0].main.humidity + " %";
        LblWind.Text = result.list[0].wind.speed + "km/h";
        ImgWeatherIcon.Source = result.list[0].weather[0].fullIconUrl;
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
	{
		var response=await DisplayPromptAsync(title: "", message: "", placeholder: "Search weather by city", accept: "Search", cancel: "Cancel");
		if(response != null)
		{
			await GetWeatherDataByCity(response);
		}
	}
}