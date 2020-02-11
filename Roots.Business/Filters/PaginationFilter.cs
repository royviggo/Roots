namespace Roots.Business.Filters
{
    public class PaginationFilter
    {
        private static readonly int _maxPageSize = 500;
        private static readonly int _defaultPageSize = 10;
        private int? _pageNumber;
        private int? _pageSize;

        public PaginationFilter()
        {
            SetPageNumber();
            SetPageSize();
        }

        public int? PageNumber
        {
            get { return _pageNumber; }
            set
            {
                _pageNumber = value;
                SetPageNumber();
            }
        }

        public int? PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                SetPageSize();
            }
        }

        public int Skip()
        {

            return ((int)PageNumber - 1) * (int)PageSize;
        }

        public int Take()
        {
            return PageSize ?? _defaultPageSize;
        }

        private void SetPageNumber()
        {
            if (_pageNumber == null || _pageNumber <= 0)
                _pageNumber = 1;
        }

        private void SetPageSize()
        {
            if (_pageSize == null || _pageSize <= 0)
                _pageSize = _defaultPageSize;

            if (_pageSize > _maxPageSize)
                _pageSize = _maxPageSize;
        }
    }
}