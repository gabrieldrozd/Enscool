using System.Text.Json;
using Common.Utilities.Exceptions;
using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Core.Application.Auth;
using Core.Infrastructure.Auth.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Auth;

public static class JwtBearerEventHandlers
{
    internal static async Task OnTokenReceived(MessageReceivedContext context)
    {
        var token = RequestMetadataExtractor.GetToken(context.HttpContext);
        if (string.IsNullOrWhiteSpace(token))
        {
            context.Fail(new NotAuthenticatedException("Token is not present in the request"));
            return;
        }

        var claims = RequestMetadataExtractor.DecodeToken(token);
        var userId = ClaimsExtractor.GetUserId(claims);
        if (userId == null)
        {
            context.Fail(new NotAuthenticatedException("UserId could not be extracted from the token"));
            return;
        }

        var blockedTokenStore = context.HttpContext.RequestServices.GetRequiredService<IBlockedTokenStore>();
        if (await blockedTokenStore.IsBlockedAsync(userId.Value, token))
        {
            context.Fail(new NotAuthenticatedException("Token is blocked"));
        }
    }

    internal static async Task OnAuthenticationChallenge(JwtBearerChallengeContext context)
    {
        if (context.AuthenticateFailure is not NotAuthenticatedException exception)
            return;

        var result = Result.Failure.Unauthorized(exception.Message);
        var envelope = result.ToEnvelope().WithCode(StatusCodes.Status401Unauthorized);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

        var responsePayload = JsonSerializer.Serialize(envelope);
        await context.Response.WriteAsync(responsePayload);
        context.HandleResponse();
    }
}