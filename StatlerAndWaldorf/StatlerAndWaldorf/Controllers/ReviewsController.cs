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
    [Route("Reviews")]
    public class StatlerAndWaldorf : Controller
    {
        private readonly StatlerAndWaldorfContext _context;

        public StatlerAndWaldorf(StatlerAndWaldorfContext context)
        {
            _context = context;
        }

        // GET: Reviews

        [Route("Index")]
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
                    where r.movieId == movie.Id
                    where r.isBlocked == false
                    select r;
                return View(reviewsWithoutBlocked);
            }

            //in case of 2 - admin
            var context = _context.Reviews;
            IEnumerable<Reviews> reviews =
                from r in context
                where r.movieId == movie.Id
                select r;
            return View(reviews);
        }

        public async Task<IActionResult> ReturnUsersReviews(Users user)
        {
            var context = _context.Reviews;
            IEnumerable<Reviews> reviews =
               from r in context
               where r.userId == user.Id
               select r;
            return View(reviews);
        }

        // GET: Reviews/Details/5
        [Route("Details")]
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
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("userId,movieId,review")] CreateReviewDTO dto)
        {
            var reviews = await _context.Reviews.SingleOrDefaultAsync(u => u.userId == int.Parse(dto.userId) &&
                                                                        u.movieId == int.Parse(dto.movieId));

            if (reviews != null)
            {
                ModelState.AddModelError("", "you already reviewed this movie."); //we should never get here
                return View();
            }

            var review = new Reviews
            {
                userId = int.Parse(dto.userId),
                movieId = int.Parse(dto.movieId),
                review = dto.review,
                timePosted = DateTime.Now
            };

            _context.Add(review);
            await _context.SaveChangesAsync();
            return View("Profile", review);
        }


        // GET: Reviews/Edit/5
        [Route("Edit")]
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,review,isBlocked")] Reviews reviews)
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
        [Route("Delete")]
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
