
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using housemanagement1.Models;
using System;
using System.Web.Security;
using housemanagement1.Repository;
using System.Linq;
using housemanagement1.Contracts;
using System.Data.Entity;
using System.Web.Helpers;
using housemanagement1.Models;
using System.Net;
using System.Runtime.Remoting.Contexts;

namespace housemanagement1.Controllers
{
    public class HomeController : BaseController
    {
        private bhousemanagementEntities3 db = new bhousemanagementEntities3();

        private readonly BaseRepository1<Reservations> _reservationRepo;

        public HomeController()
        {
            _reservationRepo = new BaseRepository1<Reservations>();
        }

        public ActionResult Index()
        {
            List<CustomerAccount> userList = _userRepo.GetAll();
            return View(userList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CustomerAccount u)
        {
            _userRepo.Create(u);
            TempData["Msg"] = $"User {u.username} added!";
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(CustomerAccount u)
        {
            var user = _userRepo.Table().FirstOrDefault(m => m.username == u.username && m.password == u.password);
            var userInfo = _db.CustomerAccount.FirstOrDefault(m => m.username == u.username && m.password == u.password);
            var adminInfo = _db.AdminAccounts.FirstOrDefault(m => m.Username == u.username && m.Password == u.password);

            if (userInfo != null)
            {
                if (userInfo.RoleId == 1)
                {
                    return RedirectToAction("Dashboard", new { username = u.username });
                }
                ModelState.AddModelError("", "User does not exist or incorrect password!");
            }
            else if (adminInfo != null)
            {
                if (adminInfo.RoleId == 2)
                {
                    return RedirectToAction("AdminLogin", new { Username = u.username });
                }
                ModelState.AddModelError("", "Admin does not exist or incorrect password!");
            }
            else
            {
                ModelState.AddModelError("", "User does not exist or incorrect password!");
            }
            return View();
        }




        public ActionResult Dashboard(string username)
        {
            ViewBag.Username = username;
            return View();
        }
        public ActionResult AdminLogin()
        {
            var getAllReservation = _reservationRepo.GetAll();
            return View(getAllReservation);
        }
  





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reserve(Reservations reservation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    string username = reservation.UserName;


                    reservation.Status = "Pending";


                    db.Reservations.Add(reservation);
                    db.SaveChanges();

                    TempData["Msg"] = "Reservation successfully saved!";
                    return RedirectToAction("ReservationSuccess");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the reservation.");

                    return View(reservation);
                }
            }
            else
            {

                return View("Index", reservation);
            }
        }



        public ActionResult ReservationSuccess()
        {
            return View();
        }




        [HttpPost]
        public ActionResult savePayment(Payment3 payment)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    string username = payment.UserName;


                    //Payment.Status = "Pending";


                    db.Payment3.Add(payment);
                    db.SaveChanges();

                    TempData["Msg"] = "Payment successfully saved!";
                    return RedirectToAction("ReservationSuccess");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the reservation.");

                    return View(payment);
                }
            }
            else
            {

                return View("Index", payment);
            }
        }
        public ActionResult Payment(Payment3 payment)
        {
            return View();
        }












        public ActionResult Details()
        {
        

            return View();
        }



        public ActionResult Edit(Reservations reserve)
        {
 
            _db.UpdateReservationStatus(reserve.ReservationId, reserve.Status);


            return View();
        }




        public ActionResult Delete(int id)
        {
            var result = _reserveRepo.Delete(id);
            if (result == ErrorCode.Success)
            {
                TempData["Msg"] = $"Reservation with ID {id} Deleted!";
            }
            else
            {
                TempData["ErrorMsg"] = $"Failed to delete reservation with ID {id}.";
            }
            return RedirectToAction("AdminLogin");
        }

    }
}
