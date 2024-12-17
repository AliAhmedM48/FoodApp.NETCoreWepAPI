using Food.App.Core.Enums;

namespace Food.App.Service.RoleFeatureService;
public interface IRoleFeatureService
{
    Task<bool> HasAcess(Role role, Feature feature);
}
