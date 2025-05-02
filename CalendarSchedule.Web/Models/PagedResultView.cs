namespace CalendarSchedule.Web.Models;

public class PagedResultView<T>
{
	public int TotalData { get; set; }
	public int Page { get; set; }
	public int TotalPage { get; set; }
	public int Size { get; set; }
	public IEnumerable<T>? Data { get; set; }
}
