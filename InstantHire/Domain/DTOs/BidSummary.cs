namespace InstantHire.Domain.DTOs;

public record BidSummary(
    string ProjectTitle,
    string FreelancerName,
    decimal BidAmount,
    decimal BudgetPercent
);