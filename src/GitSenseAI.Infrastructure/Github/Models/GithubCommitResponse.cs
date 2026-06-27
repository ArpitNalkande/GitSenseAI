using System.Text.Json.Serialization;

namespace GitSenseAI.Infrastructure.Github.Models;

public class GithubCommitResponse
{
    public string Sha { get; set; } = string.Empty;

    public CommitInfo Commit { get; set; } = new();

    public class CommitInfo
    {
        public string Message { get; set; } = string.Empty;

        public CommitAuthor Author { get; set; } = new();
    }

    public class CommitAuthor
    {
        public string Name { get; set; } = string.Empty;

        public DateTime Date { get; set; }
    }
}