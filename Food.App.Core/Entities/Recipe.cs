namespace Food.App.Core.Entities;
public class Recipe : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public decimal Price { get; set; }
    public ICollection<RecipeTag> RecipeTags { get; set; } = new List<RecipeTag>();
    public int CategoryId { get; set; }
    public Category Category { get; set; }

}
