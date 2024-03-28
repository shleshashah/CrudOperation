using CrudOperation.Interface;
using CrudOperation.Models;
using CrudOperation.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudOperation.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomer _Customer;
        private readonly EmailServices _emailServices;
        public CustomersController(ICustomer customers, EmailServices emailServices)
        {
            _Customer = customers;
            _emailServices = emailServices;
        }
        public async Task<IActionResult> Index()
        {
            List<Customer> vCustomer = new List<Customer>();
            try
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                vCustomer = await _Customer.getall();

            }
            catch (Exception ex)
            {
                vCustomer = null;
            }
            return View(vCustomer);
        }

        public async Task<IActionResult> AddEdit(int id = 0, string Pagename = null)
        {
            Customer vCustomer = new Customer();
            try
            {
                TempData["PageName"] = Pagename;
                if (id > 0)
                    vCustomer = await _Customer.get(id);

            }
            catch (Exception ex)
            {
                vCustomer = null;
            }
            return View(vCustomer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (customer.Id > 0)
                        await _Customer.Edit(customer);
                    else
                        await _Customer.Add(customer);
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(customer);
            }
            TempData["SuccessMessage"] = ErrrorMessageEnum.SavedSuccessfully;
            if (Convert.ToString(TempData["PageName"]) == "Login")
            {
                _emailServices.SendEmail(customer.Email, "Changed Password Successfully", "You Password has been changed Successfully");
                return RedirectToAction("Index", "Account");
            }
            else
                return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _Customer.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
