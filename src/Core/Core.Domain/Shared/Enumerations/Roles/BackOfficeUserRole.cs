namespace Core.Domain.Shared.Enumerations.Roles;

/// <summary>
/// Represents the role of a back office user.
/// </summary>
public enum BackOfficeUserRole
{
    /// <summary>
    /// [BackOffice] Represents a support user.
    /// </summary>
    Support = 4,

    /// <summary>
    /// [BackOffice] Represents a back office admin.
    /// </summary>
    BackOfficeAdmin = 5,

    /// <summary>
    /// [BackOffice] Represents a global admin.
    /// </summary>
    GlobalAdmin = 6
}