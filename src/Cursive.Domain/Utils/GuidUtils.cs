namespace Cursive.Domain.Utils
{
    public static class GuidUtils
    {
        public static Guid GenerateId()
        {
            return new Guid(Guid.NewGuid().ToString().Replace("-", ""));
        }
    }
}
