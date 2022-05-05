using System.Text.Json;
using YRDownloaderTest;

//string url = "https://api.met.no/weatherapi/locationforecast/2.0/compact?lat=55.47028&lon=8.45187&altitude=1"; //esbjerg
string url = "https://api.met.no/weatherapi/locationforecast/2.0/compact?lat=56.62034529117882&lon=8.154917297252135&altitude=1"; //ved thyborøn

string? json = await GetWeatherJsonAsync(url);
var result = JsonSerializer.Deserialize<Rootobject>(json);

foreach (Timesery record in result.properties.timeseries)
{
    Console.WriteLine("Time:{0,8}  -  WindSpeed:{1,6}   WindDirectionD:{2,6}   AirTEmp:{3,6}   CloudCover:{4,6}", record.time, record.data.instant.details.wind_speed, record.data.instant.details.wind_from_direction, record.data.instant.details.air_temperature, record.data.instant.details.cloud_area_fraction);
}

static async Task<string> GetWeatherJsonAsync(string url)
{
    HttpClient client = new();
    client.DefaultRequestHeaders.UserAgent.TryParseAdd("kurgans test app");

    var response = await client.GetAsync(url);
    var result = await response.Content.ReadAsStringAsync();

    return result;
}