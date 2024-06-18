using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleRooms.Models.Dtos;

public class UserContactCreateDto
{
    public int Id { get; set; }
    public string? Type { get; set; }
    public string? Number { get; set; }
    public int UserId { get; set; }
}
