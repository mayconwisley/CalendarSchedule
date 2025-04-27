namespace CalendarSchedule.Models.Dtos;

public sealed record PagedResult<T>(IEnumerable<T> Data, decimal TotalData, decimal Page, decimal TotalPage, decimal Size);
