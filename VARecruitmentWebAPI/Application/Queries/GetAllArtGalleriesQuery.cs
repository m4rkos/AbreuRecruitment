using MediatR;
using VAArtGalleryWebAPI.Domain.Entities;

namespace VAArtGalleryWebAPI.Application.Queries
{
    public class GetAllArtGalleriesQuery(string city) : IRequest<List<ArtGallery>>
    {
        public string City { get; set; } = city;
    }
}
