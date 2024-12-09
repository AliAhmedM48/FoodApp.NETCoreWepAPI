using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Food.App.Core.Entities;
using Food.App.Core.Enums;
using Food.App.Core.Interfaces;
using Food.App.Core.Interfaces.Services;
using Food.App.Core.MappingProfiles;
using Food.App.Core.ViewModels.Admin;
using Food.App.Core.ViewModels.Response;
using Food.App.Repository;

namespace Food.App.Service.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Admin> _adminRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasherService _passwordHasherService;
        public AdminService(IUnitOfWork unitOfWork, IPasswordHasherService passwordHasherService)
        {
            _unitOfWork = unitOfWork;
            _adminRepository = unitOfWork.GetRepository<Admin>();
            _passwordHasherService = passwordHasherService;
        }

        public async Task<ResponseViewModel<int>> Create(AdminCreateViewModel viewModel)
        {
            var isRepatedUserName = _adminRepository.GetAll(e => e.Username == viewModel.Username).Any();
            if (isRepatedUserName)
            {
                return new FailureResponseViewModel<int>(ErrorCode.UserNameExist);
            }

            var isRepatedEmail = _adminRepository.GetAll(e => e.Email == viewModel.Email).Any();
            if (isRepatedEmail)
            {
                return new FailureResponseViewModel<int>(ErrorCode.UserEmailExist);
            }

            var admin = viewModel.Map<Admin>();
            admin.Password = _passwordHasherService.HashPassord(admin.Password);
            admin.Role = Role.Admin;
            
            await _adminRepository.AddAsync(admin);
            var isSaved = await _unitOfWork.SaveChangesAsync();
            if (isSaved == 0)
            {
                return new FailureResponseViewModel<int>(ErrorCode.DataBaseError);
            }
            return new SuccessResponseViewModel<int>(SuccessCode.UserCreated);



        }
    }
}
