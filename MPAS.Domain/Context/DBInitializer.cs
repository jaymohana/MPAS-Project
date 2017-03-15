using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAS.Domain.Context
{
   
    public class DBInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            base.Seed(context);
        }
    }
}
