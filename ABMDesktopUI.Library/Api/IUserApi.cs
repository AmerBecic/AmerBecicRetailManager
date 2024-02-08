using System.Collections.Generic;
using System.Threading.Tasks;
using ABMDesktopUI.Library.Models;

namespace ABMDesktopUI.Library.Api
{
    public interface IUserApi
    {
        Task<List<ApplicationUserModel>> GetAll();
    }
}