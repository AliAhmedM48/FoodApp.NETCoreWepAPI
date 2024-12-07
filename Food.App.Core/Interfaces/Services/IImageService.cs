using Microsoft.AspNetCore.Http;

namespace Food.App.Core.Interfaces.Services;
public interface IImageService
{
    Task<string> UploadImage(IFormFile imageFile, string folderName);
    void DeleteOlderImage(string imageUrl, string folderName);
}
