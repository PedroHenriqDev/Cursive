namespace Cursive.Communication.Dtos.Responses
{
    public class DocumentResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}
