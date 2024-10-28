using FlightManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightManagement.Controllers
{
    public class BookingController : Controller
    {
        Db_HeThongBanVeMayBayEntities database = new Db_HeThongBanVeMayBayEntities();

        // GET: Aircrafts
        public ActionResult Index()
        {
            var bookAD = database.Bookings.ToList();
            return View(bookAD);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(database.Customers, "customerID", "firstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bookingID,bookingDate,totalAmount,status,customerID")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                // Ensure ID is not set to any value
                booking.bookingID = 0; // Not necessary if IDENTITY_INSERT is set to OFF

                database.Bookings.Add(booking);
                database.SaveChanges();
                TempData["SuccessMessage"] = "Tạo thành công!";
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(database.Customers, "customerID", "firstName", booking.Customer); // Ensure correct ViewBag key
            return View(booking);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Booking booking = database.Bookings.FirstOrDefault(p => p.bookingID == id);
            if (booking != null)
            {
                ViewBag.CustomerID = new SelectList(database.Customers, "customerID", "firstName", booking.Customer); // Ensure correct ViewBag key
                return View(booking);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookingID,bookingDate,totalAmount,status,customerID")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                var bookingDB = database.Bookings.FirstOrDefault(p => p.bookingID == booking.bookingID);
                if (bookingDB != null)
                {
                    bookingDB.bookingDate = bookingDB.bookingDate;
                    bookingDB.totalAmount = bookingDB.totalAmount;
                    bookingDB.status = bookingDB.status;

                    database.SaveChanges();
                    TempData["SuccessMessage"] = "Chỉnh sửa thành công!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CustomerID = new SelectList(database.Customers, "customerID", "firstName", booking.Customer); // Ensure correct ViewBag key
            return View(booking);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Booking booking = database.Bookings.FirstOrDefault(p => p.bookingID == id);
            if (booking != null)
            {
                return View(booking);
            }
            else return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            int i = int.Parse(id);
            var bookingDB = database.Bookings.FirstOrDefault(p => p.bookingID == i);
            if (bookingDB != null)
            {
                database.Bookings.Remove(bookingDB);
                TempData["SuccessMessage"] = "Xóa thành công!";
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
