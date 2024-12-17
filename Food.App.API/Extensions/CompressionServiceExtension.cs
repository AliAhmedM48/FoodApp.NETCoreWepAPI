using Microsoft.AspNetCore.ResponseCompression;

namespace Food.App.API.Extensions;

public static class CompressionServiceExtension
{
    public static IServiceCollection AddCompressionServices(this IServiceCollection services)
    {

        services.AddResponseCompression(opt =>
        {
            opt.EnableForHttps = true;
            opt.Providers.Add<GzipCompressionProvider>();
            opt.Providers.Add<BrotliCompressionProvider>();

        });
        services.Configure<BrotliCompressionProviderOptions>(opts =>
        {
            opts.Level = System.IO.Compression.CompressionLevel.Optimal;
        });
        services.Configure<GzipCompressionProviderOptions>(opts =>
        {
            opts.Level = System.IO.Compression.CompressionLevel.Fastest;
        });
        return services;
    }
}
