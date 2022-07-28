using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Helpers.BasePager
{
    public class BasePaginationQuery<T> : IRequest<T>
    {
        const int maxPageSize = 100;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public static List<T1> Paginate<T1>(IEnumerable<T1> data, int pageNumber, int pageSize)
        {
            return data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
