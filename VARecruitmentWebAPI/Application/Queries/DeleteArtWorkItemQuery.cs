using MediatR;
using VAArtGalleryWebAPI.Domain.Entities;

namespace VAArtGalleryWebAPI.Application.Queries
{
    public class DeleteArtWorkItemQuery(Guid idGallery, Guid idArtWork) : IRequest<bool>
    {
        public Guid IdGallery { get; set; } = idGallery;
        public Guid IdArtWork { get; set;} = idArtWork;
    }
}
