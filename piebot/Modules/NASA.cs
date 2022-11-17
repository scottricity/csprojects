using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections;

namespace NASA
{

    //Define a static structure, TODO: possible dynamic getter????
    public record class NASAOutput(
        string copyright,
        string date,
        string explanation,
        string hdurl,
        string media_type,
        string service_version,
        string title,
        string url
    );

    
    public class NASA_API
    {
        HttpClient _client = new();

        public async Task makeRequest(string url)
        {
            var requestBody = await _client.GetStringAsync(requestUri: url);
            if (requestBody != null)
            {
                var requestSerialized = JsonSerializer.Deserialize<JsonElement>(requestBody);
                Console.WriteLine(requestSerialized.GetProperty("hdurl"));
            }

        }
    }
}