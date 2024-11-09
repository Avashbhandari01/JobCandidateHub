using JobCandidateHub.Application.DTOs.Candidate;
using JobCandidateHub.Application.Interfaces.Repositories;
using JobCandidateHub.Application.Interfaces.Services;
using JobCandidateHub.Domain.Entities.Models;
using Microsoft.Extensions.Caching.Memory;

namespace JobCandidateHub.Infrastructure.Implementation.Services
{
    public class CandidateService(IGenericRepository genericRepository, IMemoryCache cache) : ICandidateService
    {
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(30);

        public async Task<bool> InsertOrUpdateCandidate(CandidateDto candidate)
        {
            try
            {
                var cacheKey = $"Candidate-{candidate.Email}";

                if (!cache.TryGetValue(cacheKey, out Candidate? candidateInfo))
                {
                    candidateInfo = await genericRepository.GetFirstOrDefaultAsync<Candidate>(x => x.Email == candidate.Email);
                    if (candidateInfo != null)
                    {
                        cache.Set(cacheKey, candidateInfo, _cacheExpiration);
                    }
                }

                if (candidateInfo == null)
                {
                    var newCandidate = new Candidate()
                    {
                        FirstName = candidate.FirstName,
                        LastName = candidate.LastName,
                        PhoneNumber = candidate.PhoneNumber ?? string.Empty,
                        Email = candidate.Email,
                        CallInterval = candidate.CallInterval ?? string.Empty,
                        LinkedInUrl = candidate.LinkedInUrl ?? string.Empty,
                        GitHubUrl = candidate.GitHubUrl ?? string.Empty,
                        Comment = candidate.Comment
                    };

                    await genericRepository.InsertAsync(newCandidate);
                }
                else
                {
                    candidateInfo.FirstName = candidate.FirstName;
                    candidateInfo.LastName = candidate.LastName;
                    candidateInfo.PhoneNumber = candidate.PhoneNumber ?? string.Empty;
                    candidateInfo.Email = candidate.Email;
                    candidateInfo.CallInterval = candidate.CallInterval ?? string.Empty;
                    candidateInfo.LinkedInUrl = candidate.LinkedInUrl ?? string.Empty;
                    candidateInfo.GitHubUrl = candidate.GitHubUrl ?? string.Empty;
                    candidateInfo.Comment = candidate.Comment;
                    candidateInfo.LastModifiedAt = DateTime.Now;

                    await genericRepository.UpdateAsync(candidateInfo);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
