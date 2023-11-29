using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutUppgiftBibliotek.Models
{
    internal class Borrower
    {
        public int Id { get; set; }
        public int LoanCardId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public LoanCard LoanCard { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
