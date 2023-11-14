using Domain.CQRS.Catalog.Commands.Request;
using Domain.CQRS.Catalog.Commands.Response;
using MediatR;

namespace Domain.CQRS.Catalog.Handlers.CommandHandlers
{
    public class BrandCommandHandler : IRequestHandler<CreateBrandRequest, CreateBrandResponse>
    {
        public async Task<CreateBrandResponse> Handle(CreateBrandRequest request, CancellationToken cancellationToken)
        {
            return new CreateBrandResponse { BrandID = 1};
        }
    }
}
