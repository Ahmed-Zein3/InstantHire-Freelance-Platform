using InstantHire.Domain.Entities;
using InstantHire.Domain.Enums;
using InstantHire.Data;

namespace InstantHire.Services;

public class ProjectAppService
{
    private readonly AppDbContext _context;
    private readonly InputService _input;

    public ProjectAppService(AppDbContext context, InputService input)
    {
        _context = context;
        _input = input;
    }

    // =========================
    // Create Client + Project Flow
    // =========================
    public async Task CreateClientWithProject()
    {
        Console.WriteLine("=== Create Client + Project ===");

       
        var companyName = _input.ReadString("Enter Company Name: ");
        var projectTitle = _input.ReadString("Enter Project Title: ");
        var budget = _input.ReadDecimal("Enter Budget: ");

        var client = new Client
        {
            CompanyName = companyName
        };

        var project = new Project
        {
            Title = projectTitle,
            Budget = budget,
            Deadline = DateTime.Now.AddDays(7)
        };

        client.Projects.Add(project);

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        Console.WriteLine("Client + Project created successfully ✔");
    }
}