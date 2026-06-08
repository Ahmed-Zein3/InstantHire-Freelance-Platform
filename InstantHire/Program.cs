using InstantHire.Data;
using InstantHire.Domain.Entities;
using InstantHire.Domain.Enums;
using InstantHire.Services;
using Microsoft.EntityFrameworkCore;

namespace InstantHire;

public class Program
{
    public static async Task Main(string[] args)
    {
        // =========================
        // EVENT SUBSCRIPTIONS
        // =========================
        NotificationService.Initialize();

        // =========================
        // DB CONNECTION
        // =========================
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer("Server=localhost;Database=InstantHireDB;Trusted_Connection=True;TrustServerCertificate=True")
            .Options;

        using var context = new AppDbContext(options);

        // =========================
        // SERVICES (ONCE ONLY)
        // =========================
        var inputService = new InputService();
        var projectAppService = new ProjectAppService(context, inputService);

        var reportService = new ReportService(context);
        var bidService = new BidService();

        // =========================
        // MENU LOOP
        // =========================
        while (true)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("         INSTANTHIRE PLATFORM");
            Console.WriteLine("========================================");

            Console.WriteLine("[1] Create Client + Project");
            Console.WriteLine("[2] Seed Data");
            Console.WriteLine("[3] List Freelancers");
            Console.WriteLine("[4] Submit Bid");
            Console.WriteLine("[5] Accept Bid");
            Console.WriteLine("[6] Add Review");
            Console.WriteLine("[7] Run Reports");
            Console.WriteLine("[0] Exit");

            Console.Write("Choose: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                // =========================
                // CREATE CLIENT + PROJECT
                // =========================
                case "1":
                    await projectAppService.CreateClientWithProject();
                    break;

                // =========================
                // SEED DATA
                // =========================
                case "2":
                    await SeedData(context);
                    break;

                // =========================
                // LIST FREELANCERS
                // =========================
                case "3":
                    await ListFreelancers(context);
                    break;

                // =========================
                // SUBMIT BID
                // =========================
                case "4":
                    await SubmitBid(context, bidService);
                    break;

                // =========================
                // ACCEPT BID
                // =========================
                case "5":
                    await AcceptBid(context, bidService);
                    break;

                // =========================
                // ADD REVIEW
                // =========================
                case "6":
                    await AddReviewFlow(context);
                    break;

                // =========================
                // REPORTS
                // =========================
                case "7":
                    await RunReports(reportService);
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid choice ❌");
                    break;
            }
        }
    }

    // =====================================================
    // SEED DATA
    // =====================================================
    static async Task SeedData(AppDbContext context)
    {
        if (await context.Clients.AnyAsync())
        {
            Console.WriteLine("Already seeded ✔");
            return;
        }

        var client = new Client
        {
            CompanyName = "Tech Corp"
        };

        var freelancer = new Freelancer
        {
            FullName = "Omar Khalil",
            Specialty = "Development",
            HourlyRate = 50
        };

        var project = new Project
        {
            Title = "Mobile App",
            Description = "Build a mobile app",
            Budget = 1000,
            Deadline = DateTime.Now.AddDays(7),
            Client = client
        };

        context.AddRange(client, freelancer, project);
        await context.SaveChangesAsync();

        Console.WriteLine("Seed Data Created 🔥");
    }

    // =====================================================
    // LIST FREELANCERS
    // =====================================================
    static async Task ListFreelancers(AppDbContext context)
    {
        var freelancers = await context.Freelancers
            .AsNoTracking()
            .ToListAsync();

        foreach (var f in freelancers)
        {
            Console.WriteLine($"{f.Id} | {f.FullName} | {f.Specialty}");
        }
    }

    // =====================================================
    // SUBMIT BID
    // =====================================================
    static async Task SubmitBid(AppDbContext context, BidService bidService)
    {
        var project = await context.Projects
            .Include(p => p.Bids)
            .FirstOrDefaultAsync();

        var freelancer = await context.Freelancers.FirstOrDefaultAsync();

        if (project == null || freelancer == null)
        {
            Console.WriteLine("Missing data ❌");
            return;
        }

        bidService.SubmitBid(project, freelancer, 500, "I can complete it fast");

        await context.SaveChangesAsync();

        Console.WriteLine("Bid Submitted ✔");
    }

    // =====================================================
    // ACCEPT BID
    // =====================================================
    static async Task AcceptBid(AppDbContext context, BidService bidService)
    {
        var project = await context.Projects
            .Include(p => p.Bids)
                .ThenInclude(b => b.Freelancer)
            .FirstOrDefaultAsync();

        if (project == null || project.Bids.Count == 0)
        {
            Console.WriteLine("No bids found ❌");
            return;
        }

        var bid = project.Bids.First();

        try
        {
            bidService.AcceptBid(project, bid);
            await context.SaveChangesAsync();

            Console.WriteLine("Bid Accepted ✔");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    // =====================================================
    // ADD REVIEW
    // =====================================================
    static async Task AddReviewFlow(AppDbContext context)
    {
        var project = await context.Projects
            .Include(p => p.Bids)
                .ThenInclude(b => b.Freelancer)
            .FirstOrDefaultAsync();

        if (project == null || project.Bids.Count == 0)
        {
            Console.WriteLine("No project or bids ❌");
            return;
        }

        var acceptedBid = project.Bids.FirstOrDefault(b => b.Status == BidStatus.Accepted);

        if (acceptedBid == null)
        {
            Console.WriteLine("No accepted bid ❌");
            return;
        }

        Console.Write("Enter rating (1-5): ");
        int rating = int.Parse(Console.ReadLine()!);

        Console.Write("Enter comment: ");
        string comment = Console.ReadLine()!;

        var review = new Review
        {
            Rating = rating,
            Comment = comment,
            ProjectId = project.Id,
            FreelancerId = acceptedBid.FreelancerId
        };

        context.Reviews.Add(review);
        await context.SaveChangesAsync();

        Console.WriteLine("Review added successfully ✔");
    }

    // =====================================================
    // REPORTS
    // =====================================================
    static async Task RunReports(ReportService reportService)
    {
        Console.WriteLine("\n--- TOP FREELANCERS ---");
        var top = await reportService.GetTopFreelancers(5);
        foreach (var item in top)
            Console.WriteLine(item);

        Console.WriteLine("\n--- HIGH BIDS ---");
        var highBids = await reportService.GetHighBids(0.8m);
        foreach (var item in highBids)
            Console.WriteLine(item);

        Console.WriteLine("\n--- CLIENT REPORT ---");
        var clients = await reportService.GetClientSpendReport();
        foreach (var item in clients)
            Console.WriteLine(item);
    }
}