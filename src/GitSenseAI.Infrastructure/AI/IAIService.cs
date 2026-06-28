using System;
using System.Collections.Generic;
using System.Text;

namespace GitSenseAI.Infrastructure.AI
{
    public interface IAIService
    {
        public Task<string> GenerateAsync(string prompt);
    }
}
