using ConsoleCompanion;
using SlutUppgiftBibliotek.Data;
using System.Text;

namespace SlutUppgiftBibliotek
{
    internal class Program
    {
        static ConsoleCompanionHelper cc = new ConsoleCompanionHelper();
        static DataAccess dataAccess = new DataAccess();
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            //dataAccess.SeedingMethod();
            ShowMenu();
            //dataAccess.UseWithCautionRemoveAllDataFromDB();
        }
        static void ShowMenu()
        {
            bool showMenu = true;
            while (showMenu)
            {
                Console.Clear();
                Console.WriteLine("⭐📚Newton Library Database Menu📚⭐");
                Console.WriteLine("\n[1] Create book📗");
                Console.WriteLine("[2] Create author🧙🏽‍");
                Console.WriteLine("[3] Create borrower🙆‍");
                Console.WriteLine("[4] Borrow book📘");
                Console.WriteLine("[5] Return book📕");
                Console.WriteLine("[6] Remove book / author / borrower🔞");
                Console.WriteLine("[7] Show loaning history on a book📜📙");
                Console.WriteLine("[8] Show loaning history on a borrower📜🙆‍");
                Console.WriteLine("[9] Exit program❌");
                int menuChoice = cc.AskForInt(1, 9, "\nEnter the number of your menu choice: ");
                switch (menuChoice)
                {
                    case 1:
                        dataAccess.CreateABook();
                        break;
                    case 2:
                        dataAccess.CreateAuthorsForMenuChoiceTwo();
                        break;
                    case 3:
                        dataAccess.CreateABorrowerAndCard();
                        break;
                    case 4:
                        dataAccess.BorrowABook(dataAccess.ShowListOfBorrowersAndGetBorrower());
                        break;
                    case 5:
                        dataAccess.ReturnABook(dataAccess.ShowListOfBorrowersAndGetBorrower());
                        break;
                    case 6:
                        dataAccess.RemoveAuthorOrBookOrBorrower();
                        break;
                    case 7:
                        var bookList = dataAccess.GetLoanHistoryOnBook(dataAccess.ShowListOfBooksAndGetBook());
                        if (bookList.Count > 0)
                        {
                            for (int i = 0; i < bookList.Count; i++)
                            {
                                Console.WriteLine($"{bookList[i]}");
                            }
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("There is no loaning history on this book");
                            Console.ReadLine();
                        }
                        break;
                    case 8:
                        var list = dataAccess.GetLoanHistoryOnBorrower(dataAccess.ShowListOfBorrowersAndGetBorrower());
                        if (list.Count > 0)
                        {
                            for (int i = 0; i < list.Count; i++)
                            {
                                Console.WriteLine($"{list[i]}");
                            }
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("There is no loaning history on this borrower");
                            Console.ReadLine();
                        }
                        break;
                    case 9:
                        showMenu = false;
                        break;
                }
            }
        }
    }
}