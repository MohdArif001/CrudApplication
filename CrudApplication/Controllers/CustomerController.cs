using CrudApplication.Models;
using CrudApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApplication.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CustomerController(ApplicationDbContext context)
        {
            dbContext = context;
        }


        //This code binding the customer data in grid
        public IActionResult Index()
        {
            //List<Customer> customers = dbContext.Customers.ToList();
            //return View(customers);

            List<CustomerViewModel> customers = (from c in dbContext.Customers
                                        join s in dbContext.States on c.StateId equals s.StateId
                                        join city in dbContext.Cities on c.CityId equals city.CityId 
                                        select new CustomerViewModel()
                                        {
                                            Id=c.Id,
                                            Name = c.Name,
                                            Email = c.Email,
                                            Mobile_No = c.Mobile_No,
                                            Address = c.Address,
                                            State = s.StateName,
                                            City = city.CityName
                                        }).ToList();
            return View(customers);
        }

       
        public IActionResult Create()
        {
            
            return View();
        }

        //This code is to create customer and save into database
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {

                dbContext.Add(customer);
                dbContext.SaveChanges();
                return RedirectToAction("Index", "Customer");
            }
            else
            {
                return View(customer);
            }
        }

        //This code select a single customer in database and show in  create customer form to update customer
        public IActionResult Update(int id)
        {
            var customer = dbContext.Customers.SingleOrDefault(e => e.Id == id);
            return View(customer);
        }

        //This code updates the customer in database and save it
        [HttpPost]
        public IActionResult Update(Customer customer)
        {
            dbContext.Update(customer);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        //This code select a single customer in database by id and remove it
        public IActionResult Delete(int id)
        {
            var customer = dbContext.Customers.SingleOrDefault(e => e.Id == id);
            dbContext.Remove(customer);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetCity(int id)
        {
            var cities = dbContext.Cities.Where(e => e.StateId == id).ToList();
            return Json(cities);
        }


        public JsonResult GetStates()
        {
            var states = dbContext.States.ToList();
            return Json(states);
        }
    }
}
