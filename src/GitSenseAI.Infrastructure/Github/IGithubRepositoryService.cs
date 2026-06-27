using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GitSenseAI.Infrastructure.Github
{
    public interface IGithubRepositoryService
    {
        Task<GithubRepositoryResponse?> GetRepositoryAsync(string owner, string repo);

         
    }
}
