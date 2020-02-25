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
    /// Api endpoints for Family
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyService _familyService;
        private readonly IMapper _mapper;

        public FamilyController(IFamilyService familyService, IMapper mapper)
        {
            _familyService = familyService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of all familys. Filter on name, and paging with page number and limit.
        /// </summary>
        /// <returns>A list of Family models</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FamilyModel>> Get([FromQuery]FamilyQuery query)
        {
            var filter = _mapper.Map<FamilyQuery, FamilyFilter>(query);
            var familys = await _familyService.GetPagedAsync(filter);

            if (familys.Data == null)
                return BadRequest();

            return Ok(_mapper.Map<PagedResponse<IEnumerable<FamilyModel>>>(familys));
        }

        /// <summary>
        /// Gets a specific family.
        /// </summary>
        /// <param name="id">The Family Id</param>
        /// <returns>A Family model</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FamilyModel>> Get(int id)
        {
            var family = await _familyService.GetByIdAsync(id);

            if (family == null)
                return NotFound();

            return Ok(new Response<FamilyModel>(_mapper.Map<FamilyModel>(family)));
        }

        /// <summary>
        /// Delete a family.
        /// </summary>
        /// <param name="id">The Family Id</param>
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
                await _familyService.Delete(request);
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
