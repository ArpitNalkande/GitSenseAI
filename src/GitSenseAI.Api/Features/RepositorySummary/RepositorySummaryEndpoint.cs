using System.Text.Json;
using GitSenseAI.Api.Features.Repository.AnalyzeRepository;
using GitSenseAI.Api.Features.Repository.RepositoryDetails;
using GitSenseAI.Infrastructure.AI;
using GitSenseAI.Infrastructure.Github;

namespace GitSenseAI.Api.Features.AI.RepositorySummary;

public static class RepositorySummaryEndpoint
{
    public static void MapRepositorySummaryEndpoint(this WebApplication app)
    {
        app.MapPost(
            "/api/ai/repository-summary",
            async (
                RepositorySummaryRequest request,
                IGithubRepositoryService githubService,
                IAIService aiService) =>
            {
                var repositoryTask = githubService.GetRepositoryAsync(request.Owner, request.Repository);
                var contributorsTask = githubService.GetContributorsAsync(request.Owner, request.Repository);
                var commitsTask = githubService.GetCommitsAsync(request.Owner, request.Repository);
                var issuesTask = githubService.GetIssuesAsync(request.Owner, request.Repository);

                await Task.WhenAll(
                    repositoryTask,
                    contributorsTask,
                    commitsTask,
                    issuesTask);

                var repository = await repositoryTask;

                if (repository is null)
                {
                    return Results.NotFound("Repository not found.");
                }

                var details = new RepositoryDetailsResponse
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
                    Issues = await issuesTask
                };

                var prompt = BuildPrompt(details);

                var aiResponse = await aiService.GenerateAsync(prompt);

                aiResponse = CleanJsonResponse(aiResponse);

                var result = JsonSerializer.Deserialize<RepositoryAnalysisResult>(
                    aiResponse,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return Results.Ok(
                    result ??
                    new RepositoryAnalysisResult());
            });
    }

    private static string BuildPrompt(RepositoryDetailsResponse details)
    {
        var commits = string.Join(
            Environment.NewLine,
            details.Commits.Select(x => $"- {x.Commit.Message}"));

        var issues = string.Join(
            Environment.NewLine,
            details.Issues.Select(x => $"- {x.Title}"));

        var contributors = string.Join(
            Environment.NewLine,
            details.Contributors.Select(x => $"- {x.Login} ({x.Contributions})"));

        return $$"""
You are a senior engineering manager.

Analyze this GitHub repository.

Repository Name:
{{details.Repository.Name}}

Description:
{{details.Repository.Description}}

Language:
{{details.Repository.Language}}

Stars:
{{details.Repository.Stars}}

Open Issues:
{{details.Repository.OpenIssues}}

Top Contributors:
{{contributors}}

Recent Commits:
{{commits}}

Recent Issues:
{{issues}}

Return ONLY valid JSON.

Schema:

{
  "summary": "string",
  "healthScore": 0,
  "recommendations": [
    "string",
    "string",
    "string"
  ]
}
""";
    }

    private static string CleanJsonResponse(string response)
    {
        response = response.Replace(
            "```json",
            string.Empty);

        response = response.Replace(
            "```",
            string.Empty);

        return response.Trim();
    }
}

