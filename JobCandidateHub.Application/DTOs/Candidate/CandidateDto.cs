using System.ComponentModel.DataAnnotations;

namespace JobCandidateHub.Application.DTOs.Candidate
{
    public class CandidateDto
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        public required string Email { get; set; }

        public string? CallInterval { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? GitHubUrl { get; set; }

        [Required]
        public required string Comment { get; set; }
    }
}
