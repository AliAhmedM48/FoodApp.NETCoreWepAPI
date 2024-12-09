using Food.App.Core.FileSetting;
using Food.App.Core.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Food.App.Service.ImageService;
public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _environment;
    private readonly string _imagesPath;

    public ImageService(IWebHostEnvironment environment)
    {
        _environment = environment;
        _imagesPath = $"{_environment.WebRootPath}{FileSettings.ImagePath}";

    }
    public async Task<string> UploadImage(IFormFile imageFile, string folderName)
    {

        var imageUrl = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
        string imageFullPath = $"{_imagesPath}{folderName}";
        var path = Path.Combine(imageFullPath, imageUrl);
        using (var stram = new FileStream(path, FileMode.Create))
        {
            await imageFile.CopyToAsync(stram);
        }
        return imageUrl;
    }
    public void DeleteOlderImage(string imageUrl, string folderName)
    {
        var oldPath = Path.Combine(_imagesPath, folderName);
        if (!string.IsNullOrEmpty(imageUrl))
        {
            var path = Path.Combine(oldPath, imageUrl);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
