
namespace InstantHire.Domain.Entities;

public class Client
{
    // Primary Key
    public int Id { get; set; }

    // Company name 
    public string CompanyName { get; set; } = string.Empty;

    // One Client can have many Projects
    public List<Project> Projects { get; set; } = new();

    // Returns a simple summary about the client
    public string GetClientSummary()
    {
        return $"Company: {CompanyName} | Projects Count: {Projects.Count}";
    }
}