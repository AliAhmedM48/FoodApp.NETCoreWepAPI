using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Food.App.Core.Entities;
using Food.App.Core.ViewModels.Admin;
using Food.App.Core.ViewModels.Users;

namespace Food.App.Core.MappingProfiles
{
    internal class AdminProfile:Profile
    {
        public AdminProfile()
        {
            CreateMap<AdminCreateViewModel, Admin>();
        }
    }
}
