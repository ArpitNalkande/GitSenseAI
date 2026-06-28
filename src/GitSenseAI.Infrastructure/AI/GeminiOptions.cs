using System;
using System.Collections.Generic;
using System.Text;

namespace GitSenseAI.Infrastructure.AI;

public class GeminiOptions
{
    public const string SectionName = "Gemini";

    public string ApiKey { get; set; }
        = string.Empty;

    public string Model { get; set; }
        = "gemini-2.5-flash";
}
