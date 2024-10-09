using MediatR;
using Microsoft.AspNetCore.Mvc;
using VAArtGalleryWebAPI.Application.Queries;
using VAArtGalleryWebAPI.Domain.Entities;
using VAArtGalleryWebAPI.WebApi.Models;

namespace VAArtGalleryWebAPI.WebApi.Controllers
{
    [Route("api/art-galleries")]
    [ApiController]
    public class ArtGalleryController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Obtem todos registos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetAllArtGalleriesResult>>> GetAllGalleries()
        {
            var galleries = await mediator.Send(new GetAllArtGalleriesQuery(string.Empty));

            var result = galleries.Select(g => new GetAllArtGalleriesResult(g.Id, g.Name, g.City, g.Manager, g.ArtWorksOnDisplay?.Count ?? 0)).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Obtem todos registos e subitems
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-data")]
        public async Task<ActionResult<List<GetAllArtGalleriesAndArtWorksOnDisplay>>> GetAllGallerieAndData([FromQuery] string? city)
        {
            var galleries = await mediator.Send(new GetAllArtGalleriesQuery(!string.IsNullOrEmpty(city) ? city : string.Empty));

            var result = galleries.Select(g => g).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Obtem item por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<List<GetAllArtGalleriesAndArtWorksOnDisplay>>> GetGalleryById([FromRoute] string id)
        {
            var galleries = await mediator.Send(new GetAllArtGalleriesQuery(string.Empty));

            var result = galleries.Select(g => g).Where(i => i.Id == Guid.Parse(id) ).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Inseri um novo registo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CreateArtGalleryResult>> Create([FromBody] CreateArtGalleryRequest request)
        {
            var result = await mediator.Send(new CreateArtGalleryQuery(request));
            
            return Ok(result);
        }

        /// <summary>
        /// Inseri um ArtWork item
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<ActionResult<CreateArtGalleryResult>> CreateArtWorksOnDisplay([FromBody] CreateArtWorkRequest request, [FromRoute] string id)
        {
            var result = await mediator.Send(new CreateArtWorksQuery(request, Guid.Parse(id) ));

            return Ok(result);
        }

        /// <summary>
        /// Detela um registo usando o identificador "ID"
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CreateArtGalleryResult>> Delete(string id)
        {
            var result = await mediator.Send(new DeleteArtGalleryQuery(Guid.Parse(id)));

            return result == true
                ? Ok($"Gallery Removido com sucesso, id: {id}")
                : throw new ArgumentException($"Erro ao tentar deletar o id: {id}");
        }

        /// <summary>
        /// Detela um registo usando o identificador "ID" da gallery e do item art work
        /// </summary>
        /// <param name="idGallery"></param>
        /// <param name="idArtWork"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpDelete("{idGallery}/{idArtWork}")]
        public async Task<ActionResult<CreateArtGalleryResult>> DeleteItem(string idGallery, string idArtWork)
        {
            var result = await mediator.Send(new DeleteArtWorkItemQuery(Guid.Parse(idGallery), Guid.Parse(idArtWork) ));
            
            return result == true 
                ? Ok($"Item Removido com sucesso, id: {idArtWork}") 
                : throw new ArgumentException($"Erro ao tentar deletar o id: {idArtWork}");
        }

        /// <summary>
        /// Atualiza um registo usando o identificador "ID"
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<CreateArtGalleryResult>> UpdateArtgallery([FromBody] CreateArtGalleryRequest request, [FromRoute] string id)
        {
            var result = await mediator.Send(new UpdateArtGalleryQuery(Guid.Parse(id), request));

            return result == true
                ? Ok($"ArtWorks Atualizado com sucesso, id: {id}")
                : throw new ArgumentException($"Erro ao tentar atualizar o id: {id}");
        }

        ///// <summary>
        ///// Atualiza um registo usando o identificador "ID" da gallery e do item art work
        ///// </summary>
        ///// <param name="idGallery"></param>
        ///// <param name="idArtWork"></param>
        ///// <returns></returns>
        ///// <exception cref="ArgumentException"></exception>
        //[HttpPut("{idGallery}/item/{idArtWork}")]
        //public async Task<ActionResult<CreateArtGalleryResult>> UpdateItem(string idGallery, string idArtWork)
        //{
        //    var result = await mediator.Send(new UpdateArtWorkItemQuery(Guid.Parse(idGallery), Guid.Parse(idArtWork)));

        //    return result == true
        //        ? Ok($"Item Atualizado com sucesso, id: {idArtWork}")
        //        : throw new ArgumentException($"Erro ao tentar atualizar o id: {idArtWork}");
        //}
    }
}
