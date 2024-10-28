using FlightManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightManagement.Controllers
{
    public class PassengerController : Controller
    {
        Db_HeThongBanVeMayBayEntities database = new Db_HeThongBanVeMayBayEntities();

        // GET: Aircrafts
        public ActionResult Index()
        {
            var passAD = database.Passengers.ToList();
            return View(passAD);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "employeeID,firstName,lastName,dateOfBirth,address,email,phoneNumber,Account.fullname,EmployeeRole.roleName")] Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                // Ensure ID is not set to any value
                passenger.passengerID = 0; // Not necessary if IDENTITY_INSERT is set to OFF

                database.Passengers.Add(passenger);
                database.SaveChanges();
                TempData["SuccessMessage"] = "Tạo thành công!";
                return RedirectToAction("Index");
            }
            return View(passenger);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Passenger passenger = database.Passengers.FirstOrDefault(p => p.passengerID == id);
            if (passenger != null)
            {
                return View(passenger);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookingID,bookingDate,totalAmount,status,customerID")] Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                var passDB = database.Passengers.FirstOrDefault(p => p.passengerID == passenger.passengerID);
                if (passDB != null)
                {
                    passDB.firstName = passDB.firstName;
                    passDB.lastName = passDB.lastName;
                    passDB.dateOfBirth = passDB.dateOfBirth;
                    passDB.email = passDB.email;
                    passDB.phone = passDB.phone;


                    database.SaveChanges();
                    TempData["SuccessMessage"] = "Chỉnh sửa thành công!";
                    return RedirectToAction("Index");
                }
            }
            return View(passenger);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Passenger passenger = database.Passengers.FirstOrDefault(p => p.passengerID == id);
            if (passenger != null)
            {
                return View(passenger);
            }
            else return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            int i = int.Parse(id);
            var passDB = database.Passengers.FirstOrDefault(p => p.passengerID == i);
            if (passDB != null)
            {
                database.Passengers.Remove(passDB);
                TempData["SuccessMessage"] = "Xóa thành công!";
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
