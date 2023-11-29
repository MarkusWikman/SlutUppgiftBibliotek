using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlutUppgiftBibliotek.Models;

namespace SlutUppgiftBibliotek.Data
{
    internal class DataAccess
    {
        Context context = new Context();
        public void CreateStuffTest()
        {
            Book book = new Book() { IsAvailable = false, DateOfLoan = new DateTime(2023, 11, 25), DateOfReturn = new DateTime(2023, 12, 25), PublicationYear = 1954, ISBN = "978-0-618-34625-0" };
            Borrower borrower = new Borrower() { FirstName = "Markus", LastName = "Wikman", };
            context.Borrowers.Add(borrower);
            LoanCard card = new LoanCard() { Pin = "1234", Borrower = borrower };
            context.LoanCards.Add(card);
            context.SaveChanges();
        }
    }
}
