using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Food.App.Core.ViewModels.Recipe;

namespace Food.App.Core.ViewModels.Tags
{
    public class TagDetailsViewModel
    {
        public string Name { get; set; }
        public IEnumerable<String> Recipes { get; set; } = new List<String>();

    }
}
