namespace Journal3.Modules;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;


public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Entry> Entries { get; set; }
    
    public AppDbContext () {}

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlite("Data Source=MeineDatenbank.db");
        }
    }
}
