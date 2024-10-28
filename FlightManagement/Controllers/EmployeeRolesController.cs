using FlightManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightManagement.Controllers
{
    public class EmployeeRolesController : Controller
    {
        Db_HeThongBanVeMayBayEntities database = new Db_HeThongBanVeMayBayEntities();

        // GET: Aircrafts
        public ActionResult Index()
        {
            var roleAD = database.EmployeeRoles.ToList();
            return View(roleAD);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "roleID,roleName,description")] EmployeeRole employeeRole)
        {
            if (ModelState.IsValid)
            {
                // Ensure ID is not set to any value
                employeeRole.roleID = 0; // Not necessary if IDENTITY_INSERT is set to OFF

                database.EmployeeRoles.Add(employeeRole);
                database.SaveChanges();
                TempData["SuccessMessage"] = "Tạo thành công!";
                return RedirectToAction("Index");
            }
            return View(employeeRole);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EmployeeRole employeeRole = database.EmployeeRoles.FirstOrDefault(p => p.roleID == id);
            if (employeeRole != null)
            {
                return View(employeeRole);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "roleID,roleName,description")] EmployeeRole employeeRole)
        {
            if (ModelState.IsValid)
            {
                var employDB = database.EmployeeRoles.FirstOrDefault(p => p.roleID == employeeRole.roleID);
                if (employDB != null)
                {
                    employDB.roleName = employeeRole.roleName;
                    employDB.description = employeeRole.description;

                    database.SaveChanges();
                    TempData["SuccessMessage"] = "Chỉnh sửa thành công!";
                    return RedirectToAction("Index");
                }
            }
            return View(employeeRole);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            EmployeeRole employeeRole = database.EmployeeRoles.FirstOrDefault(p => p.roleID == id);
            if (employeeRole != null)
            {
                return View(employeeRole);
            }
            else return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            int i = int.Parse(id);
            var employDB = database.EmployeeRoles.FirstOrDefault(p => p.roleID == i);
            if (employDB != null)
            {
                database.EmployeeRoles.Remove(employDB);
                TempData["SuccessMessage"] = "Xóa thành công!";
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
