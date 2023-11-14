using Domain.CQRS.Catalog.Queries.Request;
using Domain.CQRS.Catalog.Queries.Response;
using MediatR;

namespace Domain.CQRS.Catalog.Handlers.QueryHandlers
{
    public class BrandQueryHandler : IRequestHandler<GetBrandListRequest, GetBrandListResponse>
    {
        public async Task<GetBrandListResponse> Handle(GetBrandListRequest request, CancellationToken cancellationToken)
        {
            return new GetBrandListResponse { BrandName = "Test"};
        }
    }
}
