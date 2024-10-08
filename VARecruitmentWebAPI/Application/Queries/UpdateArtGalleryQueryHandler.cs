using MediatR;

namespace VAArtGalleryWebAPI.Application.Queries
{
    public class UpdateArtGalleryQueryHandler() : IRequestHandler<UpdateArtGalleryQuery, bool>
    {
        public Task<bool> Handle(UpdateArtGalleryQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
