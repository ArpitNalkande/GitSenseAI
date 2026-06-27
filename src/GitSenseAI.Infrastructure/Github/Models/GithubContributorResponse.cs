using System.Text.Json.Serialization;

namespace GitSenseAI.Infrastructure.Github.Models;

public class GithubContributorResponse
{
    public string Login { get; set; } = string.Empty;

    public int Contributions { get; set; }

    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; set; } = string.Empty;
}