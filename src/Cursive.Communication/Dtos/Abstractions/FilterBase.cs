namespace Cursive.Communication.Dtos.Abstractions;

public abstract class FilterBase
{
    public DateTime StartDateToCreatedAt { get; set; }
    public DateTime EndDateToCreatedAt { get; set; }
    public Guid Id { get; set; }
}
