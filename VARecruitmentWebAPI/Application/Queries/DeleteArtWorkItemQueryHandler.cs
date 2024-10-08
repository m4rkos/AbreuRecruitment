using MediatR;
using VAArtGalleryWebAPI.Domain.Interfaces;

namespace VAArtGalleryWebAPI.Application.Queries
{
    public class DeleteArtWorkItemQueryHandler(IArtWorkRepository artWorkRepository) : IRequestHandler<DeleteArtWorkItemQuery, bool>
    {
        public async Task<bool> Handle(DeleteArtWorkItemQuery request, CancellationToken cancellationToken)
        {
            return await artWorkRepository.DeleteAsync(request.IdGallery, request.IdArtWork);
        }
    }
}
