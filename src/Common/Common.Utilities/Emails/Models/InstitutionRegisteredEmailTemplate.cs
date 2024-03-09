namespace Common.Utilities.Emails.Models;

public sealed class InstitutionRegisteredEmailTemplate : EmailTemplate
{
    public string UserName { get; }
    public string ActivationLink { get; }

    private InstitutionRegisteredEmailTemplate(string userName, string activationLink)
    {
        UserName = userName;
        ActivationLink = activationLink;
    }

    public static InstitutionRegisteredEmailTemplate Populate(string userName, string activationLink)
        => new(userName, activationLink);
}