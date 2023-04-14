using api.Domain.Model.Dto;
using MediatR;

namespace api.Domain.Queries
{
    public class BikeAdByIdQuery : IRequest<BikeAdDto>
    {
        public int BikeAdId { get; set; }

        public BikeAdByIdQuery(int bikeAdId)
        {
            this.BikeAdId = bikeAdId;
        }
    }
}
