using System;
using System.Collections.Generic;
using System.Text;

namespace InstantHire.Abstractions;

public interface IReviewable
{
    // Determines whether the entity can receive a review
    bool CanReceiveReview();

    // Returns a summary of reviews
    string GetReviewSummary();
}