namespace Food.App.Core.ViewModels.Recipe;
public class ReciptDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public decimal Price { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
    public string CategoryName { get; set; }
}
