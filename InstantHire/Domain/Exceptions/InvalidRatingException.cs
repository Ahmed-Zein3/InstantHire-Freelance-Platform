namespace InstantHire.Domain.Exceptions;

public class InvalidRatingException : Exception
{
    // Constructor validates rating range (1–5)
    public InvalidRatingException(int rating)
        : base($"Rating must be between 1 and 5. Received: {rating}")
    {
    }
}
