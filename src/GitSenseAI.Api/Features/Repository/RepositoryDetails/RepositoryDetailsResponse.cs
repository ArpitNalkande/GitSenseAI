using GitSenseAI.Api.Features.Repository.AnalyzeRepository;
using GitSenseAI.Infrastructure.Github.Models;

namespace GitSenseAI.Api.Features.Repository.RepositoryDetails;

public class RepositoryDetailsResponse
{
    public AnalyzeRepositoryResponse Repository { get; set; } = new();

    public List<GithubContributorResponse> Contributors { get; set; } = [];

    public List<GithubCommitResponse> Commits { get; set; } = [];

    public List<GithubPullRequestResponse> PullRequests { get; set; } = [];

    public List<GithubIssueResponse> Issues { get; set; } = [];
}