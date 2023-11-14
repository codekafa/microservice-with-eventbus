using Domain.CQRS.Catalog.Commands.Response;
using MediatR;

namespace Domain.CQRS.Catalog.Commands.Request
{
    public class CreateBrandRequest : IRequest<CreateBrandResponse>
    {

        private string _brandName;
        public CreateBrandRequest(string brandName)
        {
            _brandName = brandName;
        }
    }
}
