using InstantHire.Domain.Entities;
using InstantHire.Domain.Enums;
using InstantHire.Domain.Exceptions;

namespace InstantHire.Services;

public class BidService
{
    // =====================================================
    // EVENT: Triggered when a bid is successfully accepted
    // =====================================================
    public static event Action<string, decimal, decimal>? OnLowBudgetWarning;
    public static event Action<string, string, decimal>? OnBidAccepted;

    // =====================================================
    // Submit a new bid for a project
    // =====================================================
    public void SubmitBid(Project project, Freelancer freelancer, decimal amount, string message)
    {
        // Validate bid amount
        if (amount <= 0)
            throw new InvalidBidException(amount);

        // Create new bid object
        var bid = new Bid
        {
            Amount = amount,
            Message = message,
            SubmittedDate = DateTime.Now,
            Status = BidStatus.Pending,

            Freelancer = freelancer,
            FreelancerId = freelancer.Id,

            Project = project,
            ProjectId = project.Id
        };
        // Low budget warning (90% rule)
        if (project.Budget > 0 && amount >= project.Budget * 0.9m)
        {
            OnLowBudgetWarning?.Invoke(project.Title, amount, project.Budget);
        }

        // Add bid to both sides of the relationship
        project.Bids.Add(bid);
        freelancer.Bids.Add(bid);
    }

    // =====================================================
    // Accept a bid (Business logic core)
    // =====================================================
    public void AcceptBid(Project project, Bid bid)
    {
        // Check if project already has an accepted bid
        var alreadyAccepted = project.Bids.Any(b => b.Status == BidStatus.Accepted);

        if (alreadyAccepted)
            throw new DuplicateAcceptanceException(project.Title);

        // Mark selected bid as accepted
        bid.Status = BidStatus.Accepted;

        // Reject all other bids in the same project
        foreach (var otherBid in project.Bids.Where(b => b.Id != bid.Id))
        {
            otherBid.Status = BidStatus.Rejected;
        }

        // Update project status
        project.StartProgress();

        // Fire event notification
        OnBidAccepted?.Invoke(
            project.Title,
            bid.Freelancer.FullName,
            bid.Amount
        );
    }
}