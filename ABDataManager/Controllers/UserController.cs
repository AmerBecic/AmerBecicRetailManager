using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ABDataManager.Library.DataAccess;
using ABDataManager.Library.Models;
using Microsoft.AspNet.Identity;

namespace ABDataManager.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        public List<UserModel> GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();

           return data.GetUserById(userId);

        }
    }
}