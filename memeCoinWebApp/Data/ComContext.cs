using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using memeCoinWebApp.Models;

namespace memeCoinWebApp.Data
{
    public class ComContext : DbContext
    {
        public ComContext (DbContextOptions<ComContext> options)
            : base(options)
        {
        }

        public DbSet<memeCoinWebApp.Models.User> User { get; set; } = default!;
        public DbSet<memeCoinWebApp.Models.Transfer> Transfer { get; set; } = default!;
        public DbSet<memeCoinWebApp.Models.Fund> Fund { get; set; } = default!;
        public DbSet<memeCoinWebApp.Models.Message> Message { get; set; } = default!;
    }
}
