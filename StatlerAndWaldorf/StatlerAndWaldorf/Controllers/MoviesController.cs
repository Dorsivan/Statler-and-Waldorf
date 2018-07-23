using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StatlerAndWaldorf.DTO;
using Microsoft.AspNetCore.Http;
using StatlerAndWaldorf.Models;

namespace StatlerAndWaldorf.Controllers
{
    public class MoviesController : Controller
    {
        private readonly StatlerAndWaldorfContext _context;

        public MoviesController(StatlerAndWaldorfContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> ExploreMovies()
        {
            return View(await _context.Movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> MovieProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .SingleOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            //HttpContext.Session.SetInt32("CurrentMovieId", (int)id);

            return View(movies);
        }

        public async Task<IActionResult> AddReview(Reviews r, int? id)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(u => u.Id == id);
            movie.Reviews.Add(r);
            await _context.SaveChangesAsync();

            return View(MovieProfile(id));
        }

        public async Task<IActionResult> MovieReviews(int? id)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(u => u.Id == id);

            return View(movie.Reviews);
        }

        // GET: Movies/Create
        public IActionResult AddMovie()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovie([Bind("Title,ReleaseDate,Genre,Length")] AddMovieDTO dto)
        {
            var movies = await _context.Movies.SingleOrDefaultAsync(u => u.Title == dto.Title);
            if (movies != null)
            {
                ModelState.AddModelError("", "This movie already exists.");
                dto.Title = "";
                return View();
            }

            var movie = new Movies
            {
                Title = dto.Title,
                ReleaseDate = dto.ReleaseDate,
                Genre = dto.Genre,
                Length = int.Parse(dto.Length)
            };

            _context.Add(movie);
            await _context.SaveChangesAsync();
            return View("MovieProfile", movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }
            return View(movies);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Length,Image,ContentType")] Movies movies)
        {
            if (id != movies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesExists(movies.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ExploreMovies));
            }
            return View(movies);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .SingleOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movies = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
            _context.Movies.Remove(movies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ExploreMovies));
        }

        private bool MoviesExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
