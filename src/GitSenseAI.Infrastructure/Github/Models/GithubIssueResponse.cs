using System.Text.Json.Serialization;

public class GithubIssueResponse
{
    public int Number { get; set; }

    public string Title { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    [JsonPropertyName("pull_request")]
    public object? PullRequest { get; set; }
}