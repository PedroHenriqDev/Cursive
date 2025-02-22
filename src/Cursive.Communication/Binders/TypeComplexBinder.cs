using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using Cursive.Communication.Dtos.Requests;

namespace Cursive.Communication.Binders;

public class TypeComplexBinder : IModelBinder
{
    public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
    {
        IDictionary<string, string> queryStringAsKvp = actionContext.Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value);

        foreach (PropertyInfo prop in bindingContext.ModelType.GetProperties())
        {
            string value = queryStringAsKvp[prop.Name];

            if (int.TryParse(value, out int valueAsInt))
            {
                prop.SetValue(bindingContext.Model, valueAsInt);
            }
            else if (DateTime.TryParse(value, out DateTime valueAsDateTime))
            {
                prop.SetValue(bindingContext.Model, valueAsDateTime);
            }
            else if (Guid.TryParse(value, out Guid valueAsGuid))
            {
                prop.SetValue(bindingContext, valueAsGuid);
            }
            else
            {
                prop.SetValue(bindingContext, value);
            }
        }

        return true;
    }
}
