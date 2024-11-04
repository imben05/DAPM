using FlightManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightManagement.Controllers
{
    public class AirlinesController : Controller
    {
        DAPMEntities database = new DAPMEntities();
        // GET: Airlines
        public ActionResult Index()
        {
            var airAD = database.Airlines.ToList();
            return View(airAD);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "airlineID,airlineName,code")] Airline airline)
        {
            if (ModelState.IsValid)
            {
                // Ensure ID is not set to any value
                airline.airlineID = 0; // Not necessary if IDENTITY_INSERT is set to OFF

                database.Airlines.Add(airline);
                database.SaveChanges();
                TempData["SuccessMessage"] = "Tạo thành công!";
                return RedirectToAction("Index");
            }
            return View(airline);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Airline airline = database.Airlines.FirstOrDefault(p => p.airlineID == id);
            if (airline != null)
            {
                return View(airline);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "airlineID,airlineName,code")] Airline airline)
        {
            if (ModelState.IsValid)
            {
                var airlineDB = database.Airlines.FirstOrDefault(p => p.airlineID == airline.airlineID);
                if (airlineDB != null)
                {
                    airlineDB.airlineName = airline.airlineName;
                    airlineDB.code = airline.code;

                    database.SaveChanges();
                    TempData["SuccessMessage"] = "Chỉnh sửa thành công!";
                    return RedirectToAction("Index");
                }
            }
            return View(airline);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            Airline airline = database.Airlines.FirstOrDefault(p => p.airlineID == id);
            if (airline != null)
                return View(airline);
            else return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            int i = int.Parse(id);
            var airlineDB = database.Airlines.FirstOrDefault(p => p.airlineID == i);
            if (airlineDB != null)
            {
                database.Airlines.Remove(airlineDB);
                TempData["SuccessMessage"] = "Xóa thành công!";
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}