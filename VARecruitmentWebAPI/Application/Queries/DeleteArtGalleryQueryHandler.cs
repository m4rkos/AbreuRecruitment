using MediatR;
using VAArtGalleryWebAPI.Domain.Interfaces;

namespace VAArtGalleryWebAPI.Application.Queries
{
    public class DeleteArtGalleryQueryHandler(IArtGalleryRepository artGalleryRepository) : IRequestHandler<DeleteArtGalleryQuery, bool>
    {
        public async Task<bool> Handle(DeleteArtGalleryQuery request, CancellationToken cancellationToken)
        {
            return await artGalleryRepository.DeleteArtGalleryByIdAsync(request.Id, cancellationToken);
        }
    }
}
