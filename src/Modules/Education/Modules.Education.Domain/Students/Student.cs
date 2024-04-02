using Core.Domain.Primitives;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Languages;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Education.Domain.Students;

public sealed class Student : AggregateRoot<UserId>
{
    public UserState State { get; private set; } = UserState.Pending;
    public Email Email { get; private set; } = null!;
    public Phone Phone { get; private set; } = null!;
    public FullName FullName { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public LanguageLevel LanguageLevel { get; private set; } = null!;
    public Date BirthDate { get; private set; } = null!;

    private Student()
    {
    }

    private Student(
        UserId id,
        UserState state,
        Email email,
        Phone phone,
        FullName fullName,
        Address address,
        LanguageLevel languageLevel,
        Date birthDate,
        InstitutionId institutionId
    )
        : base(id)
    {
        SetInstitutionId(institutionId);

        State = state;
        Email = email;
        Phone = phone;
        FullName = fullName;
        BirthDate = birthDate;
        Address = address;
        LanguageLevel = languageLevel;
    }

    public static Student Create(
        UserId id,
        UserState state,
        Email email,
        Phone phone,
        FullName fullName,
        Address address,
        LanguageLevel languageLevel,
        Date birthDate,
        InstitutionId institutionId
    ) => new(id, state, email, phone, fullName, address, languageLevel, birthDate, institutionId);
}