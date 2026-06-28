namespace GitSenseAI.Api.Features.Chat
{
    public class ChatResponse
    {
        public string Answer { get; set; } = string.Empty;
        public List<SearchDocument> Sources { get; set; } = [];
    }
}
