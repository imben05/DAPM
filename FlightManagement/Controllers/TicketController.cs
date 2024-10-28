using FlightManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightManagement.Controllers
{
    public class TicketController : Controller
    {
        Db_HeThongBanVeMayBayEntities database = new Db_HeThongBanVeMayBayEntities();

        // GET: Aircrafts
        public ActionResult Index()
        {
            var tickAD = database.Tickets.ToList();
            return View(tickAD);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.PassengerID = new SelectList(database.Passengers, "passengerID", "firstName");
            ViewBag.FlightID = new SelectList(database.Flights, "flightID");// Assuming 'airlines' table has 'airlineID' and 'airlineName'
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "employeeID,firstName,lastName,dateOfBirth,address,email,phoneNumber,accountID,roleID")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                // Ensure ID is not set to any value
                ticket.ticketID = 0; // Not necessary if IDENTITY_INSERT is set to OFF

                database.Tickets.Add(ticket);
                database.SaveChanges();
                TempData["SuccessMessage"] = "Tạo thành công!";
                return RedirectToAction("Index");
            }
            ViewBag.PassengerID = new SelectList(database.Passengers, "passengerID", "firstName", ticket.passengerID); // Ensure correct ViewBag key
            ViewBag.FlightID = new SelectList(database.Flights, "flightID", "flightNumber", ticket.flightID); // Ensure correct ViewBag key
            return View(ticket);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Ticket ticket = database.Tickets.FirstOrDefault(p => p.ticketID == id);
            if (ticket != null)
            {
                ViewBag.PassengerID = new SelectList(database.Passengers, "passengerID", "firstName", ticket.passengerID);
                ViewBag.FlightID = new SelectList(database.Flights, "flightID", "flightNumber", ticket.flightID);// Ensure correct ViewBag key
                return View(ticket);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "paymentID,amount,paymentDate,paymentMethod,status,bookingID")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var tickDB = database.Tickets.FirstOrDefault(p => p.ticketID == ticket.ticketID);
                if (tickDB != null)
                {
                    tickDB.ticketNumber = tickDB.ticketNumber;
                    tickDB.seatNumber = tickDB.seatNumber;
                    tickDB.price = tickDB.price;
                    tickDB.status = tickDB.status;
                    tickDB.flightID = tickDB.flightID;
                    ticket.passengerID = ticket.passengerID;

                    database.SaveChanges();
                    TempData["SuccessMessage"] = "Chỉnh sửa thành công!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.PassengerID = new SelectList(database.Passengers, "passengerID", "firstName", ticket.passengerID);
            ViewBag.FlightID = new SelectList(database.Flights, "flightID", "flightNumber", ticket.flightID);// Ensure correct ViewBag key
            return View(ticket);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Ticket ticket = database.Tickets.FirstOrDefault(p => p.ticketID == id);
            if (ticket != null)
            {
                return View(ticket);
            }
            else return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            int i = int.Parse(id);
            var tickDB = database.Tickets.FirstOrDefault(p => p.ticketID == i);
            if (tickDB != null)
            {
                database.Tickets.Remove(tickDB);
                TempData["SuccessMessage"] = "Xóa thành công!";
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
