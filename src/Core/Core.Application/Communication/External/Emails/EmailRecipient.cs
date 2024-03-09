using Core.Domain.Shared.ValueObjects;

namespace Core.Application.Communication.External.Emails;

public sealed class EmailRecipient
{
    public string ToAddress { get; private set; }
    public string ToName { get; private set; }

    private EmailRecipient(string toAddress, string toName)
    {
        ToAddress = toAddress;
        ToName = toName;
    }

    /// <summary>
    /// Creates an email recipient.
    /// </summary>
    /// <param name="toAddress">Email address of the recipient.</param>
    /// <param name="toName">Name of the recipient.</param>
    /// <returns>Email recipient.</returns>
    public static EmailRecipient Create(string toAddress, string toName)
        => new(toAddress, toName);

    public static implicit operator (Email toAddress, FullName toName)(EmailRecipient recipient)
        => (recipient.ToAddress, FullName.FromString(recipient.ToName));

    public static implicit operator EmailRecipient((Email toAddress, FullName toName) recipient)
        => new(recipient.toAddress.Value, recipient.toName.ToString());
}