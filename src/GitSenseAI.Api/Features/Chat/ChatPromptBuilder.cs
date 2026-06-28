namespace GitSenseAI.Api.Features.Chat;

public static class ChatPromptBuilder
{
    public static string Build(
        string question,
        List<SearchDocument> documents)
    {
        var context =
            string.Join(
                Environment.NewLine,
                documents.Select(
                    x => $"{x.Type}: {x.Content}"));

        return $$"""
You are an expert GitHub engineering assistant.

Answer ONLY using the context below.

Context:
{{context}}

Question:
{{question}}
""";
    }
}