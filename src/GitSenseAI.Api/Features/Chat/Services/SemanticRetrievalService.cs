namespace GitSenseAI.Api.Features.Chat.Services;

public class SemanticRetrievalService : IRetrievalService
{
    private readonly IEmbeddingService _embeddingService;

    public SemanticRetrievalService(IEmbeddingService embeddingService)
    {
        _embeddingService = embeddingService;
    }

    public List<SearchDocument> Search(string question, List<SearchDocument> documents)
    {
        var questionVector = _embeddingService.Generate(question);

        return documents
            .Select(document =>
            {
                var documentVector = _embeddingService.Generate(document.Content);

                return new
                {
                    Document = document,
                    Score = SimilarityService.CosineSimilarity(
                        questionVector,
                        documentVector)
                };
            })
            .OrderByDescending(x => x.Score)
            .Take(5)
            .Select(x => x.Document)
            .ToList();
    }
}