using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StatlerAndWaldorf.Models;

namespace StatlerAndWaldorf.Controllers
{
    public class UsersController : Controller
    {
        private readonly StatlerAndWaldorfContext _context;

        public UsersController(StatlerAndWaldorfContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Search (string searchString)
        {
            var user = from u in _context.Users select u;
            if (!String.IsNullOrEmpty(searchString))
            {
                user = user.Where(u => u.firstName.Contains(searchString) || u.lastName.Contains(searchString));
            }

            return View(await user.ToListAsync());
        }

        public async Task<IActionResult> Profile(Users user)
        {
            return View(user);
        }

        public async Task<IActionResult> MakeAdmin(int id)
        {
            var user = await _context.Users
               .SingleOrDefaultAsync(m => m.Id == id);

            bool temp = user.admin;
            user.admin = !temp;

            return RedirectToAction(nameof(showAllUsers));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: Users/login
        public async Task<IActionResult> Login([Bind("email,password")] DTO.LoginDTO dto)
        {
            //hashing password
            byte [] passwordBytes = Encoding.ASCII.GetBytes(dto.password);
            var md5 = new MD5CryptoServiceProvider();
            byte [] md5data = md5.ComputeHash(passwordBytes);
            string passwordHash = Encoding.ASCII.GetString(md5data);


            var users = await _context.Users
                .SingleOrDefaultAsync(m => m.email == dto.email && m.passwordHash == passwordHash);

            if (users == null)
            {
                ModelState.AddModelError("", "Wrong password or email.");
                return View();
            }

            users.lastSeen = DateTime.Now;
            _context.Users.Update(users);
            return View("Profile", users);
        }

        // GET: All Users
        public async Task<IActionResult> showAllUsers()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Signup
        public IActionResult Signup()
        {
            return View();
        }

        // POST: Users/Signup
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([Bind("email,password,firstName,lastName")] DTO.RegisterDTO dto)
        {
            var users = await _context.Users.SingleOrDefaultAsync(u => u.email == dto.email);
            if (users != null)
            {
                ModelState.AddModelError("", "User with this email already exist.");
                dto.email = "";
                return View();
            }

            byte[] passwordBytes = Encoding.ASCII.GetBytes(dto.password);
            var md5 = new MD5CryptoServiceProvider();
            byte[] md5data = md5.ComputeHash(passwordBytes);
            string passwordHash = Encoding.ASCII.GetString(md5data);

            var user = new Users
            {
                email = dto.email,
                firstName = dto.firstName,
                lastName = dto.lastName,
                passwordHash = passwordHash,
                admin = false
            };

            _context.Add(user);
            await _context.SaveChangesAsync();
            return View("Profile", user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,email,passwordHash,firstName,lastName,admin")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(showAllUsers));
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(showAllUsers));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Details(int id)
        {
            var users = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            return View(users);
        }
    }
}
