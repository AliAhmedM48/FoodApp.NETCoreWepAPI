using Food.App.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace Food.App.API.Middlewares;

public class TransactionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<TransactionMiddleware> _logger;

    public TransactionMiddleware(RequestDelegate next, AppDbContext appDbContext, ILogger<TransactionMiddleware> logger)
    {
        _next = next;
        _appDbContext = appDbContext;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContent)
    {
        IDbContextTransaction transaction = null;
        try
        {
            transaction = await _appDbContext.Database.BeginTransactionAsync();
            await _next.Invoke(httpContent);
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            if (transaction is not null)
                await transaction.RollbackAsync();

            _logger.LogError(ex.Message, "An error occurred while processing the transaction.");
            throw;
        }
    }
}
