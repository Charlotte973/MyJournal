using System;
using Journal3.Modules;

class Program
{
    static void Main()
    {
        
        if (Environment.GetCommandLineArgs().Length > 1 &&
            Environment.GetCommandLineArgs()[1].StartsWith("ef"))
        {
            return;
        }

        Database db = new Database();
        User? currentUser = null;

        while (true)
        {
            Console.WriteLine("\n Hi!");
            Console.WriteLine("1 - Create a new account");
            Console.WriteLine("2 - Login");
            Console.WriteLine("3 - Exit");
            Console.Write("-> Type in your choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                db.RegisterUser(username, password);
            }
            else if (choice == "2")
            {
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                currentUser = db.LoginUser(username, password);
                if (currentUser != null) break;
            }
            else if (choice == "3")
            {
                return;
            }
        }

        
        while (true)
        {
            Console.WriteLine("\n MyJournal-Menu");
            Console.WriteLine("1 - New Entry");
            Console.WriteLine("2 - Show old entries");
            Console.WriteLine("3 - Change old entry");
            Console.WriteLine("4 - Delete old entry");
            Console.WriteLine("5 - Log out");
            Console.Write("-> Type in your choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Titel: ");
                string title = Console.ReadLine();
                Console.Write("Content: ");
                string content = Console.ReadLine();
                db.CreateEntry(currentUser, title, content);
            }
            else if (choice == "2")
            {
                db.ShowEntries(currentUser);
            }
            else if (choice == "3")
            {
                Console.Write("Entry-ID: ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("New titel: ");
                string title = Console.ReadLine();
                Console.Write("New content: ");
                string content = Console.ReadLine();
                db.UpdateEntry(id, title, content);
            }
            else if (choice == "4")
            {
                Console.Write("Entry-ID: ");
                int id = int.Parse(Console.ReadLine());
                db.DeleteEntry(id);
            }
            else if (choice == "5")
            {
                Console.WriteLine("\nGoodbye! See ya");
                currentUser = null;
                break;
            }
        }
    }
}
