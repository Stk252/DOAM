using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DOAM.Infrastructure.DB.DOAMEntities db = new DOAM.Infrastructure.DB.DOAMEntities())
            {
                Console.WriteLine(db.Assets.Count() > 0 ? "There are assets in the DB" : "The are currently no assets");
                Console.ReadLine();
            }
        }
    }
}
