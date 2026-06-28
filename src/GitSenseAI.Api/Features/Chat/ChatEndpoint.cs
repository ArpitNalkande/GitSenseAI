using GitSenseAI.Api.Features.Repository.RepositoryDetails;
using GitSenseAI.Api.Features.Chat.Services;
using GitSenseAI.Infrastructure.AI;
using GitSenseAI.Infrastructure.Github;

namespace GitSenseAI.Api.Features.Chat;

public static class ChatEndpoint
{
    public static void MapChatEndpoint(this WebApplication app)
    {
        app.MapPost(
            "/api/chat",
            async (
                ChatRequest request,
                IGithubRepositoryService githubService,
                IRetrievalService retrievalService,
                IAIService aiService) =>
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
                    return Results.NotFound();
                }

                var details = new RepositoryDetailsResponse
                {
                    Repository = new()
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

                var documents = SearchDocumentBuilder.Build(details);

                var relevantDocuments = retrievalService.Search(
                    request.Question,
                    documents);

                var prompt = ChatPromptBuilder.Build(
                    request.Question,
                    relevantDocuments);

                var answer = await aiService.GenerateAsync(prompt);

                var response = new ChatResponse
                {
                    Answer = answer,
                    Sources = relevantDocuments
                };

                return Results.Ok(response);
            });
    }
}