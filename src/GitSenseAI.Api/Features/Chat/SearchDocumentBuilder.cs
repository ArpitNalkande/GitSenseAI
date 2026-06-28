using GitSenseAI.Api.Features.Repository.RepositoryDetails;

namespace GitSenseAI.Api.Features.Chat;

public static class SearchDocumentBuilder
{
    public static List<SearchDocument> Build(RepositoryDetailsResponse details)
    {
        var documents = new List<SearchDocument>();

        documents.AddRange(
            details.Commits.Select(x => new SearchDocument
            {
                Type = "Commit",
                Content = x.Commit.Message
            }));

        documents.AddRange(
            details.Issues.Select(x => new SearchDocument
            {
                Type = "Issue",
                Content = x.Title
            }));

        documents.AddRange(
            details.PullRequests.Select(x => new SearchDocument
            {
                Type = "PullRequest",
                Content = x.Title
            }));

        return documents;
    }
}