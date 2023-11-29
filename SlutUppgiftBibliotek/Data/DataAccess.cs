using ConsoleCompanion;
using SlutUppgiftBibliotek.Models;

namespace SlutUppgiftBibliotek.Data
{
    internal class DataAccess
    {
        ConsoleCompanionHelper cc = new ConsoleCompanionHelper();
        Context context = new Context();
        public void CreateStuffTest()
        {
            Book book = new Book() { IsAvailable = false, DateOfLoan = new DateTime(2023, 11, 25), DateOfReturn = new DateTime(2023, 12, 25), PublicationYear = 1954, ISBN = "978-0-618-34625-0", Rating = 5, Title = "The Lord Of The Rings, The Fellowship Of The Ring", };
            Author author = new Author() { FirstName = "John Ronald Reuel", LastName = "Tolkien" };
            author.Books.Add(book);
            book.Authors.Add(author);
            context.Books.Add(book);
            context.Authors.Add(author);
            Borrower borrower = new Borrower() { FirstName = "Markus", LastName = "Wikman" };
            borrower.Books.Add(book);
            context.Borrowers.Add(borrower);
            LoanCard card = new LoanCard() { Pin = "1234", Borrower = borrower };
            context.LoanCards.Add(card);
            context.SaveChanges();
        }
        public void CreateABook()
        {
            Book book = new Book();
            book.Title = cc.AskForString("What is the title of the book?:");
            book.Authors = CreateAuthors();
            book.ISBN = cc.AskForString("Please enter the ISBN of the book:");
            book.Rating = cc.AskForInt(1, 5, "Finally, please enter the rating of the book, 1-5:");
            context.Books.Add(book);
            context.SaveChanges();
        }
        public ICollection<Author> CreateAuthors()
        {
            ICollection<Author> authorsList = new List<Author>();
            int loops = cc.AskForInt("How many authors does the book have?: ");
            for (int i = 0; i < loops; i++)
            {
                Author author = new Author();
                author.FirstName = cc.AskForString($"Enter the first name of author nr{i + 1}: ");
                author.LastName = cc.AskForString($"Enter the last name of author nr{i + 1}: ");
                authorsList.Add(author);
                context.Authors.Add(author);
                context.SaveChanges();
            }
            return authorsList;
        }
        public void CreateABorrowerAndCard()
        {
            Borrower borrower = new Borrower();
            borrower.FirstName = cc.AskForString("Enter the first name of the borrower: ");
            borrower.LastName = cc.AskForString("Enter the last name of the borrower: ");
            LoanCard loanCard = new LoanCard();
            loanCard.Pin = cc.AskForString("Please enter the password for your loancard: ");
            borrower.LoanCard = loanCard;
            context.Borrowers.Add(borrower);
            context.LoanCards.Add(loanCard);
            context.SaveChanges();
        }
        public void BorrowABook(Borrower borrower)
        {
            var availableBooks = context.Books.Where(b => b.IsAvailable == true).ToList();
            Console.WriteLine("These are the available books in the library:");
            for (int i = 0; i < availableBooks.Count(); i++)
            {
                Console.WriteLine($"{i}: {availableBooks[i].Title}");
            }
            int bookBorrowed = cc.AskForInt(0, availableBooks.Count, "Enter the number of the book you wanna borrow: ");
            for (int i = 0; i < availableBooks.Count(); i++)
            {
                if (i == bookBorrowed)
                {
                    availableBooks[i].IsAvailable = false;
                    availableBooks[i].DateOfLoan = DateTime.Now;
                    availableBooks[i].DateOfReturn = DateTime.Now.AddMonths(1);
                    borrower.Books.Add(availableBooks[i]);
                }
            }
        }
    }
}
