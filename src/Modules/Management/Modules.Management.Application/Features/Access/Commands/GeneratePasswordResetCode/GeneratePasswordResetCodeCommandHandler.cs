using Common.Utilities.Emails.Models;
using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.External.Emails;
using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.Enumerations.UserStates;
using Microsoft.Extensions.Options;
using Modules.Management.Application.Abstractions;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Application.Abstractions.Settings;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Access.Commands.GeneratePasswordResetCode;

internal sealed class GeneratePasswordResetCodeCommandHandler : ICommandHandler<GeneratePasswordResetCodeCommand>
{
    private readonly PasswordResetSettings _passwordResetSettings;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailQueue _emailQueue;

    public GeneratePasswordResetCodeCommandHandler(
        IOptions<PasswordResetSettings> passwordResetSettings,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IEmailQueue emailQueue)
    {
        _passwordResetSettings = passwordResetSettings.Value;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _emailQueue = emailQueue;
    }

    public async Task<Result> Handle(GeneratePasswordResetCodeCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.Email, cancellationToken);

        if (user is null || user.State != UserState.Active)
            return Result.Failure.BadRequest(Resource.GeneratePasswordResetCodeError);

        var passwordResetCode = PasswordResetCode.Create(_passwordResetSettings.CodeExpiryInHours);
        user.AddPasswordResetCode(passwordResetCode);

        return await _unitOfWork.CommitAsync(cancellationToken)
            .Map(() =>
            {
                var template = PasswordResetCodeEmailTemplate.Populate(user.FullName.First, passwordResetCode.Value, _passwordResetSettings.CodeExpiryInHours);
                var emailMessage = EmailMessage.Create(user.Email, user.FullName, template);
                _emailQueue.Enqueue(emailMessage);
                return Result.Success.Ok();
            });
    }
}