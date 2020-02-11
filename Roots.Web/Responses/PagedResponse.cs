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
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
