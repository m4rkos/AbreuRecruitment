using MediatR;
using VAArtGalleryWebAPI.Domain.Entities;
using VAArtGalleryWebAPI.WebApi.Models;

namespace VAArtGalleryWebAPI.Application.Queries
{
    public class CreateArtGalleryQuery(CreateArtGalleryRequest data) : IRequest<ArtGallery>
    {
        public string Name { get; set; } = data.Name;

        public string City { get; set; } = data.City;

        public string Manager { get; set; } = data.Manager;
    }
}
