using Food.App.Core.Entities;
using Food.App.Core.Enums;
using Food.App.Core.Interfaces;

namespace Food.App.Service.RoleFeatureService;
public class RoleFeatureService : IRoleFeatureService
{
    private readonly IUnitOfWork _UnitOfWork;
    public RoleFeatureService(IUnitOfWork unitOfWork)
    {
        _UnitOfWork = unitOfWork;
    }
    public async Task<bool> HasAcess(Role role, Feature feature)
    {
        var result = await _UnitOfWork.GetRepository<RoleFeature>().AnyAsync(x => x.Role == role && x.Feature == feature);
        return result;
    }

}
