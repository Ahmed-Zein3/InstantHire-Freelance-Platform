using System;
using System.Collections.Generic;
using System.Text;

namespace InstantHire.Domain.Exceptions;

public class InvalidBidException : Exception
{
    // Constructor with custom message
    public InvalidBidException()
        : base("Bid amount must be greater than zero.")
    {
    }

    // Overload constructor (optional flexibility)
    public InvalidBidException(decimal amount)
        : base($"Invalid bid amount: {amount}. Bid amount must be greater than zero.")
    {
    }
}