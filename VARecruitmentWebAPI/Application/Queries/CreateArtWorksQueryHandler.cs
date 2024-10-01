using MediatR;
using VAArtGalleryWebAPI.Domain.Entities;
using VAArtGalleryWebAPI.Domain.Interfaces;

namespace VAArtGalleryWebAPI.Application.Queries
{
    public class CreateArtWorksQueryHandler(IArtWorkRepository artWorkRepository) : IRequestHandler<CreateArtWorksQuery, ArtWork>
    {
        public async Task<ArtWork> Handle(CreateArtWorksQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Null");
            }

            var result = await artWorkRepository.CreateAsync(request.IdCollection, new ArtWork(
                request.Name, request.Author, request.CreationYear, request.AskPrice));

            return result;
        }
    }
}
