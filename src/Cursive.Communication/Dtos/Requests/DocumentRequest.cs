namespace Cursive.Communication.Dtos.Requests;

public class DocumentRequest
{
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public virtual Guid UserId { get; set; }
}
