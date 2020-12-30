using System;
using System.Collections.Generic;
using System.Text;

namespace Medlatec.Infrastructure.ViewModel
{
    public class SearchResult<T> where T : class
    {
        public List<T> Items { get; set; }
        public int TotalRows { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public SearchResult()
        {
            Code = 1;
        }

        public SearchResult(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public SearchResult(List<T> items, int totalRows = 0, dynamic data = null)
        {
            Code = 1;
            Items = items;
            TotalRows = totalRows;
            Data = data;
        }
    }
}
