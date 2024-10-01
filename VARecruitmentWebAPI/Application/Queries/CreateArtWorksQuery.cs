using MediatR;
using VAArtGalleryWebAPI.Domain.Entities;
using VAArtGalleryWebAPI.WebApi.Models;

namespace VAArtGalleryWebAPI.Application.Queries
{
    public class CreateArtWorksQuery(CreateArtWorkRequest data, Guid idCollection) : IRequest<ArtWork>
    {
        public Guid IdCollection { get; set; } = idCollection;
        public Guid Id { get; set; }
        public string Name { get; set; } = data.Name;
        public string Author { get; set; } = data.Author;
        public int CreationYear { get; set; } = data.CreationYear;
        public decimal AskPrice { get; set; } = data.AskPrice;
    }
}
