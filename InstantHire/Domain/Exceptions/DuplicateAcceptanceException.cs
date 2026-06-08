using System;
using System.Collections.Generic;
using System.Text;

namespace InstantHire.Domain.Exceptions;

public class DuplicateAcceptanceException : Exception
{
    // Constructor with custom message
    public DuplicateAcceptanceException(string projectTitle)
        : base($"Project '{projectTitle}' already has an accepted bid. Reject it first.")
    {
    }
}