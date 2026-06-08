namespace InstantHire.Domain.Exceptions;

public class FreelancerNotFoundException : Exception
{
    // Constructor takes freelancer id for better debugging
    public FreelancerNotFoundException(int id)
        : base($"Freelancer with Id {id} was not found.")
    {
    }
}