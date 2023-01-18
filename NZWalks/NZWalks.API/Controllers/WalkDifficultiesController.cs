using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository,IMapper mapper )
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultiesAsync()
        {
            var walkDifficultiesDomain = await walkDifficultyRepository.GetAllAsync();
            var walkDifficultiesDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficultiesDomain);
            return Ok(walkDifficultiesDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName(nameof(GetWalkDifficultyAsync))]
        public async Task<IActionResult> GetWalkDifficultyAsync([FromRoute] Guid id)
        {
            var walkDifficultyDomain = await walkDifficultyRepository.GetAsync(id);
            if(walkDifficultyDomain == null) { return NotFound(); }
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);
            return Ok(walkDifficultyDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync([FromBody] Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if(ValidateAddWalkDifficulty(addWalkDifficultyRequest) == false) return BadRequest(ModelState);
            var newWalkDifficultyDomain = mapper.Map<Models.Domain.WalkDifficulty>(addWalkDifficultyRequest);
            newWalkDifficultyDomain = await walkDifficultyRepository.AddWalkDifficultyAsync(newWalkDifficultyDomain);
            var newWalkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(newWalkDifficultyDomain);
            return CreatedAtAction(nameof(GetWalkDifficultyAsync),new { id = newWalkDifficultyDomain.Id }, newWalkDifficultyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkdifficultyRequest updateWalkdifficultyRequest)
        {
            var updateDifficultyDomain = mapper.Map<Models.Domain.WalkDifficulty>(updateWalkdifficultyRequest);
            updateDifficultyDomain = await walkDifficultyRepository.UpdateWalkDifficultyAsync(id , updateDifficultyDomain);
            if (updateDifficultyDomain == null)
            {
                return NotFound();
            }
            var updateDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(updateDifficultyDomain);
            return Ok(updateDifficultyDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync([FromRoute] Guid id)
        {
            var deletedDifficulty = await walkDifficultyRepository.DeleteWalkDifficultyAsync(id);
            if(deletedDifficulty == null)
            {
                return NotFound();
            }
            var deletedDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(deletedDifficulty);
            return Ok(deletedDifficultyDTO);
        }

        #region private methods
        private bool ValidateAddWalkDifficulty (Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if(addWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest),
                    $"{nameof(addWalkDifficultyRequest)} is required");
            }

            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest.Code),
                    $"{nameof(addWalkDifficultyRequest.Code)} is required.");
            }

            if(ModelState.ErrorCount> 0)
            {
                return false;
            }
            return true;
        }
        private bool ValidateUpdateWalkDifficulty(Models.DTO.UpdateWalkdifficultyRequest updateWalkdifficultyRequest)
        {
            if (updateWalkdifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkdifficultyRequest),
                    $"{nameof(updateWalkdifficultyRequest)} is required");
            }

            if (string.IsNullOrWhiteSpace(updateWalkdifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkdifficultyRequest.Code),
                    $"{nameof(updateWalkdifficultyRequest.Code)} is required.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
