using BuscandoMiTrago.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuscandoMiTrago.DtaCtx
{
    public class DbContextt: DbContext
    {
        public DbContextt(DbContextOptions<DbContextt> options) : base(options) { }

        public virtual DbSet<drinks> Drinks { get; set; }
    }
}
