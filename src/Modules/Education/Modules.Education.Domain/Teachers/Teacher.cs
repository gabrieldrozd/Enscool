using Core.Domain.Primitives;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.ValueObjects;
using Modules.Education.Domain.Courses;

namespace Modules.Education.Domain.Teachers;

public sealed class Teacher : AggregateRoot<UserId>
{
    private readonly List<CourseId> _courseIds = [];

    public UserState State { get; private set; }
    public Email Email { get; private set; } = null!;
    public Phone Phone { get; private set; } = null!;
    public FullName FullName { get; private set; } = null!;
    public Address Address { get; private set; } = null!;

    public IReadOnlyList<CourseId> CourseIds => _courseIds;

    private Teacher()
    {
    }

    private Teacher(
        UserId teacherId,
        UserState state,
        Email email,
        Phone phone,
        FullName fullName,
        Address address,
        InstitutionId institutionId
    )
        : base(teacherId)
    {
        SetInstitutionId(institutionId);

        State = state;
        Email = email;
        Phone = phone;
        FullName = fullName;
        Address = address;
    }

    public static Teacher Create(
        UserId teacherId,
        UserState state,
        Email email,
        Phone phone,
        FullName fullName,
        Address address,
        InstitutionId institutionId
    ) => new(teacherId, state, email, phone, fullName, address, institutionId);
}