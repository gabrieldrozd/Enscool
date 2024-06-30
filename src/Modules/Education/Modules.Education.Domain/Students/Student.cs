using Core.Domain.Primitives;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Languages;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Education.Domain.Students;

public sealed class Student : AggregateRoot<UserId>
{
    public UserState State { get; private set; }
    public Email Email { get; private set; } = null!;
    public Phone Phone { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string? MiddleName { get; private set; }
    public string LastName { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public LanguageLevel LanguageLevel { get; private set; } = null!;
    public Date BirthDate { get; private set; } = null!;

    private Student()
    {
    }

    private Student(
        UserId studentId,
        UserState state,
        Email email,
        Phone phone,
        string firstName,
        string? middleName,
        string lastName,
        Address address,
        LanguageLevel languageLevel,
        Date birthDate,
        InstitutionId institutionId
    )
        : base(studentId)
    {
        SetInstitutionId(institutionId);

        State = state;
        Email = email;
        Phone = phone;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Address = address;
        LanguageLevel = languageLevel;
        BirthDate = birthDate;
    }

    public static Student Create(
        UserId studentId,
        UserState state,
        Email email,
        Phone phone,
        string firstName,
        string? middleName,
        string lastName,
        Address address,
        LanguageLevel languageLevel,
        Date birthDate,
        InstitutionId institutionId
    ) => new(studentId, state, email, phone, firstName, middleName, lastName, address, languageLevel, birthDate, institutionId);

    public void Update(
        Phone phone,
        string firstName,
        string? middleName,
        string lastName,
        Address address,
        Date birthDate)
    {
        Phone = phone;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Address = address;
        BirthDate = birthDate;
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