namespace InstantHire.Domain.DTOs;

public record FreelancerSummary(
    string Name,
    string Specialty,
    double AvgRating,
    int ReviewCount
);