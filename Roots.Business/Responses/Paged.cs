namespace Roots.Business.Responses
{
    public class Paged<T>
    {
        public Paged() { }

        public Paged(T response)
        {
            Data = response;
        }

        public T Data { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
