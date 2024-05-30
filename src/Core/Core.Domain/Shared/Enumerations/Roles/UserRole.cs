namespace Core.Domain.Shared.Enumerations.Roles;

/// <summary>
/// Represents the role of a user.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// [Institution] Represents a student.
    /// </summary>
    Student = 1,

    /// <summary>
    /// [Institution] Represents a teacher.
    /// </summary>
    Teacher = 2,

    /// <summary>
    /// [Institution] Represents a secretary.
    /// </summary>
    Secretary = 3,

    /// <summary>
    /// [Institution] Represents an institution admin.
    /// </summary>
    InstitutionAdmin = 4,

    /// <summary>
    /// [BackOffice] Represents a support user.
    /// </summary>
    Support = 5,

    /// <summary>
    /// [BackOffice] Represents a back office admin.
    /// </summary>
    BackOfficeAdmin = 6,

    /// <summary>
    /// [BackOffice] Represents a global admin.
    /// </summary>
    GlobalAdmin = 7,

    /// <summary>
    /// [SYSTEM] Represents a system.
    /// </summary>
    System = 99
}