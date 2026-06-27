    namespace GitSenseAI.Api.Features.Repository.AnalyzeRepository
    {
    public class AnalyzeRepositoryResponse
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Language { get; set; } = string.Empty;

        public int Stars { get; set; }

        public int Forks { get; set; }

        public int OpenIssues { get; set; }
    }
}
