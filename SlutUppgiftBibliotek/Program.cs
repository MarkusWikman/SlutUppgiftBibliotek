using ConsoleCompanion;
using SlutUppgiftBibliotek.Data;

namespace SlutUppgiftBibliotek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataAccess dataAccess = new DataAccess();
            dataAccess.BorrowABook();
        }
    }
}