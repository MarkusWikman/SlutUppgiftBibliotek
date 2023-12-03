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
            //Borrower borrower = dataAccess.CreateABorrowerAndCard();
            //dataAccess.BorrowABook(borrower);
            //var borrowersBooks = borrower.Books.ToList();

            //dataAccess.CreateStuffTest();
            dataAccess.UseWithCautionRemoveEverything();

        }
    }
}