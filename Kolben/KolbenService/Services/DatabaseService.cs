using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolbenService.Services
{
    public class DatabaseService
    {
        public void InitialiseDatabase()
        {
            LocalDatabase.CreateDatabase();
        }
    }
}
