using System.Text.Json.Serialization;

namespace GitSenseAI.Infrastructure.AI.Models;

public class GeminiResponse
{
    [JsonPropertyName("candidates")]
    public List<Candidate> Candidates { get; set; } = [];

    public class Candidate
    {
        [JsonPropertyName("content")]
        public Content Content { get; set; } = new();
    }

    public class Content
    {
        [JsonPropertyName("parts")]
        public List<Part> Parts { get; set; } = [];
    }

    public class Part
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
    }
}