using MediatR;
using VAArtGalleryWebAPI.WebApi.Models;

namespace VAArtGalleryWebAPI.Application.Queries
{
    public class UpdateArtGalleryQuery(Guid idGallery, CreateArtGalleryRequest request) : IRequest<bool>
    {
        public Guid Id { get; set; } = idGallery;
        public CreateArtGalleryRequest CreateArtWorkRequest { get; set; } = request;
    }
}
