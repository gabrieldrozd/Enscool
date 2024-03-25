namespace Modules.Management.Domain.Users;

/// <summary>
/// Represents the origin of the user.
/// </summary>
public enum UserType
{
    /// <summary>
    /// Represents an institution user.
    /// </summary>
    Institution = 1,

    /// <summary>
    /// Represents a back office user.
    /// </summary>
    BackOffice = 2
}