using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DbContextFactory
    {
        public static IDbContextBase GetDbContext()
        {
            try
            {
                return new DbContextBase();
            }
            catch
            {

                throw;
            }
        }
    }
}
