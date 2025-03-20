namespace Journal3.Modules;
using Microsoft.EntityFrameworkCore;

public class Database
{
    private readonly AppDbContext _db;

    public Database()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Data Source=MeineDatenbank.db")
            .Options;
        _db = new AppDbContext(options);
    }

    
    public void RegisterUser(string username, string password)
    {
        using (var db = new AppDbContext());
        {
            if (_db.Users.Any(u => u.Username == username))
            {
                Console.WriteLine("Username already exists!");
                return;
            }

            var user = new User { Username = username, PasswordHash = password };
            _db.Users.Add(user);
            _db.SaveChanges();
            Console.WriteLine("You are successfully registered now!");
        }
    }

    
    public User? LoginUser(string username, string password)
    {
        var user = _db.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == password);
        if (user != null)
        {
            Console.WriteLine("\nWelcome MyJournal!");
        }
        else
        {
            Console.WriteLine("Wrong username or password...");
        }
        return user;
    }

    
    public void CreateEntry(User user, string title, string content)
    {
        if (user == null)
        {
            Console.WriteLine("You have to sign in first!");
            return;
        }

        var entry = new Entry { Title = title, Content = content, UserId = user.Id };
        _db.Entries.Add(entry);
        _db.SaveChanges();
        Console.WriteLine("\nNew entry has been created!");
    }

    
    
    public void ShowEntries(User? user)
    {
        if (user == null)
        {
            Console.WriteLine("You must be logged in to see entries!");
            return;
        }
        
        Console.WriteLine($"DEBUG: Checking entries for UserID: {user.Id}");
        
        
        using (var db = new AppDbContext())
        {
            var entries = db.Entries.Where(e => e.UserId == user.Id).ToList();
            
            Console.WriteLine($"DEBUG: Found {entries.Count} entries in DB");
        
            if (entries.Count == 0)
            {
                Console.WriteLine("No entries found...");
                return;
            }

            Console.WriteLine("\nYour MyJournal Entries:");
            foreach (var entry in entries)
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"ID: {entry.Id}");
                Console.WriteLine($"Date: {entry.CreatedAt.ToString("dd.MM.yyyy HH:mm")}");
                Console.WriteLine($"Title: {entry.Title}");
                Console.WriteLine($"Content: {entry.Content}\n");
                Console.WriteLine("---------------------------------------------------\n");
            }
        }
    }
    
    
    public void UpdateEntry(int entryId, string newTitle, string newContent)
    {
        using (var db = new AppDbContext())
        {
            var entry = db.Entries.Find(entryId);
            if (entry == null)
            {
                Console.WriteLine("Entry could not be found...");
                return;
            }

            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("\nYour outdated entry:");
            Console.WriteLine($"Title: {entry.Title}");
            Console.WriteLine($"Content: {entry.Content}");
            Console.WriteLine("\n------------------------------------------------------");

            entry.Title = newTitle;
            entry.Content = newContent;
            db.SaveChanges();
            
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("\nYour updated entry:");
            Console.WriteLine("After Change:");
            Console.WriteLine($"Title: {entry.Title}");
            Console.WriteLine($"Content: {entry.Content}");
            Console.WriteLine("\n----------------------------------------------------");
        }
    }
    

    
    public void DeleteEntry(int entryId)
    {
        var entry = _db.Entries.Find(entryId);
        if (entry == null)
        {
            Console.WriteLine("Entry could not be found...");
            return;
        }

        _db.Entries.Remove(entry);
        _db.SaveChanges();
        Console.WriteLine("\nEntry has been deleted!");
    }
}