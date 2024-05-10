using Microsoft.Extensions.Options;
using Modules.Management.Application.Abstractions.Settings;
using Modules.Management.Domain.Abstractions;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Services;

internal sealed class ActivationCodeService : IActivationCodeService
{
    private readonly AccountActivationSettings _settings;

    public ActivationCodeService(IOptions<AccountActivationSettings> settings) => _settings = settings.Value;

    public ActivationCode Generate() => ActivationCode.Create(_settings.CodeExpiryInHours);
}