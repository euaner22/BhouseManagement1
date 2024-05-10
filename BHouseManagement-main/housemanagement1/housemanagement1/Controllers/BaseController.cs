using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using housemanagement1.Repository;

namespace housemanagement1.Controllers
{
    public class BaseController : Controller
    {
        public bhousemanagementEntities2 _db;
        public BaseRepository1<CustomerAccount> _userRepo;

        public BaseController()
        {
            _db = new bhousemanagementEntities2();
            _userRepo = new BaseRepository1<CustomerAccount>();   
        }

    }
}