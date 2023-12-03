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
            ShowMenu();



            //dataAccess.UseWithCautionRemoveEverything();

            //dataAccess.CreateStuffTest();

            //var s = dataAccess.GetBookByTitle("Harry Potter");
            //var l = dataAccess.GetLoanHistoryOnBook(s);
            //for (int i = 0; i < l.Count; i++)
            //{
            //    Console.WriteLine($"{l[i]}");
            //}

            //dataAccess.ReturnABook(s);
        }
        static void ShowMenu()
        {
            bool showMenu = true;
            Console.WriteLine("⭐📚Newton Library Database Main Menu📚⭐");
            while (showMenu)
            {
                Console.Clear();
                Console.WriteLine("[1] Create book");
                Console.WriteLine("[2] Create author(s)");
                Console.WriteLine("[3] Create borrower");
                Console.WriteLine("[4] Borrow book");
                Console.WriteLine("[5] Return book");
                Console.WriteLine("[6] Remove book / author / borrower");
                Console.WriteLine("[7] Exit program");
                int menuChoice = cc.AskForInt(1, 7, "Enter your menu choice");
                switch (menuChoice)
                {
                    case 1:
                        dataAccess.CreateABook();
                        break;
                    case 2:
                        dataAccess.CreateAuthors();
                        break;
                    case 3:
                        dataAccess.CreateABorrowerAndCard();
                        break;
                    case 4:
                        dataAccess.BorrowABook(dataAccess.ShowListOfBorrowersAndGetBorrower());
                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                    case 7:
                        showMenu = false;
                        break;
                }
            }
        }
    }
}