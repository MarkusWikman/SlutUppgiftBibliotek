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

            //dataAccess.CreateStuffTest();

            var s = dataAccess.GetBorrowerByFirstName("Elizabeth");
            var l = dataAccess.GetLoanHistoryOnBorrower(s);
            for (int i = 0; i < l.Count; i++)
            {
                Console.WriteLine($"{l[i]}");
            }
            //dataAccess.ReturnABook(s);
        }
    }
}