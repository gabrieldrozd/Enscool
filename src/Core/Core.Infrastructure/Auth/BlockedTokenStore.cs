using Core.Application.Auth;
using StackExchange.Redis;

namespace Core.Infrastructure.Auth;

internal sealed class BlockedTokenStore : IBlockedTokenStore
{
    private const string BlockedTokensKey = "BlockedTokens:{0}";

    private readonly IDatabase _redisDatabase;

    public BlockedTokenStore(IConnectionMultiplexer connectionMultiplexer)
    {
        _redisDatabase = connectionMultiplexer.GetDatabase();
    }

    public async Task BlockAsync(Guid userId, string accessToken)
        => await _redisDatabase.SetAddAsync(string.Format(BlockedTokensKey, userId), accessToken);

    public async Task<bool> IsBlockedAsync(Guid userId, string accessToken)
        => await _redisDatabase.SetContainsAsync(string.Format(BlockedTokensKey, userId), accessToken);
}