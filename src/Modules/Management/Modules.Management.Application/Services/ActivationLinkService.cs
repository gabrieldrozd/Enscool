using Microsoft.Extensions.Options;
using Modules.Management.Application.Abstractions.Settings;
using Modules.Management.Domain.Abstractions;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Services;

internal sealed class ActivationLinkService : IActivationLinkService
{
    private readonly AccountActivationSettings _settings;

    public ActivationLinkService(IOptions<AccountActivationSettings> settings) => _settings = settings.Value;

    public string Create(User user)
        => string.Format(_settings.ActivationLinkFormat, _settings.BaseUrl, user.Id, user.CurrentActivationCode?.Value);
}