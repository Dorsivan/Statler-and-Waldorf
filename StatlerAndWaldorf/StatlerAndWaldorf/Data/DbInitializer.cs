using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatlerAndWaldorf.Models;
using System.Security.Cryptography;
using System.Text;

namespace StatlerAndWaldorf.Data
{
    public class DbInitializer
    {
        public static void Initialize(StatlerAndWaldorfContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            byte[] passwordBytes = Encoding.ASCII.GetBytes("123456");
            var md5 = new MD5CryptoServiceProvider();
            byte[] md5data = md5.ComputeHash(passwordBytes);
            string passwordHash = Encoding.ASCII.GetString(md5data);

            var users = new Users[]
            {
            new Users{Id = 1,email = "arieli.barak@gmail.com",firstName="Ariel",lastName="Barak",passwordHash= passwordHash ,admin = true,country="Israel"},
            new Users{Id = 2,email = "a@a",firstName="a",lastName="b",passwordHash= passwordHash ,admin = false,country="Israel"}
            };
            foreach (Users u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            DateTime date = new DateTime(2016, 05, 23);//new date for movie creation

            Reviews review = new Reviews()//new review, without movie
            {
                Id = 1,
                review = "The movie really was adorable! I knew exactly what to expect since I read the book, but I loved the adaptation. What helps the movie most is that the novelist, Jojo Moyees, wrote her own screenplay.She did a great job transitioning the story from paper to screen.Little things that were left out of the novel werent really needed to move the story along. Both Emilia Clarke and Sam Clafin were excellent. Her facial expressions were amazing and her insane eyebrows deserve some type of award all on their own.",
                isBlocked = false,
                user = users[1]
            };

            Movies movie = new Movies//new movie
            {
                Id = 1,
                Title = "Me Before You",
                Genre = "Drama",
                Length = 111,
                ReleaseDate = date
            };

            review.movie = movie;
            movie.Reviews.Add(review);

            var movies = new Movies[]
            {
                movie
            };

            foreach (Movies m in movies)
            {
                context.Movies.Add(m);
            }
            context.SaveChanges();

            var reviews = new Reviews[]
            {
                review
            };
            foreach (Reviews r in reviews)
            {
                context.Reviews.Add(r);
            }
            context.SaveChanges();
        }
    }
}
