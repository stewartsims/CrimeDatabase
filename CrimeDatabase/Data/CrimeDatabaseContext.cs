using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CrimeDatabase.Models;

namespace CrimeDatabase.Data
{
    public class CrimeDatabaseContext : DbContext
    {
        public CrimeDatabaseContext (DbContextOptions<CrimeDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<CrimeDatabase.Models.CrimeEvent> CrimeEvent { get; set; } = default!;
        public DbSet<CrimeDatabase.Models.AuditLog> AuditLog { get; set; } = default!;
    }
}
