namespace GitSenseAI.Api.Features.Chat.Services;

public static class SimilarityService
{
    public static double CosineSimilarity(float[] a, float[] b)
    {
        double dot = 0;
        double magnitudeA = 0;
        double magnitudeB = 0;

        for (var i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            magnitudeA += a[i] * a[i];
            magnitudeB += b[i] * b[i];
        }

        if (magnitudeA == 0 || magnitudeB == 0)
        {
            return 0;
        }

        return dot /
               (Math.Sqrt(magnitudeA)
                * Math.Sqrt(magnitudeB));
    }
}