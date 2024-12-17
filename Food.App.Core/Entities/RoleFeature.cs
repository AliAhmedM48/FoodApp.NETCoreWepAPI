using Food.App.Core.Enums;

namespace Food.App.Core.Entities;
public class RoleFeature : BaseEntity
{
    public Role Role { get; set; }
    public Feature Feature { get; set; }
}
