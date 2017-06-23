using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace EFPlusIssues
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Warmup");
            using (var dbContext = new TestDbContext())
            {
                dbContext.Contacts.Count();
            }

            RunQueriesWithoutFilter();
            RunQueriesWithGlobalFilter();
            RunQueriesWithInstanceFilter();

            Console.WriteLine("\t Done");
        }

        private static void RunQueriesWithoutFilter()
        {
            Console.WriteLine("\n ** Test 1: Queries without filter");
            using (var dbContext = new TestDbContext())
            {
                Console.WriteLine("\t First time query execution");
                dbContext.Contacts.Where(x => x.IsDeleted).ToList();
            }

            using (var dbContext = new TestDbContext())
            {
                Console.WriteLine("\t Second time query execution");
                dbContext.Contacts.Where(x => x.IsDeleted).ToList();
            }
        }

        private static void RunQueriesWithGlobalFilter()
        {
            Console.WriteLine("\n ** Test 2: Queries with global filter");
            QueryFilterManager.Filter<Contact>(q => q.Where(x => x.IsDeleted == true));

            using (var dbContext = new TestDbContext())
            {
                Console.WriteLine("\t First time query execution");
                QueryFilterManager.InitilizeGlobalFilter(dbContext);
                dbContext.Contacts.ToList();
            }

            using (var dbContext = new TestDbContext())
            {
                Console.WriteLine("\t Second time query execution");
                QueryFilterManager.InitilizeGlobalFilter(dbContext);
                dbContext.Contacts.ToList();
            }
        }

        private static void RunQueriesWithInstanceFilter()
        {
            Console.WriteLine("\n ** Test 3: Queries with instance filter");
            using (var dbContext = new TestDbContext())
            {
                Console.WriteLine("\t First time query execution");
                dbContext.Filter<Contact>(q => q.Where(x => x.IsDeleted));
                dbContext.Contacts.ToList();
            }

            using (var dbContext = new TestDbContext())
            {
                Console.WriteLine("\t Second time query execution");
                dbContext.Filter<Contact>(q => q.Where(x => x.IsDeleted));
                dbContext.Contacts.ToList();
            }
        }
    }
}
