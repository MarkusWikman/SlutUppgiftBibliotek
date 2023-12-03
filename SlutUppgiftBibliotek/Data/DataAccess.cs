using ConsoleCompanion;
using Microsoft.EntityFrameworkCore;
using SlutUppgiftBibliotek.Models;

namespace SlutUppgiftBibliotek.Data
{
    internal class DataAccess
    {
        ConsoleCompanionHelper cc = new ConsoleCompanionHelper();
        Context context = new Context();
        public void CreateStuffTest()
        {
            Book book = new Book() { IsAvailable = false, PublicationYear = 1954, DateOfLoan = DateTime.Now, PlannedDateOfReturn = DateTime.Now.AddMonths(1), ISBN = "978-0-618-34625-0", Rating = 5, Title = "Harry Potter", };
            Author author = new Author() { FirstName = "JK", LastName = "Rowling" };
            author.Books.Add(book);
            book.Authors.Add(author);
            book.AmountOfTimesBorrowed++;
            context.Books.Add(book);
            context.Authors.Add(author);
            Borrower borrower = new Borrower() { FirstName = "Elizabeth", LastName = "Andersson" };
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
        public Borrower CreateABorrowerAndCard()
        {
            Borrower borrower = new Borrower();
            borrower.FirstName = cc.AskForString("Enter the first name of the borrower: ");
            borrower.LastName = cc.AskForString("Enter the last name of the borrower: ");
            LoanCard loanCard = new LoanCard();
            loanCard.Pin = cc.AskForString("Please enter password for the loancard: ");
            borrower.LoanCard = loanCard;
            context.Borrowers.Add(borrower);
            context.LoanCards.Add(loanCard);
            context.SaveChanges();
            return borrower;
        }
        public void BorrowABook(Borrower borrower)
        {
            var availableBooks = context.Books.Where(b => b.IsAvailable == true).ToList();
            if (availableBooks.Count < 1)
            {
                Console.WriteLine("Unfortunately we have run out of books");
            }
            else
            {
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
                        availableBooks[i].AmountOfTimesBorrowed++;
                        availableBooks[i].IsAvailable = false;
                        availableBooks[i].DateOfLoan = DateTime.Now;
                        availableBooks[i].PlannedDateOfReturn = DateTime.Now.AddMonths(1);
                        borrower.Books.Add(availableBooks[i]);
                    }
                }
                context.SaveChanges();
            }
        }
        public Borrower ShowListOfBorrowersAndGetBorrower()
        {
            var l = context.Borrowers.ToList();
            if (l.Count > 0)
            {
                for (int i = 0; i < l.Count; i++)
                {
                    Console.WriteLine($"Id:{l[i].Id}: {l[i].FirstName} {l[i].LastName}");
                }
                do
                {
                    Console.WriteLine("Please enter the Id of the borrower you wish to use:");
                    int nr = int.Parse(Console.ReadLine());
                    var b = l.Where(b => b.Id == nr).FirstOrDefault();
                    if (b != null)
                    {
                        return b;
                    }
                    Console.WriteLine("You entered a non existing Id number, try again");
                } while (true);
            }
            else return null;
        }
        public Borrower GetBorrowerByFirstName(string firstName)
        {
            return context.Borrowers
                .Include(borrower => borrower.Books)
                           .Where(borrower => borrower.FirstName == firstName)
                           .First();
        }
        public Book GetBookByTitle(string title)
        {
            return context.Books
               .Where(book => book.Title == title)
               .First();
        }
        public List<Book> GetBorrowerByFirstNamesBooks(string firstName)
        {
            return context.Borrowers
                           .Include(borrower => borrower.Books)
                           .Where(borrower => borrower.FirstName == firstName)
                           .SelectMany(borrower => borrower.Books)
                           .ToList();
        }
        public void ReturnABook(Borrower borrower)
        {
            var borrowersBooks = context.Borrowers
                .Where(b => b.FirstName == borrower.FirstName)
                .SelectMany(b => b.Books)
                .ToList();
            if (borrowersBooks.Count < 1)
            {
                Console.WriteLine("Unfortunately you don't have any books");
            }
            else
            {
                Console.WriteLine("These are your books:");
                for (int i = 0; i < borrowersBooks.Count(); i++)
                {
                    Console.WriteLine($"{i}: {borrowersBooks[i].Title}");
                }
                int bookToReturn = cc.AskForInt(0, borrowersBooks.Count - 1, "Enter the number of the book you wanna return: ");
                for (int i = 0; i < borrowersBooks.Count(); i++)
                {
                    if (i == bookToReturn)
                    {
                        LoanHistory loanHistory = new LoanHistory();
                        loanHistory.DateOfLoan = borrowersBooks[i].DateOfLoan;
                        loanHistory.DateOfReturn = DateTime.Now;
                        loanHistory.Borrower = borrower;
                        loanHistory.Book = borrowersBooks[i];
                        context.LoanHistories.Add(loanHistory);

                        borrowersBooks[i].IsAvailable = true;
                        borrowersBooks[i].DateOfLoan = null;
                        borrowersBooks[i].PlannedDateOfReturn = null;
                        borrower.Books.Remove(borrowersBooks[i]);
                    }
                }
                context.SaveChanges();
            }
        }
        public void RemoveAuthorOrBookOrBorrower()
        {
            int menuChoice = cc.AskForInt(1, 3, "Would you like to remove an author[1], book[2] or a borrower[3]?:");
            switch (menuChoice)
            {
                case 1:
                    var a = context.Authors.ToList();
                    Console.WriteLine("Here is a list of all the authors:");
                    for (int i = 0; i < a.Count; i++)
                    {
                        Console.WriteLine($"{i}: {a[i].FirstName}, {a[i].LastName}");
                    }
                    int removeChoice = cc.AskForInt(0, a.Count, "Enter the number of the one you want to remove:");
                    for (int i = 0; i < a.Count(); i++)
                    {
                        if (i == removeChoice)
                        {
                            context.Authors.Remove(a[i]);
                            context.SaveChanges();
                        }
                    }
                    break;
                case 2:
                    var b = context.Books.ToList();
                    Console.WriteLine("Here is a list of all the books:");
                    for (int i = 0; i < b.Count; i++)
                    {
                        Console.WriteLine($"{i}: {b[i].Title}");
                    }
                    int bookRemoveChoice = cc.AskForInt(0, b.Count, "Enter the number of the one you want to remove:");
                    for (int i = 0; i < b.Count(); i++)
                    {
                        if (i == bookRemoveChoice)
                        {
                            context.Books.Remove(b[i]);
                            context.SaveChanges();
                        }
                    }
                    break;
                case 3:
                    var bo = context.Borrowers.ToList();
                    Console.WriteLine("Here is a list of all the borrowers:");
                    for (int i = 0; i < bo.Count; i++)
                    {
                        Console.WriteLine($"{i}: {bo[i].FirstName} {bo[i].LastName}");
                    }
                    int borrowerRemoveChoice = cc.AskForInt(0, bo.Count, "Enter the number of the one you want to remove:");
                    for (int i = 0; i < bo.Count(); i++)
                    {
                        if (i == borrowerRemoveChoice)
                        {
                            context.Borrowers.Remove(bo[i]);
                            context.SaveChanges();
                        }
                    }
                    break;
            }
        }
        public List<LoanHistory> GetLoanHistoryOnBorrower(Borrower borrower)
        {
            return context.Borrowers
                .Include(b => b.LoanHistory)
                .ThenInclude(b => b.Book)
                .Where(b => b.Equals(borrower))
                .SelectMany(b => b.LoanHistory)
                .ToList();
        }
        public List<LoanHistory> GetLoanHistoryOnBook(Book book)
        {
            return context.Books
                .Include(b => b.LoanHistory)
                .ThenInclude(b => b.Borrower)
                .Where(b => b.Equals(book))
                .SelectMany(b => b.LoanHistory)
                .ToList();
        }
        public void UseWithCautionRemoveEverything()
        {
            var allAuthors = context.Authors.ToList();
            context.Authors.RemoveRange(allAuthors);

            var allBooks = context.Books.ToList();
            context.Books.RemoveRange(allBooks);

            var allBorrowers = context.Borrowers.ToList();
            context.Borrowers.RemoveRange(allBorrowers);

            var allLoanCards = context.LoanCards.ToList();
            context.LoanCards.RemoveRange(allLoanCards);

            var allLoanHistories = context.LoanHistories.ToList();
            context.LoanHistories.RemoveRange(allLoanHistories);

            context.SaveChanges();
        }

    }
}

