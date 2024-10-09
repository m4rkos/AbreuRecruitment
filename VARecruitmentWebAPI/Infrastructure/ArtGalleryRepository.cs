using System.Text.Json;
using VAArtGalleryWebAPI.Domain.Interfaces;
using VAArtGalleryWebAPI.Domain.Entities;
using VAArtGalleryWebAPI.WebApi.Models;

namespace VAArtGalleryWebAPI.Infrastructure
{
    public class ArtGalleryRepository(string filePath) : IArtGalleryRepository
    {
        private readonly string _filePath = filePath;

        public async Task<List<ArtGallery>> GetAllArtGalleriesAsync(string? city, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                using StreamReader sr = new(_filePath);
                string galleriesJson = sr.ReadToEnd();
                var result = JsonSerializer.Deserialize<List<ArtGallery>>(galleriesJson) ?? [];

                if (!string.IsNullOrEmpty(city))
                {
                    return result.Where(a => string.Equals(a.City, city, StringComparison.OrdinalIgnoreCase) ).ToList();
                }

                return result;
            });
        }

        public async Task<ArtGallery?> GetArtGalleryByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var galleries = await GetAllArtGalleriesAsync(null, cancellationToken);
            return galleries.Find(g => g.Id == id);
        }

        public async Task<bool> DeleteArtGalleryByIdAsync(Guid galleryId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var galleries = await new ArtGalleryRepository(_filePath).GetAllArtGalleriesAsync(null, cancellationToken);

            var gallery = galleries.Find(match: g => g.Id == galleryId)
                ?? throw new ArgumentException("unknown gallery id ", nameof(galleryId));

            try
            {
                galleries.Remove(gallery);
                return await WriteDB(galleries);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ArtGallery> CreateAsync(ArtGallery artGallery, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var galleries = await GetAllArtGalleriesAsync(null, cancellationToken);

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

        public async Task<bool> UpdateGallery(Guid idGallery, CreateArtGalleryRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var galleries = await new ArtGalleryRepository(_filePath).GetAllArtGalleriesAsync(null, cancellationToken);

            try
            {
                var gallery = galleries.Find(match: g => g.Id == idGallery)
                    ?? throw new ArgumentException("unknown gallery id ", nameof(idGallery));
                
                var galleryIndex = galleries.IndexOf(gallery);

                gallery.City = request.City;
                gallery.Name = request.Name;
                gallery.Manager = request.Manager;

                galleries[galleryIndex] = gallery;

                return await WriteDB(galleries);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<bool> WriteDB(IEnumerable<ArtGallery>? galleries)
        {
            try
            {
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
    }
}
