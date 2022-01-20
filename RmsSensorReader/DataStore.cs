using System.Text.Json;


namespace RmsSensorReader;

public class DataStore
{
    static HttpClient client;
    string url = "https://backend.thinger.io/v3/users/gustavf/devices/berlinstation2/callback/data";

    public DataStore(string token)
    {
        client = new HttpClient();
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }

    public async Task StoreReading(double temp, double humidity, double cpuTemp)
    {
        temp = Math.Round(temp, 2);
        humidity = Math.Round(humidity, 2);
        cpuTemp = Math.Round(cpuTemp, 1);
        var data = new { timestamp = DateTime.UtcNow, temperature = temp, humidity = humidity, cpu_temp = cpuTemp };
        string json = JsonSerializer.Serialize(data);
        var result = await client.PostAsync(url, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (result.Content != null)
        {
            string s = await result.Content.ReadAsStringAsync();
        }
        Console.WriteLine($"Saving data outcome: {result.StatusCode}");
    }
}