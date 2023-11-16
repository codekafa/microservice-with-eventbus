using Domain.CQRS.Catalog.Commands.Response;
using Domain.Dto.CatalogService;
using MediatR;

namespace Domain.CQRS.Catalog.Commands.Request
{
    public class CreateBrandRequest : IRequest<CreateBrandResponse>
    {

        private CreateBrandDto _brand;
        public CreateBrandRequest(CreateBrandDto brandDto)
        {
            _brand = brandDto;
        }

        public CreateBrandDto BrandDto { get { return _brand; } }   
    }
}
