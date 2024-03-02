namespace Core.Domain.Shared.Enumerations.Roles;

/// <summary>
/// Represents the role of a user.
/// </summary>
public enum UserRole
{
    Student = 0,
    Teacher = 1,
    Secretary = 2,
    InstitutionAdmin = 3,

    Support = 4,
    BackOfficeAdmin = 5,
    GlobalAdmin = 6,

    System = 99
}