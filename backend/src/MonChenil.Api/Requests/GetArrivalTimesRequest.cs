namespace MonChenil.Api.Requests;

public record GetArrivalTimesRequest
{
    public DateTime StartDate { get; init; } = DateTime.Now;
    public DateTime EndDate { get; init; } = DateTime.Now.Date.AddDays(1);
}