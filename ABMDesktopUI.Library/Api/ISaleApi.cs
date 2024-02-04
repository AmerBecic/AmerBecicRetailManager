using System.Threading.Tasks;
using ABMDesktopUI.Library.Models;

namespace ABMDesktopUI.Library.Api
{
    public interface ISaleApi
    {
        Task PostSale(SaleModel sale);
    }
}