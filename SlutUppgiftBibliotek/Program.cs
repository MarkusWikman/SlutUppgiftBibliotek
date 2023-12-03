using ConsoleCompanion;
using SlutUppgiftBibliotek.Data;
using SlutUppgiftBibliotek.Models;

namespace SlutUppgiftBibliotek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataAccess dataAccess = new DataAccess();
            //dataAccess.UseWithCautionRemoveEverything();

            dataAccess.CreateStuffTest();

            //var s = dataAccess.GetBorrowerByFirstName("Elizabeth");
            //dataAccess.ReturnABook(s);
        }
    }
}