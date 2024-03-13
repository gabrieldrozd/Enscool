using Common.Utilities.Emails.Models;
using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.External.Emails;
using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.ValueObjects;
using Modules.Management.Application.Abstractions;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Domain.Abstractions;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Users.Commands.Register;

internal sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand>
{
    private readonly IEmailQueue _emailQueue;
    private readonly IActivationLinkService _activationLinkService;
    private readonly IActivationCodeService _activationCodeService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(
        IEmailQueue emailQueue,
        IActivationLinkService activationLinkService,
        IActivationCodeService activationCodeService,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _emailQueue = emailQueue;
        _activationLinkService = activationLinkService;
        _activationCodeService = activationCodeService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsWithEmailAsync(request.Email, cancellationToken))
            return Result.Failure.BadRequest(Resource.EmailTaken);

        var user = User.CreateInitialInstitutionAdmin(
            Email.Parse(request.Email),
            Phone.Parse(request.Phone),
            FullName.Create(request.FirstName, request.MiddleName, request.LastName),
            _activationCodeService.Generate());

        _userRepository.Insert(user);
        return await _unitOfWork.CommitAsync(cancellationToken)
            .MatchOrBadRequest(() =>
            {
                var template = InstitutionRegisteredEmailTemplate.Populate(user.FullName.First, _activationLinkService.Create(user));
                var emailMessage = EmailMessage.Create(user.Email, user.FullName, template);
                _emailQueue.Enqueue(emailMessage);
                return Result.Success.Ok();
            });
    }
}