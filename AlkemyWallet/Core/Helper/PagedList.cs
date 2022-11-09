﻿using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Helper
{
    public class PagedList<T>:List<T>
    {

        public int CurrentPages { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => (CurrentPages > 1);

        public bool HasNext => (CurrentPages < TotalPages);

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPages = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            AddRange(items);
        }

        public static PagedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count=source.Count();
            var items=source.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        internal static object Create()
        {
            throw new NotImplementedException();
        }
    }
}
