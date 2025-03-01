
using System.Text.Json.Serialization;

namespace Cursive.Communication.Dtos.Requests;

public class DocumentPutRequest : DocumentRequest
{
    [JsonIgnore]
    public override Guid UserId { get; set; }
}
