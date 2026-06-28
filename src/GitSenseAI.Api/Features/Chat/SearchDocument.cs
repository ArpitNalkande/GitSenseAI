namespace GitSenseAI.Api.Features.Chat;

public class SearchDocument
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Type { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
}