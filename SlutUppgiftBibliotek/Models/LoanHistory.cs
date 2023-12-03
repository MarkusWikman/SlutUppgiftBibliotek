using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutUppgiftBibliotek.Models
{
    internal class LoanHistory
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public Borrower Borrower { get; set; }
        public int BorrowerId { get; set; }
        public DateTime? DateOfLoan { get; set; }
        public DateTime? DateOfReturn { get; set; }
        public override string ToString()
        {
            return $"{Borrower.FirstName} {Borrower.LastName} loaned {Book.Title} from {DateOfLoan.ToString()} until {DateOfReturn.ToString()}";
        }
        public LoanHistory()
        {
        }
    }
}
