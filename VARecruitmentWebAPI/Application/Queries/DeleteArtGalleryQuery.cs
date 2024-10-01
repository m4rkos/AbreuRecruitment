using MediatR;

namespace VAArtGalleryWebAPI.Application.Queries
{
    public class DeleteArtGalleryQuery(Guid idGallery) : IRequest<bool>
    {
        public Guid Id { get; set; } = idGallery;
    }
}
