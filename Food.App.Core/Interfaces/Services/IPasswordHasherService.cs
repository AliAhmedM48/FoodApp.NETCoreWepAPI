using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.App.Core.Interfaces.Services
{
    public interface IPasswordHasherService
    {
        String HashPassord(string password);
        bool ValidatePassword(string password, string storedHash);
    }
}
