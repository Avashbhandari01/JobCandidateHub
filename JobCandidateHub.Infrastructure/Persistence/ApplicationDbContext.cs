using JobCandidateHub.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace JobCandidateHub.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Candidate> Candidates { get; set; }
    }
}
