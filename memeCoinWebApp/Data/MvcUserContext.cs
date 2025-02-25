using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using memeCoinWebApp.Models;

namespace MvcUser.Data
{
    public class MvcUserContext : DbContext
    {
        public MvcUserContext (DbContextOptions<MvcUserContext> options)
            : base(options)
        {
        }

        public DbSet<memeCoinWebApp.Models.User> User { get; set; } = default!;
    }
}
