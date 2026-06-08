namespace InstantHire.Domain.DTOs;

public record ClientSpendReport(
    string CompanyName,
    int ProjectsCompleted,
    decimal TotalSpent
);