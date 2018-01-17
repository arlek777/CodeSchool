namespace CodeSchool.Web.Infrastructure.AppSettings
{
    public class CacheValues
    {
        public int StaticContent { get; set; }
        public int DynamicContent { get; set; }
    }

    public class CacheSettings
    {
        public CacheValues Local { get; set; }
        public CacheValues Remote { get; set; }
    }
}
