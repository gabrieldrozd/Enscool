namespace Common.Utilities.Emails.Models;

public sealed class InstitutionUserCreatedEmailTemplate : EmailTemplate
{
    public string UserName { get; }
    public string RoleName { get; }
    public string ActivationLink { get; }

    private InstitutionUserCreatedEmailTemplate(string userName, string roleName, string activationLink)
    {
        UserName = userName;
        RoleName = roleName;
        ActivationLink = activationLink;
    }

    public static InstitutionUserCreatedEmailTemplate Populate(string userName, string roleName, string activationLink)
        => new(userName, roleName, activationLink);
}