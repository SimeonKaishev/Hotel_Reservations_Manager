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
           // HttpContext.Items.Add("startdate", reservation.StartDate.ToString());
            TempData["startdate"] = reservation.StartDate;
            //reservation.Reserver = CurrentUser.GetCurrent(_context);
           // HttpContext.Items.Add("enddate", reservation.EndDate.ToString());
            TempData["enddate"] = reservation.EndDate;
           // HttpContext.Items.Add("IsAllInclusive", reservation.IsAllInclusive.ToString());
            TempData["allink"] = reservation.IsAllInclusive;
           // CurrentReservation.SetResFirst(reservation);
           // HttpContext.Items.Add("breakfast", reservation.IsBreakfastIncluded.ToString());
            TempData["breakfast"] = reservation.IsBreakfastIncluded;
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
                   // HttpContext.Items.Add("roomId", rooms[0].Id);
                    TempData["roomid"] = rooms[0].Id;
                    TempData["roomcap"] = rooms[0].Capacity;
                    TempData["clientsnum"] = 0;
                    //List<Client> clients = new List<Client>();
                    // HttpContext.Items.Add("clientIds","");
                    TempData["clientids"] =" ";
                    //CurrentReservation.Room = rooms[0];//
                    //CurrentReservation.Clients = new List<Client>();//
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
                if ( int.Parse(TempData["clientsnum"].ToString())< int.Parse(TempData["roomcap"].ToString()))
                {
                    var clients = (from c in _context.Clients where c.Id == id select c).ToList();
                    if (clients.Count > 0)
                    {
                        string clientIds;
                        try
                        {
                             clientIds = TempData["clientids"].ToString();
                        }
                        catch (Exception)
                        {
                            clientIds = "";
                        }
                        clientIds += (clients[0].Id.ToString() + " ");
                        TempData["clientsnum"] = int.Parse(TempData["clientsnum"].ToString()) + 1;
                        //HttpContext.Items["clientIds"] = clientIds;
                        TempData["clientids"] = clientIds;
                       // CurrentReservation.Clients.Add(clients[0]);
                        return RedirectToAction(nameof(ShowClients));
                    }
                    
                    //return RedirectToAction(nameof(ShowClients));
                }
                // Reservation res = CurrentReservation.GetReservation();
                Reservation res = new Reservation();
                res.StartDate = DateTime.Parse(TempData["startdate"].ToString());
                 res.EndDate = DateTime.Parse(TempData["enddate"].ToString());
                res.IsAllInclusive = bool.Parse(TempData["allink"].ToString());
                res.IsAllInclusive = bool.Parse(TempData["breakfast"].ToString());
               // string a = TempData["clientids"].ToString();
                res.Reserver = CurrentReservation.GetReserver(int.Parse(TempData["userId"].ToString()), _context);
                res.Room = CurrentReservation.GetRoom(int.Parse(TempData["roomid"].ToString()), _context);
                res.Clients =CurrentReservation.GetClients(TempData["clientids"].ToString().Substring(1), _context);
                res.Price = CurrentReservation.CalkPrice(res);
                try { SecurityChecker.CheckReservation(res); }
                catch (Exception)
                {
                    return RedirectToAction(nameof(Create));
                }
                _context.Add(res);
                await _context.SaveChangesAsync();
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
