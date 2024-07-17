using System.ComponentModel.DataAnnotations;

namespace CalendarSchedule.Models.Dtos;

public class ScheduleUserCreateDto
{
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int ScheduleId { get; set; }
}
