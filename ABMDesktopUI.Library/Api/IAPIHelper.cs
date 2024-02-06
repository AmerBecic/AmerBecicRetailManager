using System.Net.Http;
using System.Threading.Tasks;
using ABMDesktopUI.Library.Models;

namespace ABMDesktopUI.Library.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        void LogOffUser();
        Task GetLoggedInUserInfo(string token);

        HttpClient ApiClient { get; }
    }
}