namespace Food.App.Core.Entities;
public class Tag : BaseEntity
{
    public string Name { get; set; }
    public ICollection<RecipeTag> Tags { get; set; } = new List<RecipeTag>();

}
