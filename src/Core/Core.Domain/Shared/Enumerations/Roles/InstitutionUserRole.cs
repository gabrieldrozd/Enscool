namespace Core.Domain.Shared.Enumerations.Roles;

/// <summary>
/// Represents the role of an institution user.
/// </summary>
public enum InstitutionUserRole
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
    InstitutionAdmin = 4
}