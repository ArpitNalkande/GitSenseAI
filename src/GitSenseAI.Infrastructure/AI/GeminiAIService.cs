using System.Text;
using System.Text.Json;
using GitSenseAI.Infrastructure.AI.Models;
using Microsoft.Extensions.Options;

namespace GitSenseAI.Infrastructure.AI;

public class GeminiAIService : IAIService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly GeminiOptions _options;

    public GeminiAIService(IHttpClientFactory httpClientFactory, IOptions<GeminiOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    public async Task<string> GenerateAsync(string prompt)
    {
        var client = _httpClientFactory.CreateClient();

        var url = $"https://generativelanguage.googleapis.com/v1beta/models/{_options.Model}:generateContent?key={_options.ApiKey}";

        var request = new GeminiRequest
        {
            Contents =
            [
                new GeminiRequest.Content
                {
                    Parts =
                    [
                        new GeminiRequest.Part
                        {
                            Text = prompt
                        }
                    ]
                }
            ]
        };

        var json = JsonSerializer.Serialize(request);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await client.PostAsync(url, content);

        //response.EnsureSuccessStatusCode();

        if (!response.IsSuccessStatusCode)
        {
            var error =
                await response.Content.ReadAsStringAsync();

            throw new Exception(error);
        }

        var responseJson = await response.Content.ReadAsStringAsync();

        var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(
            responseJson,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return geminiResponse?
                   .Candidates
                   .FirstOrDefault()?
                   .Content
                   .Parts
                   .FirstOrDefault()?
                   .Text
               ?? "No response generated.";
    }
}