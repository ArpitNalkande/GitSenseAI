using GitSenseAI.Infrastructure.Configurations;
using GitSenseAI.Infrastructure.Github;
using GitSenseAI.Infrastructure.Github.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

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

    private async Task<T?> GetFromGithubAsync<T>(
    string url)
    {
        var client = _httpClientFactory.CreateClient();

        client.DefaultRequestHeaders.UserAgent.Add(
            new ProductInfoHeaderValue(
                "GitSenseAI",
                "1.0"));

        if (!string.IsNullOrWhiteSpace(
                _githubOptions.personalAccessToken))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    _githubOptions.personalAccessToken);
        }

        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        var json =
            await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
    }

    public async Task<GithubRepositoryResponse?>
    GetRepositoryAsync(string owner,string repository)
    {
        var url = $"https://api.github.com/repos/{owner}/{repository}";

        return await GetFromGithubAsync
            <GithubRepositoryResponse>(url);
    }


    public async Task<List<GithubContributorResponse>>
    GetContributorsAsync(
        string owner,
        string repository)
    {
        var url =
            $"https://api.github.com/repos/{owner}/{repository}/contributors?per_page=5";

        return await GetFromGithubAsync
            <List<GithubContributorResponse>>(url)
            ?? [];
    }

    public async Task<List<GithubCommitResponse>>
    GetCommitsAsync(
        string owner,
        string repository)
    {
        var url =
            $"https://api.github.com/repos/{owner}/{repository}/commits?per_page=5";

        return await GetFromGithubAsync<
            List<GithubCommitResponse>>(url)
            ?? [];
    }

    public async Task<List<GithubPullRequestResponse>>
    GetPullRequestsAsync(
        string owner,
        string repository)
    {
        var url =
            $"https://api.github.com/repos/{owner}/{repository}/pulls?per_page=5";

        return await GetFromGithubAsync<
            List<GithubPullRequestResponse>>(url)
            ?? [];
    }

    public async Task<List<GithubIssueResponse>>
    GetIssuesAsync(
        string owner,
        string repository)
    {
        var url =
            $"https://api.github.com/repos/{owner}/{repository}/issues?per_page=5";

        var issues =
            await GetFromGithubAsync<
                List<GithubIssueResponse>>(url)
            ?? [];

        return issues
            .Where(x => x.PullRequest is null)
            .ToList();
    }
}