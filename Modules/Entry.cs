namespace Journal3.Modules;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Entry
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; } 
}

    
    