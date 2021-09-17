using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestWind.Collections
{
    public class PartialList<T>
    {
        public readonly int TotalCount;
        public List<T> Items { get; set; }
        public PartialList(int totalCount, List<T> items)
        {
            TotalCount = totalCount;
            Items = items;
        }
    }
}
