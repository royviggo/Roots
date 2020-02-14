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
        public async Task<IActionResult> Get([FromQuery]PlaceQuery query)
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
        public async Task<IActionResult> Get(int id)
        {
            var place = await _placeService.GetByIdAsync(id);

            if (place == null)
                return BadRequest();

            return Ok(new Response<PlaceVm>(_mapper.Map<PlaceVm>(place)));
        }

        /// <summary>
        /// Create a new place.
        /// </summary>
        /// <param name="model">The Place create model</param>
        /// <returns>Inserted id</returns>
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody]PlaceCreateModel model)
        {
            var request = _mapper.Map<PlaceCreateModel, PlaceCreateRequest>(model);

            return await _placeService.Create(request);
        }

        /// <summary>
        /// Updates a place.
        /// </summary>
        /// <param name="model">The Place update model</param>
        /// <returns>Number of records updated</returns>
        [HttpPut]
        public async Task<ActionResult<int>> Update([FromBody]PlaceUpdateModel model)
        {
            var request = _mapper.Map<PlaceUpdateModel, PlaceUpdateRequest>(model);

            return await _placeService.Update(request);
        }

        /// <summary>
        /// Delete a place.
        /// </summary>
        /// <param name="model">The Place delete model</param>
        /// <returns>Number of records deleted</returns>
        [HttpDelete]
        public async Task<ActionResult<int>> Delete([FromBody]DeleteModel model)
        {
            var request = _mapper.Map<DeleteModel, DeleteRequest>(model);

            return await _placeService.Delete(request);
        }
    }
}
