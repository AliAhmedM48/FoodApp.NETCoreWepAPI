namespace Food.App.Core.Entities;
public class RecipeTag :BaseEntity
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }

    public int TagId { get; set; }
    public Tag Tag { get; set; }

}
