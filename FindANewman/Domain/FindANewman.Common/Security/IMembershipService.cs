namespace FindANewman.Common.Security
{
    public interface IMembershipService
    {
        MembershipValidationResult ValidateUser(string username, string password);
    }
}
