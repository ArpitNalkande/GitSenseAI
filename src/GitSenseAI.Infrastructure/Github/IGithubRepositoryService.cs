using GitSenseAI.Infrastructure.Github;
using GitSenseAI.Infrastructure.Github.Models;

public interface IGithubRepositoryService
{
    Task<GithubRepositoryResponse?> GetRepositoryAsync(string owner, string repository);
    Task<List<GithubContributorResponse>> GetContributorsAsync(string owner, string repository);
    Task<List<GithubCommitResponse>> GetCommitsAsync(string owner, string repository);
    Task<List<GithubPullRequestResponse>> GetPullRequestsAsync(string owner, string repository);
    Task<List<GithubIssueResponse>> GetIssuesAsync(string owner, string repository);
}