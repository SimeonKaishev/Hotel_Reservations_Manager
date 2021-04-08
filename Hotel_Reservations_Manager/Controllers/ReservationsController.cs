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

namespace Hotel_Reservations_Manager.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly HotelContext _context;

        public ReservationsController(HotelContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservations.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,IsBreakfastIncluded,IsAllInclusive,Price")] Reservation reservation)
        {
            //reservation.Price = 1;
           // if (ModelState.IsValid)
          //  {
                try
                {
                    SecurityChecker.CheckDate(reservation.StartDate, reservation.EndDate);
                }
                catch (Exception)
                {
                    return View(reservation);
                }
            HttpContext.Items.Add("startdate", reservation.StartDate);
            //reservation.Reserver = CurrentUser.GetCurrent(_context);
            HttpContext.Items.Add("enddate", reservation.EndDate);
            HttpContext.Items.Add("IsAllInclusive", reservation.IsAllInclusive);
            CurrentReservation.SetResFirst(reservation);
            HttpContext.Items.Add("breakfast", reservation.IsBreakfastIncluded);
                //_context.Add(reservation);
               // await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ShowRooms));
         //   }
           // return View(reservation);
        }
        public async Task<IActionResult> ConfirmRoom(int id)
        {
            if (id == 0)
                return RedirectToAction(nameof(ShowRooms));
            else
            {
                var rooms = (from r in _context.Rooms where r.Id == id select r).ToList();
                if (rooms.Count > 0)
                {
                    HttpContext.Items.Add("roomId", rooms[0].Id);
                    List<Client> clients = new List<Client>();
                    HttpContext.Items.Add("clientIds",clients);
                    CurrentReservation.Room = rooms[0];//
                    CurrentReservation.Clients = new List<Client>();//
                    return RedirectToAction(nameof(ShowClients));
                }
                return RedirectToAction(nameof(ShowRooms));
            }
        }
        public async Task<IActionResult> ShowRooms()
        {
            return View(await _context.Rooms.ToListAsync());
        }
        public async Task<IActionResult> ShowClients()
        {
            return View(await _context.Clients.ToListAsync());
        }
        public async Task<IActionResult> ConfirmClient(int id)
        {
            if (id == 0)
                return RedirectToAction(nameof(ShowClients));
            else
            {
                if (CurrentReservation.Clients==null|| CurrentReservation.Clients.Count<CurrentReservation.Room.Capacity)
                {
                    var clients = (from c in _context.Clients where c.Id == id select c).ToList();
                    if (clients.Count > 0)
                    {
                        
                        CurrentReservation.Clients.Add(clients[0]);
                        return RedirectToAction(nameof(ShowClients));
                    }
                    
                    //return RedirectToAction(nameof(ShowClients));
                }
                Reservation res = CurrentReservation.GetReservation();
                try { SecurityChecker.CheckReservation(res); }
                catch (Exception)
                {
                    return RedirectToAction(nameof(Create));
                }
                _context.Add(res);
               // await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,IsBreakfastIncluded,IsAllInclusive,Price")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
