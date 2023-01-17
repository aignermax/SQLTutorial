using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Identity.Client;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            // fetch Data from database -domain walks
            var walksDomainData = await walkRepository.GetAllAsync();
            // convert domain walks to dto walks
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walksDomainData);

            // return response
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName(nameof(GetWalkAsync))]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walkDomainData = await walkRepository.GetAsync(id);
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomainData);
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            // convert DTO to Domain object
            var walkDomain = mapper.Map<Models.Domain.Walk>(addWalkRequest);
            // pass domain obj to repository to persist 
            walkDomain = await walkRepository.AddAsync(walkDomain);
            // convert domain object back to dto
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);
            // send dto back to client
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDomain.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            var walkDomain = mapper.Map<Models.Domain.Walk>(updateWalkRequest);
            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);
            if (walkDomain == null)
            {
                return NotFound();
            }
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);
            return Ok(walkDTO);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walkDomain = await walkRepository.DeleteAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);
            return Ok(walkDTO); 
        }
    }
}
