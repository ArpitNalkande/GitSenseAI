using System;
using System.Collections.Generic;
using System.Text;

namespace GitSenseAI.Infrastructure.Configurations
{
    public class GithubOptions
    {
        public const string SectionName = "Github";
        public string personalAccessToken { get; set; } = string.Empty;
    }
}
