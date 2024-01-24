using Microsoft.EntityFrameworkCore;
using NTierAppliction.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApplication.Infrastructure
{
    public class MianContext:DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }

        public MianContext(DbContextOptions<MianContext> options) : base(options) { 
        
        }
    }
}
