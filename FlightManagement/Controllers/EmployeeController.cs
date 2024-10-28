using FlightManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightManagement.Controllers
{
    public class EmployeeController : Controller
    {
        Db_HeThongBanVeMayBayEntities database = new Db_HeThongBanVeMayBayEntities();

        // GET: Aircrafts
        public ActionResult Index()
        {
            var employAD = database.Employees.ToList();
            return View(employAD);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(database.Accounts, "accountID", "username");
            ViewBag.EmployRole = new SelectList(database.EmployeeRoles, "roleID", "roleName"); // Assuming 'airlines' table has 'airlineID' and 'airlineName'
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "employeeID,firstName,lastName,dateOfBirth,address,email,phoneNumber,accountID,roleID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Ensure ID is not set to any value
                employee.employeeID = 0; // Not necessary if IDENTITY_INSERT is set to OFF

                database.Employees.Add(employee);
                database.SaveChanges();
                TempData["SuccessMessage"] = "Tạo thành công!";
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(database.Accounts, "accountID", "username", employee.accountID);
            ViewBag.EmployRole = new SelectList(database.EmployeeRoles, "roleID", "roleName", employee.roleID); // Ensure correct ViewBag key
            return View(employee);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Employee employee = database.Employees.FirstOrDefault(p => p.employeeID == id);
            if (employee != null)
            {
                ViewBag.AccountID = new SelectList(database.Accounts, "accountID", "username", employee.accountID);
                ViewBag.EmployRole = new SelectList(database.EmployeeRoles, "roleID", "roleName", employee.roleID); // Ensure correct ViewBag key
                return View(employee);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "employeeID,firstName,lastName,dateOfBirth,address,email,phoneNumber,accountID,roleID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var employDB = database.Employees.FirstOrDefault(p => p.employeeID == employee.employeeID);
                if (employDB != null)
                {
                    employDB.firstName = employee.firstName;
                    employDB.lastName = employee.lastName;
                    employDB.dateOfBirth = employee.dateOfBirth;
                    employDB.address = employee.address;
                    employDB.email = employee.email;
                    employDB.phoneNumber = employee.phoneNumber;
                    employee.employeeID = employDB.employeeID;
                    employee.roleID = employDB.roleID;

                    database.SaveChanges();
                    TempData["SuccessMessage"] = "Chỉnh sửa thành công!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.AccountID = new SelectList(database.Accounts, "accountID", "username", employee.accountID);
            ViewBag.EmployRole = new SelectList(database.EmployeeRoles, "roleID", "roleName", employee.roleID); // Ensure correct ViewBag key
            return View(employee);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Employee employee = database.Employees.FirstOrDefault(p => p.employeeID == id);
            if (employee != null)
            {
                return View(employee);
            }
            else return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            int i = int.Parse(id);
            var employDB = database.Employees.FirstOrDefault(p => p.employeeID == i);
            if (employDB != null)
            {
                database.Employees.Remove(employDB);
                TempData["SuccessMessage"] = "Xóa thành công!";
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
