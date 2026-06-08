using InstantHire.Data;
using InstantHire.Domain.Entities;
using InstantHire.Domain.Exceptions;

namespace InstantHire.Services;

public class ProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }

    // =====================================================
    // COMPLETE PROJECT + ADD REVIEW
    // =====================================================
    public async Task AddReview(Project project, int rating, string comment)
    {
        // Check if project is completed
        if (project.Status != Domain.Enums.ProjectStatus.Completed)
            throw new Exception("Project must be completed before adding review");

        // Prevent duplicate review
        if (project.Review != null)
            throw new Exception("Project already has a review");

        var review = new Review
        {
            Rating = rating,
            Comment = comment,
            ProjectId = project.Id,
            FreelancerId = project.Bids
                .First(b => b.Status == Domain.Enums.BidStatus.Accepted)
                .FreelancerId,
            CreatedAt = DateTime.Now
        };

        project.Review = review;

        _context.Reviews.Add(review);

        await _context.SaveChangesAsync();
    }
}