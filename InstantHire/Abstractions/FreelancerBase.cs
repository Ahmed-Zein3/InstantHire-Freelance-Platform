using System;
using System.Collections.Generic;
using System.Text;

namespace InstantHire.Abstractions;

public abstract class FreelancerBase
{
    // Freelancer full name
    public string FullName { get; set; } = string.Empty;

    // Freelancer specialty
    public string Specialty { get; set; } = string.Empty;

    // Freelancer hourly rate
    public decimal HourlyRate { get; set; }

    // Must be implemented by derived classes
    public abstract string GetProfileSummary();
}