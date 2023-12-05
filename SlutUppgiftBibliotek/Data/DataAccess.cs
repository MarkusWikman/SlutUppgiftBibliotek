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
            Book book2 = new Book() { IsAvailable = false, PublicationYear = 2022, DateOfLoan = DateTime.Now, PlannedDateOfReturn = DateTime.Now.AddMonths(1), ISBN = "918-0-029-9310-0", Rating = 3, Title = "Programmers guide to the galaxy", };
            Book book3 = new Book() { IsAvailable = false, PublicationYear = 2010, DateOfLoan = DateTime.Now, PlannedDateOfReturn = DateTime.Now.AddMonths(1), ISBN = "291-0-079-1298-1", Rating = 1, Title = "How women work", };
            Book book4 = new Book() { IsAvailable = true, PublicationYear = 1990, DateOfLoan = DateTime.Now, PlannedDateOfReturn = DateTime.Now.AddMonths(1), ISBN = "918-0-213-0293-4", Rating = 5, Title = "The Alchemist", };
            Book book5 = new Book() { IsAvailable = true, PublicationYear = 2010, DateOfLoan = DateTime.Now, PlannedDateOfReturn = DateTime.Now.AddMonths(1), ISBN = "929-0-079-1298-8", Rating = 5, Title = "1984", };

            Author author = new Author() { FirstName = "JK", LastName = "Rowling" };
            Author author2 = new Author() { FirstName = "Markus", LastName = "Wikman" };
            Author author3 = new Author() { FirstName = "Lucas", LastName = "Bergström" };
            Author author4 = new Author() { FirstName = "Paulo", LastName = "Coelho" };
            Author author5 = new Author() { FirstName = "George", LastName = "Orwell" };

            author.Books.Add(book);
            author2.Books.Add(book2);
            author3.Books.Add(book3);
            author4.Books.Add(book3); // Visa exempel på att en bok kan ha fler författare
            author4.Books.Add(book4);
            author5.Books.Add(book5);

            book.Authors.Add(author);
            book2.Authors.Add(author2);
            book3.Authors.Add(author3);
            book3.Authors.Add(author4);
            book4.Authors.Add(author4);
            book5.Authors.Add(author5);

            book.AmountOfTimesBorrowed++;
            book2.AmountOfTimesBorrowed++;
            book3.AmountOfTimesBorrowed++;

            context.Books.Add(book);
            context.Books.Add(book2);
            context.Books.Add(book3);
            context.Books.Add(book4);
            context.Books.Add(book5);

            context.Authors.Add(author);
            context.Authors.Add(author2);
            context.Authors.Add(author3);
            context.Authors.Add(author4);
            context.Authors.Add(author5);

            Borrower borrower = new Borrower() { FirstName = "Elizabeth", LastName = "Andersson" };
            borrower.Books.Add(book);
            Borrower borrower2 = new Borrower() { FirstName = "Jens", LastName = "Palmqvist" };
            borrower2.Books.Add(book2);
            Borrower borrower3 = new Borrower() { FirstName = "Hoger", LastName = "Najmadin" };
            borrower3.Books.Add(book3);
            Borrower borrower4 = new Borrower() { FirstName = "Eva", LastName = "Andersson" };

            context.Borrowers.Add(borrower);
            context.Borrowers.Add(borrower2);
            context.Borrowers.Add(borrower3);
            context.Borrowers.Add(borrower4);

            LoanCard card = new LoanCard() { Pin = "1234", Borrower = borrower };
            LoanCard card2 = new LoanCard() { Pin = "4321", Borrower = borrower2 };
            LoanCard card3 = new LoanCard() { Pin = "1998", Borrower = borrower3 };
            LoanCard card4 = new LoanCard() { Pin = "2005", Borrower = borrower4 };

            context.LoanCards.Add(card);
            context.LoanCards.Add(card2);
            context.LoanCards.Add(card3);
            context.LoanCards.Add(card4);

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
        public void CreateAuthorsForMenuChoiceTwo()
        {
            Author author = new Author();
            author.FirstName = cc.AskForString($"Enter the first name of author: ");
            author.LastName = cc.AskForString($"Enter the last name of author: ");
            context.Authors.Add(author);
            context.SaveChanges();
            Console.WriteLine("Author has been created and added to database");
            Console.ReadLine();
        }
        public ICollection<Author> CreateAuthors()
        {
            Console.WriteLine("Do you wish to use already existing author(s) in the DB? If your answer is yes press 1, otherwise press any button:");
            if (int.Parse(Console.ReadLine()) != 1)
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
            else
            {
                return ShowListOfAuthorsAndGetAuthor();
            }
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
        public Book ShowListOfBooksAndGetBook()
        {
            var l = context.Books.ToList();
            if (l.Count > 0)
            {
                for (int i = 0; i < l.Count; i++)
                {
                    Console.WriteLine($"Id:{l[i].Id}: {l[i].Title}");
                }
                do
                {
                    Console.WriteLine("Please enter the Id of the book you wish to use:");
                    int nr = int.Parse(Console.ReadLine());
                    var b = l.Where(b => b.Id == nr).FirstOrDefault();
                    if (b != null)
                    {
                        return b;
                    }
                    Console.WriteLine("You entered a non existing Id number, try again");
                } while (true);
            }
            return null;
        }
        public ICollection<Author> ShowListOfAuthorsAndGetAuthor()
        {
            var l = context.Authors.ToList();
            if (l.Count > 0)
            {
                int amountOfAuthors = cc.AskForInt(0, l.Count, "How many authors do you wish to use for this book?: ");
                for (int i = 0; i < l.Count; i++)
                {
                    Console.WriteLine($"Id:{l[i].Id}: {l[i].FirstName} {l[i].LastName}");
                }
                ICollection<Author> result = new List<Author>();
                for (int i = 0; i < amountOfAuthors; i++)
                {
                    Console.WriteLine("Please enter the Id of the author you wish to use:");
                    int nr = int.Parse(Console.ReadLine());
                    var b = l.Where(b => b.Id == nr).FirstOrDefault();
                    if (b != null)
                    {
                        result.Add(b);
                        continue;
                    }
                }
                return result;
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
            var loanHistories = context.LoanHistories
               .Where(l => l.BorrowerId == borrower.Id)
               .Include(l => l.Book)
               .ToList();
            return loanHistories;
        }

        public List<LoanHistory> GetLoanHistoryOnBook(Book book)
        {
            var loanHistories = context.LoanHistories
               .Where(l => l.BookId == book.Id)
               .Include(l => l.Borrower)
               .ToList();
            return loanHistories;
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

