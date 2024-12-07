using Microsoft.AspNetCore.Http;

namespace Food.App.Core.ViewModels.Recipe.Create;
public class CreateRecipeViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile ImageFile { get; set; }
    public int CategoryId { get; set; }
    public List<int> TagIds { get; set; }

}
