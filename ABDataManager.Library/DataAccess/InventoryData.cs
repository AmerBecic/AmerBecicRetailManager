using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABDataManager.Library.Internal.DataAccess;
using ABDataManager.Library.Models;

namespace ABDataManager.Library.DataAccess
{
    public class InventoryData
    {
        public List<InventoryModel> GetInventory()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<InventoryModel, dynamic>("dbo.spInventory_GetAll", new { }, "ABMData");

            return output;
        }

        public void SaveInventoryRecord(InventoryModel products)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData<InventoryModel>("dbo.spInventory_Insert", products, "ABMData");
        }
    }
}
