namespace GamesApi.Models
{
    public class PageList<T>
    {
        public PageList(IEnumerable<T> items, int currentPage, int pageSize)
        {
            if (currentPage <= 0)
                currentPage = 1;
            if (pageSize <= 0)
                pageSize = 100;
            Items = items.Skip((currentPage - 1) * pageSize).Take(pageSize);
            TotalCount = items.Count();
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
        }

        public IEnumerable<T> Items { get; }
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public long TotalCount { get; }
        public bool HasNext => CurrentPage < TotalPages;
        public bool HasPrevious => CurrentPage > 1;
    }
}
