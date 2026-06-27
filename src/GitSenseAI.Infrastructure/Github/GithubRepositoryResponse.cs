using System.Text.Json.Serialization;

namespace GitSenseAI.Infrastructure.Github;

public class GithubRepositoryResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("stargazers_count")]
    public int Stars { get; set; }

    [JsonPropertyName("forks_count")]
    public int Forks { get; set; }

    [JsonPropertyName("open_issues_count")]
    public int OpenIssues { get; set; }
}