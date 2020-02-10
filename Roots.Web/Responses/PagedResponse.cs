namespace Roots.Web.Responses
{
    public class PagedResponse<T>
    {
        public PagedResponse() { }

        public PagedResponse(T response)
        {
            Data = response;
        }

        public T Data { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
