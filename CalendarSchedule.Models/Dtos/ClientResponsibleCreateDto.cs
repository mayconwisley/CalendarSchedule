using System.ComponentModel.DataAnnotations;

namespace CalendarSchedule.Models.Dtos;

public class ClientResponsibleCreateDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(150)]
    public string? Email { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(150)]
    public string? Position { get; set; }

    [Required]
    public bool Active { get; set; } = true;
}
