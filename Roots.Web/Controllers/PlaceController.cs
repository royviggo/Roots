using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roots.Business.Filters;
using Roots.Business.Interfaces;
using Roots.Business.Requests;
using Roots.Web.Models;
using Roots.Web.InputModels;
using Roots.Web.Queries;
using Roots.Web.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Roots.Business.Responses;
using Roots.Business.Exceptions;

namespace Roots.Web.Controllers
{
    /// <summary>
    /// Api endpoints for Place
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService _placeService;
        private readonly IMapper _mapper;

        public PlaceController(IPlaceService placeService, IMapper mapper)
        {
            _placeService = placeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of all places. Filter on name, and paging with page number and limit.
        /// </summary>
        /// <returns>A list of Place models</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlaceVm>> Get([FromQuery]PlaceQuery query)
        {
            var filter = _mapper.Map<PlaceQuery, PlaceFilter>(query);
            var places = await _placeService.GetPagedAsync(filter);

            if (places.Data == null)
                return BadRequest();

            return Ok(_mapper.Map<PagedResponse<IEnumerable<PlaceVm>>>(places));
        }

        /// <summary>
        /// Gets a specific place.
        /// </summary>
        /// <param name="id">The Place Id</param>
        /// <returns>A Place model</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlaceVm>> Get(int id)
        {
            var place = await _placeService.GetByIdAsync(id);

            if (place == null)
                return NotFound();

            return Ok(new Response<PlaceVm>(_mapper.Map<PlaceVm>(place)));
        }

        /// <summary>
        /// Create a new place.
        /// </summary>
        /// <param name="model">The Place create model</param>
        /// <returns>Inserted id</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreatedModel>> Create([FromBody]PlaceCreateModel model)
        {
            var request = _mapper.Map<PlaceCreateModel, PlaceCreateRequest>(model);

            if (request == null)
                return BadRequest();

            try
            {
                var id = await _placeService.Create(request);
                return Created(nameof(Get), new CreatedModel { Id = id });
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates a place.
        /// </summary>
        /// <param name="id">The Place Id</param>
        /// <param name="model">The Place update model</param>
        /// <returns>Number of records updated</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody]PlaceUpdateModel model)
        {
            var request = _mapper.Map<PlaceUpdateModel, PlaceUpdateRequest>(model);

            if (request == null || id != request.Id)
                return BadRequest();

            try
            {
                await _placeService.Update(request);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete a place.
        /// </summary>
        /// <param name="id">The Place Id</param>
        /// <returns>Number of records deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteRequest { Id = id };

            try
            {
                await _placeService.Delete(request);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
