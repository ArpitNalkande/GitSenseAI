namespace GitsenseAI.Api.Features.Repository.AnalyzeRepository;

public class AnalyzeRepositoryRequest
{
    public string Owner { get; set; } = string.Empty;
    public string Repository { get; set; } = string.Empty;
}