using InstantHire.Domain.Exceptions;

namespace InstantHire.Domain.Entities;

public class Review
{
    // =========================
    // Primary Key
    // =========================
    public int Id { get; set; }

    // =========================
    // Rating (1 to 5 ONLY)
    // =========================
    private int _rating;

    public int Rating
    {
        get => _rating;
        set
        {
            if (value < 1 || value > 5)
                throw new InvalidRatingException(value);

            _rating = value;
        }
    }

    // =========================
    // Comment from client
    // =========================
    public string Comment { get; set; } = string.Empty;

    // =========================
    // Date created
    // =========================
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // =========================
    // Relationship: Project (1-1)
    // =========================
    public int ProjectId { get; set; }
    public virtual Project Project { get; set; } = null!;

    // =========================
    // Relationship: Freelancer (1-Many Reviews)
    // =========================
    public int FreelancerId { get; set; }
    public virtual Freelancer Freelancer { get; set; } = null!;
}