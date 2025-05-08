using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            
        }



        [HttpGet] 
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();

            //return DTO Region
            /*
            var regionsDTO = new List<Models.DTO.Region>();
            regions.ToList().ForEach(regions =>
            {
                var regionDTO = new Models.DTO.Region()
                {
                    Id = regions.Id,
                    Code = regions.Code,
                    Name = regions.Name,
                    Area = regions.Area,
                    Lat = regions.Lat,
                    Long = regions.Long,
                    Population = regions.Population,
                };

               regionsDTO.Add(regionDTO);
            });*/

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetResultAsync")]
        public async Task<IActionResult> GetResultAsync(Guid id)
        {
            var regions = await regionRepository.GetAsync(id);

            if(regions == null)
            {
                return NotFound(); 
            }

            var regionDTO = mapper.Map<Models.DTO.Region> (regions);

            return Ok(regionDTO);


        }

        [HttpPost]
        public async Task<IActionResult>AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };
            region = await regionRepository.AddAsync(region);

            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            return CreatedAtAction(nameof(GetResultAsync),new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get Region from DataBase
            var region = await regionRepository.DeleteAsync(id);
            //if region is null
            if (region == null)
            {
                return NotFound();
            }

            //Convert response to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            //return Ok response
            return Ok(regionDTO); 
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id,[FromBody]Models.DTO.UpdeteRegionRequest updeteRegionRequest)
        {
            //convert DTO to Domain
            var region = new Models.Domain.Region
            {
                Code = updeteRegionRequest.Code,
                Area = updeteRegionRequest.Area,
                Lat = updeteRegionRequest.Lat,
                Long = updeteRegionRequest.Long,
                Name = updeteRegionRequest.Name,
                Population = updeteRegionRequest.Population
            };
            
            //Update Region using repository
            region = await regionRepository.UpdateAsync(id, region);
            
            //if null
            if(region == null) 
                { 
                return NotFound();
                }

            //convert Domain back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            //return Ok response
            return Ok(regionDTO);
        }
    }
}
