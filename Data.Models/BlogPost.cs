using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class BlogPost
{
   
    public string? Id { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 5)] // Max length of 100 characters and min length of 5 characters
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Text { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; }
    public Category? Category { get; set; }
    public List<Tag> Tags { get; set; } = new();
}

