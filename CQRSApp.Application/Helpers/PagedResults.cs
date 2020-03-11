using System;
using System.Collections.Generic;
using System.Text;

namespace CQRSApp.Application.Helpers
{
    public class PagedResults<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
