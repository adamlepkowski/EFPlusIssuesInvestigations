using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPlusIssues
{
    public class TestDbContext : DbContext
    {
        public TestDbContext() : base("test")
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
