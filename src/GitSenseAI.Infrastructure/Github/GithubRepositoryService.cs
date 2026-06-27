using System.Net.Http.Headers;
using System.Text.Json;
using GitSenseAI.Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace GitSenseAI.Infrastructure.Github;

public class GithubRepositoryService : IGithubRepositoryService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly GithubOptions _githubOptions;

    public GithubRepositoryService(
        IHttpClientFactory httpClientFactory,
        IOptions<GithubOptions> githubOptions)
    {
        _httpClientFactory = httpClientFactory;
        _githubOptions = githubOptions.Value;
    }

    public async Task<GithubRepositoryResponse?> GetRepositoryAsync(
        string owner,
        string repository)
    {
        var client = _httpClientFactory.CreateClient();

        client.DefaultRequestHeaders.UserAgent.Add(
            new ProductInfoHeaderValue("GitSenseAI", "1.0"));

        if (!string.IsNullOrWhiteSpace(_githubOptions.personalAccessToken))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    _githubOptions.personalAccessToken);
        }

        var url =
            $"https://api.github.com/repos/{owner}/{repository}";

        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var json =
            await response.Content.ReadAsStringAsync();

        var repositoryResponse =
            JsonSerializer.Deserialize<GithubRepositoryResponse>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        return repositoryResponse;
    }
}