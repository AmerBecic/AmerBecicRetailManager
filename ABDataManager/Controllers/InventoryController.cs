using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ABDataManager.Library.DataAccess;
using ABDataManager.Library.Models;

namespace ABDataManager.Controllers
{
    //[Authorize]
    public class InventoryController : ApiController
    {
        [HttpGet]
        public List<InventoryModel> Get()
        {
            InventoryData data = new InventoryData();

            return data.GetInventory();
        }

        [HttpPost]
        public void Post(InventoryModel products)
        {
            InventoryData data = new InventoryData();

            data.SaveInventoryRecord(products);
        }
    }
}
