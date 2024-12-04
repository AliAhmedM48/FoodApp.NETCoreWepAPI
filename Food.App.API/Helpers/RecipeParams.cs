namespace Food.App.API.Helpers;

public class RecipeParams
{
    private const int MaxPageSize = 30;
    private int _pageSize = 10;
    public int PageNumber { get; set; }
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize)? MaxPageSize :value; 
    }
}
