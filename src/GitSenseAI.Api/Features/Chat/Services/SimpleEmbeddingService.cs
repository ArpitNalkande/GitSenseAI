// This file defines the IEmbeddingService interface,
// which provides a method for generating embeddings from text.

// EmbeddingService are the array of floats that
// represent the semantic meaning of a piece of text.

// embedding algorithm 


namespace GitSenseAI.Api.Features.Chat.Services;

public class SimpleEmbeddingService : IEmbeddingService
{
    private const int VectorSize = 128;

    public float[] Generate(string text)
    {
        var vector = new float[VectorSize];

        var words = text
            .ToLower()
            .Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries);

        foreach (var word in words)
        {
            var index = Math.Abs(word.GetHashCode()) % VectorSize;

            vector[index] += 1;
        }

        return vector;
    }
}