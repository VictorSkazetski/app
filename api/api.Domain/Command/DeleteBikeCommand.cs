using MediatR;

namespace api.Domain.Command
{
    public class DeleteBikeCommand : IRequest<bool>
    {
        public int BikeAdId { get; set; }

        public DeleteBikeCommand(int bikeAdId)
        {
            this.BikeAdId = bikeAdId;
        }
    }
}
