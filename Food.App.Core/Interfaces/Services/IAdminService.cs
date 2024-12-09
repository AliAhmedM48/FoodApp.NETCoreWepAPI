using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Food.App.Core.ViewModels.Admin;
using Food.App.Core.ViewModels.Response;
using Food.App.Core.ViewModels.Users;

namespace Food.App.Core.Interfaces.Services
{
    public interface IAdminService
    {
        Task<ResponseViewModel<int>> Create(AdminCreateViewModel viewModel);

    }
}
