using Microsoft.EntityFrameworkCore;
using PolskieBrowary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolskieBrowary.Data
{
    public class PolskieBrowaryContext : DbContext
    {
        public PolskieBrowaryContext(DbContextOptions<PolskieBrowaryContext> options) : base(options)
        {
        }

        public DbSet<Brevery> Brevery { get; set; }
        public DbSet<Beer> Beer { get; set; }
        public DbSet<BeerGenre> BeerGenre { get; set; }
        public DbSet<Comment> Comment { get; set; }
    }
}
