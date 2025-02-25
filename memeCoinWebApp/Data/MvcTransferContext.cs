using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using memeCoinWebApp.Models;

namespace MvcTransfer.Data
{
    public class MvcTransferContext : DbContext
    {
        public MvcTransferContext (DbContextOptions<MvcTransferContext> options)
            : base(options)
        {
        }

        public DbSet<memeCoinWebApp.Models.Transfer> Transfer { get; set; } = default!;
    }
}
