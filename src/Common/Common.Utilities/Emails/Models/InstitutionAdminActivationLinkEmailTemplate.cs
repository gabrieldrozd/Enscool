namespace Common.Utilities.Emails.Models;

public sealed class InstitutionAdminActivationLinkEmailTemplate : EmailTemplate
{
    public string UserName { get; }
    public string ActivationLink { get; }

    private InstitutionAdminActivationLinkEmailTemplate(string userName, string activationLink)
    {
        UserName = userName;
        ActivationLink = activationLink;
    }

    public static InstitutionAdminActivationLinkEmailTemplate Populate(string userName, string activationLink)
        => new(userName, activationLink);
}