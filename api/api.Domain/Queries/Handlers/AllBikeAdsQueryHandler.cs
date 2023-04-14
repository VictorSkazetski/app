using api.Data;
using api.Domain.Model.Dto;
using api.Infrastructure.Data.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace api.Domain.Queries.Handlers
{
    public class AllBikeAdsQueryHandler :
        IRequestHandler<AllBikeAdsQuery, List<BikeAdDto>>
    {
        private readonly ICrudBaseRepository<BikeAdEntity, int> bikeAdRepository;
        private readonly IMapper mapper;

        public AllBikeAdsQueryHandler(
            ICrudBaseRepository<BikeAdEntity, int> bikeAdRepository,
            IMapper mapper)
        {
            this.bikeAdRepository = bikeAdRepository;
            this.mapper = mapper;
        }

        public async Task<List<BikeAdDto>> Handle(
            AllBikeAdsQuery request, 
            CancellationToken cancellationToken)
        {
            var bikes = await this.bikeAdRepository.GetAllAsync();

            return bikes.Select(x => this.mapper.Map<BikeAdDto>(x))
                .ToList();
        }
    }
}
