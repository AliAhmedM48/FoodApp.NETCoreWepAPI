using Food.App.Core.Helpers;
using Food.App.Repository.Helpers;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Food.App.Core.Extensions;

public static class HttpExtensions
{
    public static void AddPaginationHeader<T>(this HttpResponse response, PageList<T> data)
    {
        var paginationHeader = new PaginationHeader(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);
        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationHeader, jsonOptions));
        response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
    }
}
