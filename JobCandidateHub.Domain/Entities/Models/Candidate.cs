using JobCandidateHub.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace JobCandidateHub.Domain.Entities.Models
{
    public class Candidate : BaseEntity<int>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public string? CallInterval { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? GitHubUrl { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
