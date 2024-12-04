namespace Food.App.Core.Entities;
public class Category :BaseEntity
{
    public string Name { get; set; }
    public List<Recipe> Recipes { get; set; } = new List<Recipe>();
}
