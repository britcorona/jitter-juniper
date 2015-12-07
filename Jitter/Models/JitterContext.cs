using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Jitter.Models
{
    public class JitterContext : ApplicationDbContext //dec_7 branch
    {
        // IDbSet, IQueryable
        public virtual DbSet<JitterUser> JitterUsers { get; set; }
        public virtual DbSet<Jot> Jots { get; set; }
    }
}