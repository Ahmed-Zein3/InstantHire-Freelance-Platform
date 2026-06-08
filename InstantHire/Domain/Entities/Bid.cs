using System;
using System.Collections.Generic;
using System.Text;

using InstantHire.Domain.Enums;

namespace InstantHire.Domain.Entities;

public class Bid
{
    // Primary Key
    public int Id { get; set; }

    // Amount offered by freelancer
    public decimal Amount { get; set; }

    // Message sent with the bid
    public string Message { get; set; } = string.Empty;

    // Date submitted
    public DateTime SubmittedDate { get; set; }

    // Bid Status
    public BidStatus Status { get; set; } = BidStatus.Pending;

    // =========================
    // Freelancer Relationship
    // =========================

    public int FreelancerId { get; set; }

    public Freelancer Freelancer { get; set; } = null!;

    // =========================
    // Project Relationship
    // =========================

    public int ProjectId { get; set; }

    public Project Project { get; set; } = null!;
}
