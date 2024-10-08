using MediatR;

namespace VAArtGalleryWebAPI.Application.Queries
{
    public class UpdateArtGalleryQuery(Guid idGallery) : IRequest<bool>
    {
        public Guid Id { get; set; } = idGallery;
    }
}
