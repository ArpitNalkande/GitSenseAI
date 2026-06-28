

namespace GitSenseAI.Api.Features.Chat.Services;

public interface IEmbeddingService
{
    float[] Generate(string text);
}