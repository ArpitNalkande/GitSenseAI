namespace GitSenseAI.Api.Features.AI.RepositorySummary;

public class RepositorySummaryRequest
{
    public string Owner { get; set; } = string.Empty;

    public string Repository { get; set; } = string.Empty;
}