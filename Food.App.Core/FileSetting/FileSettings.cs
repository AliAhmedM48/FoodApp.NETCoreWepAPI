using Microsoft.AspNetCore.Hosting;

namespace Food.App.Core.FileSetting;
public class FileSettings
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    public FileSettings(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    public const string BaseImageUrl = "/Assets/Images/";
    public const string RecipeImageFolder = "Recipe";


    public const string AllowedExtensions = ".jpg,.png.,jpeg";
    public const int MaxFileSizeInMB = 5;
    public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
}
