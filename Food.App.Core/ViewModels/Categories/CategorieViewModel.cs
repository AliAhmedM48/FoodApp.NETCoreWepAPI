using Food.App.Core.ViewModels.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.App.Core.ViewModels.Categories
{
    public class CategoryViewModelInclude
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<RecipeViewModel> Recipes { get; set; } = new List<RecipeViewModel>();
    }

    public class CategoryViewModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
