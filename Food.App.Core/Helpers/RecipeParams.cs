namespace Food.App.Core.Helpers;

public class RecipeParams
{
    private const int MaxPageSize = 30;
    private int _pageSize = 5;
    private int _pageNumber = 1;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = (value < 1) ? 1 : value;
    }
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public int CategoryId { get; set; }
    public int TagId { get; set; }
    public int RecipePrice { get; set; }

    public string? RecipeName { get; set; }
}
