using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Languages;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Education.Application.Features.Students.InternalCommands.CreateStudent;

public sealed record CreateStudentInternalCommand(
    UserId UserId,
    UserState State,
    Email Email,
    Phone Phone,
    string FirstName,
    string? MiddleName,
    string LastName,
    Date BirthDate,
    Address Address,
    LanguageLevel LanguageLevel,
    InstitutionId InstitutionId
) : IInternalCommand;