using Core.Domain.Primitives;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Management.Domain.Institutions;

public sealed class Institution : AggregateRoot<InstitutionId>
{
    private readonly List<UserId> _administratorIds = [];

    public InstitutionState State { get; private set; }
    public string? ShortName { get; private set; }
    public string? FullName { get; private set; }
    public Address? Address { get; private set; }

    public IReadOnlyList<UserId> AdministratorIds => _administratorIds.AsReadOnly();

    private Institution()
    {
    }

    private Institution(InstitutionId id, UserId administratorId)
        : base(id)
    {
        SetInstitutionId(id);
        AddAdministrators([administratorId]);
    }

    private void AddAdministrators(List<UserId> userIds)
    {
        foreach (var administratorId in userIds.Where(userId => !_administratorIds.Contains(userId)))
            _administratorIds.Add(administratorId);
    }
}