using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoHub.Domain.Util;

public class PagedResult<T>
{
    public List<T> Items { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }


    public PagedResult()
    {
        Items = new List<T>();
        CurrentPage = 1;
        PageSize = 20;
        TotalCount = 0;
        TotalPages = 0;
    }

    public PagedResult(List<T> item)
    {
        Items = item;
        CurrentPage = 1;
        PageSize = 20;
        TotalCount = 0;
        TotalPages = 0;
    }
}
