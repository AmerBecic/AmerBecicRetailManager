using System.Threading.Tasks;
using ABDataManager.Models;

namespace ABMDesktopUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}