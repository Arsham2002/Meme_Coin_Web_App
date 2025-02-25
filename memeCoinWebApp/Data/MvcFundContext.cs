using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using memeCoinWebApp.Models;

namespace MvcFund.Data
{
    public class MvcFundContext : DbContext
    {
        public MvcFundContext (DbContextOptions<MvcFundContext> options)
            : base(options)
        {
        }

        public DbSet<memeCoinWebApp.Models.Fund> Fund { get; set; } = default!;
    }
}
