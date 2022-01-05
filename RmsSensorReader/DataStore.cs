using System.Text.Json;


namespace RmsSensorReader;

public class DataStore
{
    static HttpClient client;
    public DataStore(string token)
    {
        client = new HttpClient();
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }

    public async Task StoreReading(double temp, double humidity)
    {
        var data = new { timestamp = DateTime.UtcNow, temperature = temp, humidity = humidity };
        string url = "https://backend.thinger.io/v3/users/gustavf/devices/berlinstation2/callback/data";
        string json = JsonSerializer.Serialize(data);
        var result = await client.PostAsync(url, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (result.Content != null)
        {
            string s = await result.Content.ReadAsStringAsync();
        }
        Console.WriteLine($"Saving data outcome: {result.StatusCode}");
    }
}
