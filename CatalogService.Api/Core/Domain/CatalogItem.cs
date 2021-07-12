using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.Api.Core.Domain
{
    public class CatalogItem
    {

        public long Id { get; set; }

        public string Name { get; set; }

        public double Pricec { get; set; }

        public CatalogBrand CatalogBrand { get; set; }

        public CatalogType CatalogType { get; set; }
    }
}
