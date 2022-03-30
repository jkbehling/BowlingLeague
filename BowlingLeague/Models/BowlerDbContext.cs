using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models
{
    public class BowlerDbContext : DbContext
    {
        public BowlerDbContext(DbContextOptions<BowlerDbContext> options) : base(options)
        {

        }

        public DbSet<Bowler> bowlers { get; set; }

        public DbSet<Team> teams { get; set; }
    }
    
}
