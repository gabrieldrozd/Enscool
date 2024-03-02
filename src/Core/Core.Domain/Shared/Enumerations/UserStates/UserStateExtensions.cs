using Common.Utilities.Exceptions;
using Common.Utilities.Resources;

namespace Core.Domain.Shared.Enumerations.UserStates;

public static class UserStateExtensions
{
    private static readonly Dictionary<UserState, IEnumerable<UserState>> NextStates = new()
    {
        [UserState.Pending] = [UserState.Active, UserState.Deleted],
        [UserState.Active] = [UserState.Inactive, UserState.Deleted],
        [UserState.Inactive] = [UserState.Active, UserState.Deleted],
        [UserState.Deleted] = [UserState.Active]
    };

    public static void ValidateTransitionTo(this UserState current, UserState next)
    {
        if (!NextStates[current].Contains(next))
            throw new DomainException(Resource.InvalidUserStateTransition, current, next);
    }
}