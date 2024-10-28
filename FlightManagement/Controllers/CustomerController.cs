using FlightManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FlightManagement.Controllers
{
    public class CustomerController : Controller
    {
        Db_HeThongBanVeMayBayEntities database = new Db_HeThongBanVeMayBayEntities();
        // GET: Customer
        public ActionResult Index()
        {
            var userAD = database.Customers.ToList();
            return View(userAD);
        }
        public ActionResult Create()
        {
            // Load available accounts to select from
            ViewBag.AccountID = new SelectList(database.Accounts, "accountID", "username");
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "accountID,firstname,lastname,dateOfBirth,address,email,phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Check if accountID exists in Accounts table
                var account = database.Accounts.Find(customer.accountID);
                if (account == null)
                {
                    ModelState.AddModelError("accountID", "Account ID không tồn tại.");
                    ViewBag.AccountID = new SelectList(database.Accounts, "accountID", "username", customer.accountID);
                    return View(customer);
                }

                database.Customers.Add(customer);
                database.SaveChanges();
                TempData["SuccessMessage"] = "Thêm người dùng thành công!";
                return RedirectToAction("Index");
            }

            ViewBag.AccountID = new SelectList(database.Accounts, "accountID", "username", customer.accountID);
            return View(customer);
        }


        // GET: Customers/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = database.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(database.Accounts, "accountID", "username", customer.accountID);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customerID, accountID, lastname, firstname, dateOfBirth, address, email, phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing customer from the database
                    var existingCustomer = database.Customers.Find(customer.customerID);

                    if (existingCustomer == null)
                    {
                        return HttpNotFound();
                    }

                    // Update the fields that are allowed to be modified
                    existingCustomer.accountID = customer.accountID;
                    existingCustomer.lastName = customer.lastName;
                    existingCustomer.firstName = customer.firstName;
                    existingCustomer.dateOfBirth = customer.dateOfBirth;
                    existingCustomer.address = customer.address;
                    existingCustomer.email = customer.email;
                    existingCustomer.phone = customer.phone;

                    // Optionally, if your Customer entity has a concurrency token (e.g., timestamp or row version), handle it like this:
                    // database.Entry(existingCustomer).OriginalValues["TimestampColumnName"] = customer.TimestampValue;

                    // Mark the entity as modified
                    database.Entry(existingCustomer).State = EntityState.Modified;

                    // Save changes
                    database.SaveChanges();

                    TempData["SuccessMessage"] = "Sửa người dùng thành công!";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // Log the exception or handle it in a way appropriate to your application
                    ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi cập nhật thông tin người dùng. Vui lòng thử lại.");
                    ViewBag.AccountID = new SelectList(database.Accounts, "accountID", "username", customer.accountID);
                    return View(customer);
                }
            }

            ViewBag.AccountID = new SelectList(database.Accounts, "accountID", "username", customer.accountID);
            return View(customer);
        }




        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = database.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = database.Customers.Find(id);
            database.Customers.Remove(customer);
            database.SaveChanges();
            TempData["SuccessMessage"] = "Xóa người dùng thành công!";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                database.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}