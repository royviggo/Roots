﻿namespace Roots.Business.Filters
{
    public class PersonFilter : PaginationFilter
    {
        public string Name { get; set; }
        public int Gender { get; set; }
        public int Status { get; set; }
    }
}
