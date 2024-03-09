namespace Services.Email.Sender.Builder;

public interface IWithImportance
{
    IWithSubject WithSubject(string subject);
}