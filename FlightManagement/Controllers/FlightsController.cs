using FlightManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightManagement.Controllers
{
    public class FlightsController : Controller
    {
        Db_HeThongBanVeMayBayEntities database = new Db_HeThongBanVeMayBayEntities();
        // GET: Flights
        public ActionResult Index()
        {
            var flyAD = database.Flights.ToList();
            return View(flyAD);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.AircraftID = new SelectList(database.Aircraft, "aircraftID", "model");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "flightID,flightNumber,departureCity,arrivalCity,departureTime,arrivalTime,aircraftID")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                // Ensure ID is not set to any value
                flight.flightID = 0; // Not necessary if IDENTITY_INSERT is set to OFF

                database.Flights.Add(flight);
                database.SaveChanges();
                TempData["SuccessMessage"] = "Tạo thành công!";
                return RedirectToAction("Index");
            }
            ViewBag.AircraftID = new SelectList(database.Aircraft, "aircraftID", "model", flight.aircraftID);
            return View(flight);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Flight flight = database.Flights.FirstOrDefault(p => p.flightID == id);
            if (flight != null)
            {
                ViewBag.AircraftID = new SelectList(database.Aircraft, "aircraftID", "model", flight.aircraftID);
                return View(flight);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "flightID,flightNumber,departureCity,arrivalCity,departureTime,arrivalTime,aircraftID")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                var flightDB = database.Flights.FirstOrDefault(p => p.flightID == flight.flightID);
                if (flightDB != null)
                {
                    flightDB.flightNumber = flight.flightNumber;
                    flightDB.departureCity = flight.departureCity;
                    flightDB.arrivalCity = flight.arrivalCity;
                    flightDB.departureTime = flight.departureTime;
                    flightDB.arrivalTime = flight.arrivalTime;
                    flightDB.aircraftID = flight.aircraftID;

                    database.SaveChanges();
                    TempData["SuccessMessage"] = "Chỉnh sửa thành công!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.AircraftID = new SelectList(database.Aircraft, "aircraftID", "model", flight.aircraftID);
            return View(flight);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            Flight flight = database.Flights.FirstOrDefault(p => p.flightID == id);
            if (flight != null)
                return View(flight);
            else return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            int i = int.Parse(id);
            var FlightDB = database.Flights.FirstOrDefault(p => p.flightID == i);
            if (FlightDB != null)
            {
                database.Flights.Remove(FlightDB);
                TempData["SuccessMessage"] = "Xóa thành công!";
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}