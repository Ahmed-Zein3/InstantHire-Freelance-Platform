using InstantHire.Abstractions;
using InstantHire.Domain.Entities;

namespace InstantHire.Domain.Entities;

public class Freelancer : FreelancerBase, IReviewable
{
    // Primary Key
    public int Id { get; set; }

    // One Freelancer can have many Skills
    public List<Skill> Skills { get; set; } = new();

    // One Freelancer can submit many Bids
    public List<Bid> Bids { get; set; } = new();

    public List<Review> Reviews { get; set; } = new();
    public bool CanReceiveReview()
    {
        throw new NotImplementedException();
    }

    public string GetReviewSummary()
    {
        throw new NotImplementedException();
    }

    public override string GetProfileSummary()
    {
        throw new NotImplementedException();
    }

  

    public double GetReputationScore()
    {
        if (Reviews == null || !Reviews.Any())
            return 0;

        return Reviews.Average(r => r.Rating);
    }
}