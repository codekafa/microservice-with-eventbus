using AutoMapper;
using Data.Domain;
using Domain.CQRS.Catalog.Commands.Request;
using Domain.CQRS.Catalog.Commands.Response;
using MediatR;
using Repositories.Base;

namespace Domain.CQRS.Catalog.Handlers.CommandHandlers
{
    public class BrandCommandHandler : IRequestHandler<CreateBrandRequest, CreateBrandResponse>
    {
        BaseRepository<Brand> _brandRepository;
        private readonly IMapper _mapper;

        public BrandCommandHandler(BaseRepository<Brand> brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public async Task<CreateBrandResponse> Handle(CreateBrandRequest request, CancellationToken cancellationToken)
        {
            var brand = _mapper.Map<Brand>(request.BrandDto);
            var result = _brandRepository.Insert(brand);
            return new CreateBrandResponse { BrandId = result.Id };
        }
    }
}
