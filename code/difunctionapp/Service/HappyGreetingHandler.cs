
public class HappyGreetingHandler : IGreetingHandler
{
    private readonly ICalendarHandler _calendarHandler;

    public HappyGreetingHandler(ICalendarHandler calendarHandler)
    {
        _calendarHandler = calendarHandler;
    }
    public async Task<string> GetGreetingAsync(CancellationToken cancellationToken = default)
    {
        var month = _calendarHandler.GetMonth();
        return $"Have a nice day it is {month}";
    }
}