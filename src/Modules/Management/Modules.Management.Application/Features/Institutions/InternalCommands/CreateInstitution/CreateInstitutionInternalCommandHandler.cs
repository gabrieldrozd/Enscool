using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Commands;
using Modules.Management.Application.Abstractions;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Domain.Institutions;

namespace Modules.Management.Application.Features.Institutions.InternalCommands.CreateInstitution;

internal sealed class CreateInstitutionInternalCommandHandler : ICommandHandler<CreateInstitutionInternalCommand>
{
    private readonly IInstitutionRepository _institutionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInstitutionInternalCommandHandler(IInstitutionRepository institutionRepository, IUnitOfWork unitOfWork)
    {
        _institutionRepository = institutionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateInstitutionInternalCommand request, CancellationToken cancellationToken)
    {
        if (await _institutionRepository.ExistsAsync(request.InstitutionId, cancellationToken))
            return Result.Failure.BadRequest(Resource.InstitutionAlreadyExists);

        var institution = Institution.Create(request.InstitutionId, request.UserId);

        _institutionRepository.Insert(institution);
        return await _unitOfWork.CommitAsync(cancellationToken)
            .Map(Result.Success.Ok);
    }
}