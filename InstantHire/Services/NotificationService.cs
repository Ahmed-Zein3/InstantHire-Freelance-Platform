using InstantHire.Services;

namespace InstantHire.Services;

public static class NotificationService
{
    public static void Initialize()
    {
        BidService.OnBidAccepted += HandleBidAccepted;
        BidService.OnLowBudgetWarning += HandleLowBudgetWarning;
    }

    private static void HandleBidAccepted(string projectTitle, string freelancerName, decimal amount)
    {
        Console.WriteLine();
        Console.WriteLine($"📧 NOTIFICATION: Bid accepted on \"{projectTitle}\"");
        Console.WriteLine($"Freelancer: {freelancerName} | Amount: ${amount}");
        Console.WriteLine("Project status updated to: InProgress");
        Console.WriteLine();
    }

    private static void HandleLowBudgetWarning(string projectTitle, decimal amount, decimal budget)
    {
        Console.WriteLine();
        Console.WriteLine($"⚠️ WARNING: Bid on \"{projectTitle}\" is {amount} (90% of {budget})");
        Console.WriteLine("Client should review before accepting.");
        Console.WriteLine();
    }
}