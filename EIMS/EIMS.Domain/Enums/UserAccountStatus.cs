namespace EIMS.Domain.Enums
{
    public enum UserAccountStatus
    {
        PendingEvidence = 0,    // Account created, waiting for HOD to upload evidence
        PendingAdminReview = 1, // Evidence uploaded, waiting for Admin approval
        Active = 2,             // Account approved and active
        Declined = 3,           // Account declined by Admin
        Suspended = 4,          // Account temporarily suspended
        Deactivated = 5
    }
}