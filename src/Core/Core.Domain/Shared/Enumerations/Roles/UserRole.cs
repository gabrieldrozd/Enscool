namespace Core.Domain.Shared.Enumerations.Roles;

/// <summary>
/// Represents the role of a user.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// [Institution] Represents a student.
    /// </summary>
    Student = 0,

    /// <summary>
    /// [Institution] Represents a teacher.
    /// </summary>
    Teacher = 1,

    /// <summary>
    /// [Institution] Represents a secretary.
    /// </summary>
    Secretary = 2,

    /// <summary>
    /// [Institution] Represents an institution admin.
    /// </summary>
    InstitutionAdmin = 3,

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
    GlobalAdmin = 6,

    /// <summary>
    /// [SYSTEM] Represents a system.
    /// </summary>
    System = 99
}