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
        public bhousemanagementEntities3 _db;
        public BaseRepository1<CustomerAccount> _userRepo;
        public BaseRepository1<Reservations> _reserveRepo;
        public BaseRepository1<Payment3> _paymentRepo;

        public BaseController()
        {
            _db = new bhousemanagementEntities3();
            _userRepo = new BaseRepository1<CustomerAccount>();
            _reserveRepo = new BaseRepository1<Reservations>();
            _paymentRepo = new BaseRepository1<Payment3>();
        }

    }
}