﻿using System.Collections.Generic;
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

namespace housemanagement1.Controllers
{
    public class HomeController : BaseController
    {
        private bhousemanagementEntities2 db = new bhousemanagementEntities2();

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




        //[HttpPost]
        //public ActionResult Payment(Payment model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            // Set the PaymentDate to the current date and time
        //            model.PaymentDate = DateTime.Now;

        //            using (var db = new bhousemanagementEntities())
        //            {
        //                // Call the stored procedure to save payment
        //                db.Database.ExecuteSqlCommand("EXEC SavePayment1 @CardHolderName, @PaymentAmount, @ExpiryMonth",
        //                    new SqlParameter("@CardHolderName", model.CardHolderName),
        //                    new SqlParameter("@PaymentAmount", model.PaymentAmount),
        //                    new SqlParameter("@ExpiryMonth", model.ExpiryMonth)
        //                );
        //            }

        //            TempData["Msg"] = "Payment successfully saved!";
        //            return RedirectToAction("Dashboard");
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log the exception for debugging
        //            Console.WriteLine(ex.Message);
        //            ModelState.AddModelError("", "An error occurred while saving the payment.");
        //            return View(model);
        //        }
        //    }
        //    else
        //    {
        //        // If the model state is not valid, return the view with the model to display validation errors
        //        return View(model);
        //    }
        //}












        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMsg"] = "User ID is required.";
                return RedirectToAction("Index");
            }

            var user = _userRepo.Get(id.Value);

            if (user == null)
            {
                TempData["ErrorMsg"] = "User not found.";
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Edit(int id)
        {
            return View(_userRepo.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(CustomerAccount u)
        {
            _userRepo.Update(u.id, u);
            TempData["Msg"] = $"User {u.username} Updated!";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _userRepo.Delete(id);
            TempData["Msg"] = $"User Deleted!";

            return RedirectToAction("Index");
        }


    }
}
