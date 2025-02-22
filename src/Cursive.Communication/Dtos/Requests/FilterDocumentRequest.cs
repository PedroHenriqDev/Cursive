using System.Web.Http.ModelBinding;
using Cursive.Communication.Binders;
using Cursive.Communication.Dtos.Abstractions;

namespace Cursive.Communication.Dtos.Requests;

[ModelBinder(BinderType = typeof(TypeComplexBinder))]
public class FilterDocumentRequest : FilterBase
{
    public string Title { get; set; } = string.Empty;   
}
