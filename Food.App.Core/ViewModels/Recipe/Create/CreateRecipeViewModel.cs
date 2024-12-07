using Food.App.Core.Extensions;
using Food.App.Core.FileSetting;
using Microsoft.AspNetCore.Http;

namespace Food.App.Core.ViewModels.Recipe.Create;
public class CreateRecipeViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    [AllowedExtensions(FileSettings.AllowedExtensions), MaxFileSize(FileSettings.MaxFileSizeInBytes)]
    public IFormFile ImageFile { get; set; }
    public int CategoryId { get; set; }
    public List<int> TagIds { get; set; }

}
