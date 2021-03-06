using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelData;
using HotelData.Entity;
using Hotel_Reservations_Manager.Services;
using Hotel_Reservations_Manager.Exeptions;

namespace Hotel_Reservations_Manager.Controllers
{
    public class UsersController : Controller
    {
      //  [TempData]
        public string username;
        private readonly HotelContext _context;
        
        public UsersController(HotelContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            if (int.Parse(TempData["userId"].ToString()) != int.Parse(Properties.Resources.AdminId))
                return RedirectToAction(nameof(Index));
            TempData["userId"] = TempData["userId"].ToString();
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,FirstName,SecondName,LastName,Egn,PhoneNumber,Email,IsActive,HireDate,LeaveDate")] User user)
        {
            if (ModelState.IsValid)
            {
               
                user.IsActive = true;
                user.HireDate = DateTime.Now;
                try
                {
                    SecurityChecker.CheckUser(user);
                }
                catch (InvalidEgnException)
                {
                    ViewData["message"] = "Egn is invalid";
                    user.Egn = null;
                    return View(user);
                }
                catch (Exception)
                {
                    return View(user);
                }
                try
                {
                    AvailabilityChecker.CheckUserAvailabikity(user, _context);
                }
                catch (UsernameAlreadyExistsException)
                {
                    ViewData["message"] = "Username is taken";
                    user.Username = null;
                    return View(user);
                }
                catch (EmailAlreadyExistsException)
                {
                    ViewData["message"] = "Email is already registered";
                    user.Email = null;
                    return View(user);
                }
                catch (EgnAlreadyExistsException)
                {
                    ViewData["message"] = "EGN is already registered";
                    user.Egn = null;
                    return View(user);
                }
                catch (PhoneAlreadyExistsException)
                {
                    ViewData["message"] = "Phone is already registered";
                    user.PhoneNumber = null;
                    return View(user);
                }
                catch (Exception)
                {
                    return View(user);
                }
                user.Password = Hasher.GetHash(user.Password);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
   
            if (int.Parse(TempData["userId"].ToString()) != int.Parse(Properties.Resources.AdminId))
                return RedirectToAction(nameof(Index));
            TempData["userId"] = TempData["userId"].ToString();
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,FirstName,SecondName,LastName,Egn,PhoneNumber,Email,IsActive,HireDate,LeaveDate")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (int.Parse(TempData["userId"].ToString()) != int.Parse(Properties.Resources.AdminId))
                return RedirectToAction(nameof(Index));
            TempData["userId"] = TempData["userId"].ToString();
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        // GET: Users/Login
        public IActionResult LogIn()
        {
            AvailabilityHandler.GetHandled(_context);
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn([Bind("Id,Username,Password,FirstName,SecondName,LastName,Egn,PhoneNumber,Email,IsActive,HireDate,LeaveDate")] User user)
        {
            //if (CurrentUser.IsLogged == true)
           // {
           //     return View(user);
           // }
            var users =  (from u in _context.Users where u.Username == user.Username select u).ToList();
            if (users.Count == 0)
            {
                return RedirectToAction(nameof(Index));
            }
            try
                {
                    Hasher.CheckPass(users[0], user.Password);
                }
                catch (Exeptions.IncorrectPassExeption)
                {
                    user.Password = null;
                    return View(user);
                }
            if (users[0].IsActive == false)
            {
                user.Username = null;
                user.Password = null;
                return View(user);
            }
            //string username = user.Username;
            username = user.Username;
            TempData["Username"] = user.Username;
            HttpContext.Items.Add("username",user.Username);
            TempData["userId"] = users[0].Id;
            HttpContext.Items.Add("userId", users[0].Id);
            string usrname = HttpContext.Items["username"].ToString();
            //HttpContext.Session.Set(Username, user.Username);
            //CurrentUser.SetCurrentUser(users[0].Id,users[0].Username);
                return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Fire(int? id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            user.IsActive = false;
            user.LeaveDate = DateTime.Now;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Logout()
        {
            TempData["userId"] = null;
            TempData["Username"] = null;
            return RedirectToAction(nameof(LogIn));
        }
    }
}

