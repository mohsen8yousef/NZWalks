using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.repositories;
using System.Formats.Asn1;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalksRepository walksRepository;
        private readonly IMapper mapper;

        public WalksController(IWalksRepository walksRepository,IMapper mapper)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllWalksAsync()
        {
            //Fetch data from database - domain
            var walks = await walksRepository.GetAllAsync();

            //convert domain walks to DTO
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walks);
            
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walk = await walksRepository.GetAsync(id);

            if(walk == null) {return NotFound();}  

            var walksDTO = mapper.Map<Models.DTO.Walk>(walk);

            return Ok(walksDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            var walk = new Models.Domain.Walk
            {
                length = addWalkRequest.length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDefficultyId = addWalkRequest.WalkDefficultyId
            };

            walk = await walksRepository.AddWalkAsync(walk);

            var walkDTO = new Models.DTO.Walk
            {
                Id = walk.Id,
                length = addWalkRequest.length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDefficultyId = addWalkRequest.WalkDefficultyId
            };

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id,
            [FromBody] Models.DTO.AddWalkRequest UpdateWalkRequest)
        {
            var walks = new Models.Domain.Walk
            {
                length = UpdateWalkRequest.length,
                Name = UpdateWalkRequest.Name,
                RegionId = UpdateWalkRequest.RegionId,
                WalkDefficultyId = UpdateWalkRequest.WalkDefficultyId
            };
            
            walks = await walksRepository.UpdateWalkAsync(id , walks);

            if(walks == null)
            return NotFound();

            var walkDTO =   mapper.Map<Models.DTO.Walk>(walks);

            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walk = await walksRepository.DeleteWalkAsync(id);
            if(walk == null) return NotFound();
            var walkDTO = mapper.Map<Models.Domain.Walk>(walk);
            return Ok(walkDTO);
        }
    }
}
