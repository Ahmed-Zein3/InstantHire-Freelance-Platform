using InstantHire.Domain.Enums;
using InstantHire.Domain.Exceptions;

namespace InstantHire.Domain.Entities;

public class Project
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public decimal Budget { get; set; }
    public DateTime Deadline { get; set; }

    public ProjectStatus Status { get; private set; } = ProjectStatus.Open;

    public List<Bid> Bids { get; set; } = new();

    public Client Client { get; set; }

    public int ClientId { get; set; }

    public Review? Review { get; set; }

    // =========================
    // ONLY ONE ACCEPTED BID RULE
    // =========================
    public void AcceptBid(Bid bid)
    {
        if (Bids.Any(b => b.Status == BidStatus.Accepted))
            throw new DuplicateAcceptanceException(Title);

        bid.Status = BidStatus.Accepted;

        foreach (var b in Bids.Where(x => x.Id != bid.Id))
        {
            b.Status = BidStatus.Rejected;
        }

        Status = ProjectStatus.InProgress;

        OnBidAccepted?.Invoke(Title, bid.Freelancer.FullName, bid.Amount);
    }

    // =========================
    // COMPLETE PROJECT
    // =========================
    public void MarkCompleted()
    {
        Status = ProjectStatus.Completed;
    }
    public void StartProgress()
    {
        if (Status != ProjectStatus.Open)
            throw new Exception("Project cannot start progress unless it is Open");

        Status = ProjectStatus.InProgress;
    }

    // =========================
    // EVENT
    // =========================
    public static event Action<string, string, decimal>? OnBidAccepted;
}