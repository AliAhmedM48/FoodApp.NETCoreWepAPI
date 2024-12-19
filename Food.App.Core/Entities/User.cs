using Food.App.Core.Enums;

namespace Food.App.Core.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
}
