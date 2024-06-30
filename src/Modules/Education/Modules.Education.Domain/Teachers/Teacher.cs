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
    public string FirstName { get; private set; } = null!;
    public string? MiddleName { get; private set; }
    public string LastName { get; private set; } = null!;
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
        string firstName,
        string? middleName,
        string lastName,
        Address address,
        InstitutionId institutionId
    )
        : base(teacherId)
    {
        SetInstitutionId(institutionId);

        State = state;
        Email = email;
        Phone = phone;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Address = address;
    }

    public static Teacher Create(
        UserId teacherId,
        UserState state,
        Email email,
        Phone phone,
        string firstName,
        string? middleName,
        string lastName,
        Address address,
        InstitutionId institutionId
    ) => new(teacherId, state, email, phone, firstName, middleName, lastName, address, institutionId);

    public void Update(
        Phone phone,
        string firstName,
        string? middleName,
        string lastName,
        Address address)
    {
        Phone = phone;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Address = address;
    }

    public void Reactivate()
    {
        State.ValidateTransitionTo(UserState.Active);
        State = UserState.Active;
    }

    public void Deactivate()
    {
        State.ValidateTransitionTo(UserState.Inactive);
        State = UserState.Inactive;
    }

    public override void Restore()
    {
        State.ValidateTransitionTo(UserState.Active);
        State = UserState.Active;
        base.Restore();
    }

    public override void Delete()
    {
        State.ValidateTransitionTo(UserState.Deleted);
        State = UserState.Deleted;
        base.Delete();
    }
}