namespace Food.App.Core.ViewModels.Recipe.Create;
public class UpdateRecipeViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public int CategoryId { get; set; }
    public List<int> TagIds { get; set; }

}
