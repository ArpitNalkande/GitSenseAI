using GitSenseAI.Api.Features.Repository.AnalyzeRepository;
using GitSenseAI.Infrastructure.Github;

namespace GitSenseAI.Api.Features.Repository.RepositoryDetails;

public static class RepositoryDetailsEndpoint
{
    public static void MapRepositoryDetailsEndpoint(this WebApplication app)
    {
        app.MapPost(
            "/api/repositories/details",
            async (RepositoryDetailsRequest request, IGithubRepositoryService githubService) =>
            {
                var repositoryTask = githubService.GetRepositoryAsync(request.Owner, request.Repository);
                var contributorsTask = githubService.GetContributorsAsync(request.Owner, request.Repository);
                var commitsTask = githubService.GetCommitsAsync(request.Owner, request.Repository);
                var pullRequestsTask = githubService.GetPullRequestsAsync(request.Owner, request.Repository);
                var issuesTask = githubService.GetIssuesAsync(request.Owner, request.Repository);

                await Task.WhenAll(
                    repositoryTask,
                    contributorsTask,
                    commitsTask,
                    pullRequestsTask,
                    issuesTask);

                var repository = await repositoryTask;

                if (repository is null)
                {
                    return Results.NotFound("Repository not found.");
                }

                var response = new RepositoryDetailsResponse
                {
                    Repository = new AnalyzeRepositoryResponse
                    {
                        Name = repository.Name,
                        Description = repository.Description,
                        Language = repository.Language,
                        Stars = repository.Stars,
                        Forks = repository.Forks,
                        OpenIssues = repository.OpenIssues
                    },
                    Contributors = await contributorsTask,
                    Commits = await commitsTask,
                    PullRequests = await pullRequestsTask,
                    Issues = await issuesTask
                };

                return Results.Ok(response);
            });
    }
}