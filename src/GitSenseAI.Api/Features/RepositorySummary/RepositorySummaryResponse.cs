namespace GitSenseAI.Api.Features.AI.RepositorySummary;

public class RepositorySummaryResponse
{
    public string Summary { get; set; } = string.Empty;

    public int HealthScore { get; set; }

    public List<string> Recommendations { get; set; } = [];
}