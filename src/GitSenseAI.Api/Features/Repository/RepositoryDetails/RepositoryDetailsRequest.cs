namespace GitSenseAI.Api.Features.Repository.RepositoryDetails;

public class RepositoryDetailsRequest
{
    public string Owner { get; set; } = string.Empty;

    public string Repository { get; set; } = string.Empty;
}