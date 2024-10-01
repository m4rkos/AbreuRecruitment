using System.Text.Json;
using VAArtGalleryWebAPI.Domain.Interfaces;
using VAArtGalleryWebAPI.Domain.Entities;

namespace VAArtGalleryWebAPI.Infrastructure
{
    public class ArtGalleryRepository(string filePath) : IArtGalleryRepository
    {
        private readonly string _filePath = filePath;

        public async Task<List<ArtGallery>> GetAllArtGalleriesAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                using StreamReader sr = new(_filePath);
                string galleriesJson = sr.ReadToEnd();
                return JsonSerializer.Deserialize<List<ArtGallery>>(galleriesJson) ?? [];
            });
        }

        public async Task<ArtGallery?> GetArtGalleryByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var galleries = await GetAllArtGalleriesAsync(cancellationToken);
            return galleries.Find(g => g.Id == id);
        }

        public async Task<bool> DeleteArtGalleryByIdAsync(Guid galleryId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var galleries = await new ArtGalleryRepository(_filePath).GetAllArtGalleriesAsync(cancellationToken);

            var gallery = galleries.Find(match: g => g.Id == galleryId)
                ?? throw new ArgumentException("unknown gallery id ", nameof(galleryId));

            try
            {
                galleries.Remove(gallery);
                return await Task.Run(() =>
                {
                    using TextWriter tw = new StreamWriter(_filePath, false);
                    tw.Write(JsonSerializer.Serialize(galleries));

                    return true;
                });
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ArtGallery> CreateAsync(ArtGallery artGallery, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var galleries = await GetAllArtGalleriesAsync(cancellationToken);

            Console.WriteLine(artGallery.Name);

            artGallery.Id = Guid.NewGuid();
            galleries.Add(artGallery);

            return await Task.Run(() =>
            {
                using TextWriter tw = new StreamWriter(_filePath, false);
                tw.Write(JsonSerializer.Serialize(galleries));

                return artGallery;
            });

        }
    }
}
