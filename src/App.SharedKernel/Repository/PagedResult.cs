using System.Collections;
using System.Collections.Generic;

namespace App.SharedKernel.Repository
{
    public class PagedResult<T> : IEnumerable<T>
    {
        public PagedResult(IEnumerable<T> list, int total, int pageSize, int pageNumber)
        {
            Size = pageSize;
            Total = total;
            Page = list;
            Number = pageNumber;
        }

        public int Size { get; set; }

        public int Number { get; set; }

        public int Total { get; set; }

        public IEnumerable<T> Page { get; set; }

        public int NextNumber { get { return Number + 1; } }
        public int PreviousNumber { get { return Number - 1; } }

        public bool LastPage
        {
            get
            {
                return Size * (Number + 1) >= Total;
            }
        }
        public bool FirstPage
        {
            get
            {
                return Number == 0;
            }
        }

        public int PagesAmount
        {
            get
            {
                return (int)System.Math.Ceiling((double)Total / Size);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Page.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
