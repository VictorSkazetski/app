using api.Domain.Model.Dto;
using MediatR;

namespace api.Domain.Command
{
    public class SellBikeCommand : IRequest<SellBikeDto>
    {
        public SellBikeData BikeData { get; set; }

        public SellBikeCommand(SellBikeData bikeData)
        {
            this.BikeData = bikeData;
        }
    }
}
