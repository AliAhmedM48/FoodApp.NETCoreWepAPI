using Food.App.Core.Entities;

namespace Food.App.Core.ViewModels;
public class UserViewModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
}

public class UserDetailsViewModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    IEnumerable<Recipe> favoriteRecipes { get; set; }
}