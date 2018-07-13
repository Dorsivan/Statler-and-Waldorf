using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StatlerAndWaldorf.Models
{
    public class StatlerAndWaldorfContext : DbContext
    {
        public StatlerAndWaldorfContext (DbContextOptions<StatlerAndWaldorfContext> options)
            : base(options)
        {
        }

        public DbSet<StatlerAndWaldorf.Models.Users> Users { get; set; }
        public DbSet<StatlerAndWaldorf.Models.Movies> Movies { get; set; }
        public DbSet<StatlerAndWaldorf.Models.Reviews> Reviews { get; set; }

    }
}
