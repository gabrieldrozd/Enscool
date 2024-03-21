using Core.Domain.Primitives.Enumerations;
using Microsoft.AspNetCore.Routing.Matching;

namespace Modules.Management.Api.Endpoints.Users;

public sealed record AccessEndpointsOrder : Enumeration<AccessEndpointsOrder>
{
    public static readonly AccessEndpointsOrder GetCurrentUser = new(1, nameof(GetCurrentUser));
    public static readonly AccessEndpointsOrder Register = new(2, nameof(Register));
    public static readonly AccessEndpointsOrder Login = new(3, nameof(Login));
    public static readonly AccessEndpointsOrder Refresh = new(4, nameof(Refresh));
    public static readonly AccessEndpointsOrder GenerateResetPasswordCode = new(5, nameof(GenerateResetPasswordCode));
    public static readonly AccessEndpointsOrder ResetPassword = new(6, nameof(ResetPassword));
    public static readonly AccessEndpointsOrder ChangePassword = new(7, nameof(ChangePassword));
    public static readonly AccessEndpointsOrder Activate = new(8, nameof(Activate));

    public AccessEndpointsOrder(int id, string value) : base(id, value)
    {
    }
}