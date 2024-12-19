using Food.App.Core.Extensions;
using Food.App.Core.FileSetting;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Food.App.Core.ViewModels.Recipe.Create;
public class CreateRecipeViewModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [AllowedExtensions(FileSettings.AllowedExtensions), MaxFileSize(FileSettings.MaxFileSizeInBytes)]
    public IFormFile ImageFile { get; set; }
    [Required]
    public int Price { get; set; }
    public int CategoryId { get; set; }
    public List<int> TagIds { get; set; } 

}
