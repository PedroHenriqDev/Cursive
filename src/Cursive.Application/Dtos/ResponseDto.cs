namespace Cursive.API.Dtos;

public class ResponseDto<T>
{
    public string Message { get; set; } = string.Empty;
    public DateTime ResponseDate { get; set; }
    public T? Value { get; set; }
}
