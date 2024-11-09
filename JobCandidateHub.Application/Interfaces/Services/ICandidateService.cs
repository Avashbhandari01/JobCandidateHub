using JobCandidateHub.Application.DTOs.Candidate;

namespace JobCandidateHub.Application.Interfaces.Services
{
    public interface ICandidateService
    {
        Task<bool> InsertOrUpdateCandidate(CandidateDto candidate);
    }
}
