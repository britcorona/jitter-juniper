using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity; //This allows you to inherit

namespace Jitter.Models
{
    public class JitterContext : DbContext //This now inherits from a Db (database). Uses default connections.
    {
        public DbSet<JitterUser> JitterUsers { get; set; } //This creates the table in the database
        public DbSet<Jot> Jots { get; set; } //Way into the database.
    }
}