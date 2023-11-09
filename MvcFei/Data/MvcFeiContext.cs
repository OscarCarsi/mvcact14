using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcFei.Models;

namespace MvcFei.Data
{
    public class MvcFeiContext : DbContext
    {
        public MvcFeiContext (DbContextOptions<MvcFeiContext> options)
            : base(options)
        {
        }

        public DbSet<MvcFei.Models.Alumno> Alumno { get; set; } = default!;
    }
}
