using Domain.CQRS.Catalog.Queries.Response;
using MediatR;

namespace Domain.CQRS.Catalog.Queries.Request
{
    public class GetBrandListRequest : IRequest<GetBrandListResponse>
    {
    }
}
