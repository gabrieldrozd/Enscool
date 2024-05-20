using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Education.Application.Features.Teachers.InternalCommands.CreateTeacher;

public sealed record CreateTeacherInternalCommand(
    UserId UserId,
    UserState State,
    Email Email,
    Phone Phone,
    FullName FullName,
    Address Address,
    InstitutionId InstitutionId
) : IInternalCommand;