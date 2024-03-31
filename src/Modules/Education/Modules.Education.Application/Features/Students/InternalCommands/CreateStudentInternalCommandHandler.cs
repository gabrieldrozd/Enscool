using Common.Utilities.Primitives.Results;
using Core.Application.Communication.Internal.Commands;
using MediatR;

namespace Modules.Education.Application.Features.Students.InternalCommands;

internal sealed class CreateStudentInternalCommandHandler : ICommandHandler<CreateStudentInternalCommand>
{
    public Task<Result> Handle(CreateStudentInternalCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}