namespace Common.Utilities.Emails.Models;

public sealed class PasswordResetCodeEmailTemplate : EmailTemplate
{
    public string UserName { get; }
    public string ResetCode { get; }
    public int CodeExpiryInHours { get; }

    private PasswordResetCodeEmailTemplate(string userName, string resetCode, int codeExpiryInHours)
    {
        UserName = userName;
        ResetCode = resetCode;
        CodeExpiryInHours = codeExpiryInHours;
    }

    public static PasswordResetCodeEmailTemplate Populate(string userName, string resetCode, int codeExpiryInHours)
        => new(userName, resetCode, codeExpiryInHours);
}