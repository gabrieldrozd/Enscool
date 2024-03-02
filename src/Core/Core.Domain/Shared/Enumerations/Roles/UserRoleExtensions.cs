namespace Core.Domain.Shared.Enumerations.Roles;

public static class UserRoleExtensions
{
    public static bool IsInstitutionRole(this UserRole role)
        => role switch
        {
            UserRole.Student => true,
            UserRole.Teacher => true,
            UserRole.Secretary => true,
            UserRole.InstitutionAdmin => true,
            UserRole.Support => false,
            UserRole.BackOfficeAdmin => false,
            UserRole.GlobalAdmin => false,
            UserRole.System => false,
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };

    public static bool IsBackOfficeRole(this UserRole role)
        => role switch
        {
            UserRole.Student => false,
            UserRole.Teacher => false,
            UserRole.Secretary => false,
            UserRole.InstitutionAdmin => false,
            UserRole.Support => true,
            UserRole.BackOfficeAdmin => true,
            UserRole.GlobalAdmin => true,
            UserRole.System => false,
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };

    public static bool IsStudent(this UserRole role) => role is UserRole.Student;
    public static bool IsTeacher(this UserRole role) => role is UserRole.Teacher;
    public static bool IsSecretary(this UserRole role) => role is UserRole.Secretary;
    public static bool IsInstitutionAdmin(this UserRole role) => role is UserRole.InstitutionAdmin;
    public static bool IsSupport(this UserRole role) => role is UserRole.Support;
    public static bool IsBackOfficeAdmin(this UserRole role) => role is UserRole.BackOfficeAdmin;
    public static bool IsGlobalAdmin(this UserRole role) => role is UserRole.GlobalAdmin;

    public static UserRole ToUserRole(this Enum role)
    {
        var roleString = role.ToString();
        return roleString switch
        {
            "Student" => UserRole.Student,
            "Teacher" => UserRole.Teacher,
            "Secretary" => UserRole.Secretary,
            "InstitutionAdmin" => UserRole.InstitutionAdmin,
            "Support" => UserRole.Support,
            "BackOfficeAdmin" => UserRole.BackOfficeAdmin,
            "GlobalAdmin" => UserRole.GlobalAdmin,
            "System" => UserRole.System,
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
    }
}