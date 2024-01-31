
public class AngryGreetingHandler : IGreetingHandler
{
    private readonly ICalendarHandler _calendarHandler;

    public AngryGreetingHandler(ICalendarHandler calendarHandler)
    {
        _calendarHandler = calendarHandler;
    }
    public async Task<string> GetGreetingAsync(CancellationToken cancellationToken = default)
    {
        var month = _calendarHandler.GetMonth();
        return $"Have a rotten day, it is: {month}";
    }
}