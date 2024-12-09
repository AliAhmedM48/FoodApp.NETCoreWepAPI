using Food.App.Core.Extensions;
using Food.App.Core.FileSetting;
using Microsoft.AspNetCore.Http;

namespace Food.App.Core.ViewModels.Recipe;
public class UpdateReciptImageViewModel
{
    public int Id { get; set; }
    [AllowedExtensions(FileSettings.AllowedExtensions), MaxFileSize(FileSettings.MaxFileSizeInBytes)]
    public IFormFile ImageFile { get; set; }

}
