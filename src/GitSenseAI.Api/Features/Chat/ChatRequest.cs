namespace GitSenseAI.Api.Features.Chat
{
    public class ChatRequest
    {
        public string Owner { get; set; } = string.Empty;
        public string Repository { get; set; } = string.Empty;
        public string Question { get; set; } = string.Empty;
    }
}
