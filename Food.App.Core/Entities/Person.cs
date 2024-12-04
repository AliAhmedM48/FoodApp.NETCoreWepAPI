using Food.App.Core.Enums;

namespace Food.App.Core.Entities;
public class Person : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}
