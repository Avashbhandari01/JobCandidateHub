using System.ComponentModel.DataAnnotations;

namespace JobCandidateHub.Domain.Entities.Base
{
    public class BaseEntity<TPrimaryKey>
    {
        [Key]
        public TPrimaryKey Id { get; set; } = default!;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? LastModifiedAt { get; set; }
    }
}
