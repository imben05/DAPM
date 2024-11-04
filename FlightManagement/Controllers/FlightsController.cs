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
        DAPMEntities database = new DAPMEntities();
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
            ViewBag.AirlineID = new SelectList(database.Airlines, "airlineID", "airlineName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "flightID,flightNumber,departureCity,arrivalCity,departureTime,flightDuration,flightPrice,aircraftID, airlineID")] Flight flight)
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
             ViewBag.AirlineID = new SelectList(database.Airlines, "airlineID", "airlineName", flight.airlineID);
            return View(flight);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Flight flight = database.Flights.FirstOrDefault(p => p.flightID == id);
            if (flight != null)
            {
                ViewBag.AircraftID = new SelectList(database.Aircraft, "aircraftID", "model", flight.aircraftID);
                 ViewBag.AirlineID = new SelectList(database.Airlines, "airlineID", "airlineName", flight.airlineID);
                return View(flight);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "flightID,departureCity,arrivalCity,departureTime,flightDuration,flightPrice,aircraftID,airlineID")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                var flightDB = database.Flights.FirstOrDefault(p => p.flightID == flight.flightID);
                if (flightDB != null)
                {
                    flightDB.departureCity = flight.departureCity;
                    flightDB.arrivalCity = flight.arrivalCity;
                    flightDB.departureTime = flight.departureTime;
                    flightDB.flightDuration = flight.flightDuration;
                    flightDB.flightPrice = flight.flightPrice;
                    flightDB.aircraftID = flight.aircraftID;
                    flightDB.airlineID = flight.airlineID;

                    database.SaveChanges();
                    TempData["SuccessMessage"] = "Chỉnh sửa thành công!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.AircraftID = new SelectList(database.Aircraft, "aircraftID", "model", flight.aircraftID);
             ViewBag.AirlineID = new SelectList(database.Airlines, "airlineID", "airlineName", flight.airlineID);
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
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string departureCity, string arrivalCity, DateTime? departureTime)
        {
            // Lấy danh sách chuyến bay từ cơ sở dữ liệu
            var flightList = database.Flights.AsQueryable(); // AsQueryable() cho phép linh hoạt với các truy vấn

            // Lọc theo thành phố khởi hành
            if (!string.IsNullOrEmpty(departureCity))
            {
                flightList = flightList.Where(x => x.departureCity == departureCity);
            }

            // Lọc theo thành phố đến
            if (!string.IsNullOrEmpty(arrivalCity))
            {
                flightList = flightList.Where(x => x.arrivalCity == arrivalCity);
            }

            // Lọc theo thời gian khởi hành
            if (departureTime.HasValue)
            {
                flightList = flightList.Where(x => x.departureTime.Date == departureTime.Value.Date);
            }

            return View("Search", flightList.ToList());
        }
    }
}