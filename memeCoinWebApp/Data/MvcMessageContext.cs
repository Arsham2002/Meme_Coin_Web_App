using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using memeCoinWebApp.Models;

namespace MvcMessage.Data
{
    public class MvcMessageContext : DbContext
    {
        public MvcMessageContext (DbContextOptions<MvcMessageContext> options)
            : base(options)
        {
        }

        public DbSet<memeCoinWebApp.Models.Message> Message { get; set; } = default!;
    }
}
