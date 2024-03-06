using System.ComponentModel.DataAnnotations;
using Common.Utilities.Resources;

namespace Modules.Management.Domain.Institutions;

public enum InstitutionState
{
    [Display(Name = nameof(Resource.InstitutionStateNew), ResourceType = typeof(Resource))]
    New = 0,

    [Display(Name = nameof(Resource.InstitutionStateActive), ResourceType = typeof(Resource))]
    Active = 1,

    [Display(Name = nameof(Resource.InstitutionStateInactive), ResourceType = typeof(Resource))]
    Inactive = 2,

    [Display(Name = nameof(Resource.InstitutionStateDeleted), ResourceType = typeof(Resource))]
    Deleted = 3
}