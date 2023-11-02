using Data.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatalogService.Api.Controllers
{
    [ApiController]
    [Route("api/catalog")]
    public class CatalogController : ControllerBase
    {
        private static readonly string[] Brands = new[]
        {
            "Opel", "Wolkswagen", "Mercedes", "Bmw", "Tofaş", "Fiat", "Kia", "Honda", "Renault", "Mazda"
        };

        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ILogger<CatalogController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("getbrands")]
        public IEnumerable<Brand> GetBrands()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Brand
            {
                CreateDate = DateTime.Now.AddDays(index),
                Id = rng.Next(-20, 55),
                BrandName = Brands[rng.Next(Brands.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("getmodels/{brandId}")]
        public IEnumerable<Model> GetModels(long brandId)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Model
            {
                CreateDate = DateTime.Now.AddDays(index),
                Id = rng.Next(-20, 55),
                ModelName = Brands[rng.Next(Brands.Length)],
                BrandId = index
            })
            .ToArray();
        }

    }
}
