using GitsenseAI.Api.Features.Repository.AnalyzeRepository;
using GitSenseAI.Infrastructure.Github;

namespace GitSenseAI.Api.Features.Repository.AnalyzeRepository;

public static class AnalyzeRepositoryEndpoint
{
    public static void MapAnalyzeRepositoryEndpoint(
        this WebApplication app)
    {
        app.MapPost(
            "/api/repositories/analyze",
            async (AnalyzeRepositoryRequest request,IGithubRepositoryService githubService) =>
            {
                var repository =
                    await githubService.GetRepositoryAsync(
                        request.Owner,
                        request.Repository);

                if (repository is null)
                {
                    return Results.NotFound(
                        "Repository not found.");
                }

                var response =
                    new AnalyzeRepositoryResponse
                    {
                        Name = repository.Name,
                        Description = repository.Description,
                        Language = repository.Language,
                        Stars = repository.Stars,
                        Forks = repository.Forks,
                        OpenIssues = repository.OpenIssues
                    };

                return Results.Ok(response);
            });
    }
}