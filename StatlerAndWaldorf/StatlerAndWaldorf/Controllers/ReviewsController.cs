using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StatlerAndWaldorf.Models;
using StatlerAndWaldorf.DTO;
using Microsoft.AspNetCore.Http;

namespace StatlerAndWaldorf.Controllers
{
    public class StatlerAndWaldorf : Controller
    {
        private readonly StatlerAndWaldorfContext _context;

        public StatlerAndWaldorf(StatlerAndWaldorfContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var reviews = await _context.Reviews
                .ToListAsync();
            return View(reviews);
        }

        public async Task<IActionResult> ReviewsOfMovie(Movies movie)
        {
            int role = (int)HttpContext.Session.GetInt32("Role");

            if (role == 0) //in case of guest
            {
                return View();
            }

            else if (role == 1) //in case of normal user
            {
                var contextr = _context.Reviews;
                IEnumerable<Reviews> reviewsWithoutBlocked =
                    from r in contextr
                    where r.movie.Id == movie.Id
                    where r.isBlocked == false
                    select r;
                return View(reviewsWithoutBlocked);
            }

            //in case of 2 - admin
            var context = _context.Reviews;
            IEnumerable<Reviews> reviews =
                from r in context
                where r.movie.Id == movie.Id
                select r;
            return View(reviews);
        }

        public async Task<IActionResult> ReturnUsersReviews(Users user)
        {
            var context = _context.Reviews;
            IEnumerable<Reviews> reviews =
               from r in context
               where r.user.Id == user.Id
               select r;
            return View(reviews);
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .SingleOrDefaultAsync(m => m.Id == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,user,movie,review,timePosted")] AddReviewDTO dto)
        {
            var reviews = await _context.Movies.SingleOrDefaultAsync(u => u.Id == dto.Id);
            if (reviews != null)
            {
                ModelState.AddModelError("", "This review already exists."); //we should never get here
                dto.Id = -1;
                return View();
            }

            var review = new Reviews
            {
                Id = dto.Id,
                user = dto.user,
                movie = dto.movie,
                review = dto.review,
                timePosted = dto.timePosted
            };

            _context.Add(review);
            await _context.SaveChangesAsync();
            return View("Profile", review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews.SingleOrDefaultAsync(m => m.Id == id);
            if (reviews == null)
            {
                return NotFound();
            }
            return View(reviews);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,user,movie,review,isBlocked")] Reviews reviews)
        {
            if (id != reviews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reviews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewsExists(reviews.Id))
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
            return View(reviews);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .SingleOrDefaultAsync(m => m.Id == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviews = await _context.Reviews.SingleOrDefaultAsync(m => m.Id == id);
            _context.Reviews.Remove(reviews);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewsExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
