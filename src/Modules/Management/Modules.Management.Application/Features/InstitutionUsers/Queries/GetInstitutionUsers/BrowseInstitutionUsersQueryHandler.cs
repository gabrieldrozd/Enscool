using Common.Utilities.Extensions;
using Common.Utilities.Primitives.Results;
using Core.Application.Communication.Internal.Queries.Browse;
using Core.Application.Extensions;
using Core.Application.Queries.Browse;
using Core.Domain.Shared.Enumerations.UserStates;
using Microsoft.EntityFrameworkCore;
using Modules.Management.Application.Abstractions;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.InstitutionUsers.Queries.GetInstitutionUsers;