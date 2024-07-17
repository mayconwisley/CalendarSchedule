using System.ComponentModel.DataAnnotations;

namespace CalendarSchedule.Models.Dtos;

public class UserContactCreateDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Type { get; set; }

    [Required]
    [MaxLength(20)]
    public string? Number { get; set; }

    [Required]
    public int UserId { get; set; }
}
