using CrudApplication.Models;
using CrudApplication.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CrudApplication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AccountController(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult SignUp()
        {
            return View();
        }



        [HttpPost]
        public IActionResult SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Users()
                {

                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.ConfirmPassword,
                    Gender = model.Gender
                };

                try
                {
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
            else
            {
                return View();
            }

        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignInAsync(SignInModel model)
        {

            var user = dbContext.Users.SingleOrDefault(e => e.Email == model.Email && e.Password == model.Password);
            if (user != null)
            {
                //A claim is a statement about a subject by an issuer and    
                //represent attributes of the subject that are useful in the context of authentication and authorization operations.    
                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Name, model.Email),
                };
                //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                var principal = new ClaimsPrincipal(identity);
                //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                {
                    IsPersistent = model.RememberMe
                });
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
