using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.Api.Core.Application.ViewModel
{
    public class PaginationItemModel<TEntity> where TEntity : class
    {


        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int DataCount { get; set; }
        public IEnumerable<TEntity> Items { get; set; }

    }
}
