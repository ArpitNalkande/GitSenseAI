namespace GitSenseAI.Api.Features.Chat.Services
{
    public interface IRetrievalService
    {
        List<SearchDocument> Search(string question,List<SearchDocument> documents);
    }
}