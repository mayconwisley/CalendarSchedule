using System.ComponentModel.DataAnnotations;

namespace ScheduleRooms.Models.Dtos;

public class ScheduleUserCreateDto
{
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int ScheduleId { get; set; }
}
