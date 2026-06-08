
namespace InstantHire.Domain.Entities;

public class Skill
{
    // Primary Key
    public int Id { get; set; }

    // Skill name 
    public string Name { get; set; } = string.Empty;

    // Foreign Key -> Freelancer who owns this skill
    public int FreelancerId { get; set; }

    // Navigation Property
    public Freelancer Freelancer { get; set; } = null!;
}