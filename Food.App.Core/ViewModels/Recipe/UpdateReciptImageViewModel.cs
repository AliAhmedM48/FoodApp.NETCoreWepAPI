using Microsoft.AspNetCore.Http;

namespace Food.App.Core.ViewModels.Recipe;
public class UpdateReciptImageViewModel
{
    public int Id { get; set; }
    public IFormFile ImageFile { get; set; }

}
