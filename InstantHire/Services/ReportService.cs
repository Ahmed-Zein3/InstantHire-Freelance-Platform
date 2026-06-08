using Microsoft.EntityFrameworkCore;
using InstantHire.Domain.DTOs;
using InstantHire.Data;
using InstantHire.Domain.Entities;
using InstantHire.Domain.Enums;

namespace InstantHire.Services;

public class ReportService
{
    private readonly AppDbContext _context;

    public ReportService(AppDbContext context)
    {
        _context = context;
    }

    // =====================================================
    // 1. Top Freelancers by reputation (AVG reviews)
    // =====================================================
    public async Task<List<FreelancerSummary>> GetTopFreelancers(int n)
    {
        var freelancers = await _context.Freelancers
            .Include(f => f.Reviews)
            .AsNoTracking()
            .ToListAsync();

        return freelancers
            .Select(f => new FreelancerSummary(
                f.FullName,
                f.Specialty,
                f.Reviews.Any() ? f.Reviews.Average(r => r.Rating) : 0,
                f.Reviews.Count
            ))
            .OrderByDescending(f => f.AvgRating)
            .Take(n)
            .ToList();
    }

    // =====================================================
    // 2. Projects with no bids yet
    // =====================================================
    public async Task<List<Project>> GetUnbidProjects()
    {
        return await _context.Projects
            .Include(p => p.Bids)
            .AsNoTracking()
            .Where(p => !p.Bids.Any())
            .ToListAsync();
    }

    // =====================================================
    // 3. High bids above threshold percentage of budget
    // =====================================================
    public async Task<List<BidSummary>> GetHighBids(decimal thresholdPercent)
    {
        var result = await _context.Bids
            .Include(b => b.Project)
            .Include(b => b.Freelancer)
            .AsNoTracking()
            .Where(b => b.Project.Budget > 0 &&
                        (b.Amount / b.Project.Budget) >= thresholdPercent)
            .Select(b => new BidSummary(
                b.Project.Title,
                b.Freelancer.FullName,
                b.Amount,
                (b.Amount / b.Project.Budget) * 100
            ))
            .ToListAsync();

        return result;
    }

    // =====================================================
    // 4. Search freelancers by skill
    // =====================================================
    public async Task<List<Freelancer>> SearchBySkill(string skill)
    {
        return await _context.Freelancers
            .Include(f => f.Skills)
            .AsNoTracking()
            .Where(f => f.Skills.Any(s => s.Name.ToLower() == skill.ToLower()))
            .ToListAsync();
    }

    // =====================================================
    // 5. Client spending report
    // =====================================================
    public async Task<List<ClientSpendReport>> GetClientSpendReport()
    {
        var clients = await _context.Clients
            .Include(c => c.Projects)
                .ThenInclude(p => p.Bids)
            .AsNoTracking()
            .ToListAsync();

        return clients
            .Select(c => new ClientSpendReport(
                c.CompanyName,
                c.Projects.Count(p => p.Status == ProjectStatus.Completed),
                c.Projects
                    .SelectMany(p => p.Bids)
                    .Where(b => b.Status == BidStatus.Accepted)
                    .Sum(b => b.Amount)
            ))
            .ToList();
    }
}