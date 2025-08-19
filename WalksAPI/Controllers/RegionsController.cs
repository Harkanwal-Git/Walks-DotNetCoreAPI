using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models.Domain;
using Walks.API.Models.DTO;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly WalksDBContext _dbContext;
        public RegionsController(WalksDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get All Regions
        [HttpGet]
        public IActionResult GetRegions()
        {

            //var regions=_dbContext.Regions;  //returns IQueryable
            var regions = _dbContext.Regions.ToList();  //loads into memory

            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            return Ok(regionsDto);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetRegionById([FromRoute] Guid? id)
        {
            var region = _dbContext.Regions.FirstOrDefault(r => r.Id == id.Value);

            //return region == null ? NotFound() : Ok(region);

            if (region == null) { NotFound(); }

            RegionDto regionDto = new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };
            return Ok(regionDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // map DTO to domain model
            Region regionDomainModel = new Region
            {
                Id = Guid.NewGuid(),
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            // use domain model to create region
            _dbContext.Regions.Add(regionDomainModel);
            _dbContext.SaveChanges();

            // Map domain model back to DTO
            var regionDTO = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetRegionById), new { id = regionDTO.Id }, regionDTO);

        }
        //Update region
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute]Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Check if region exists
            var regionDomainModel = _dbContext.Regions.Find(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //Map DTO to domain model

            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            _dbContext.SaveChanges();

            //Convert Domain model to DTO
            RegionDto regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }

        //Delete a region

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
           var regionDomainModel= _dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Delete region
            _dbContext.Regions.Remove(regionDomainModel);
            _dbContext.SaveChanges();
            //return Ok();

            //IF YOU WANT TO RETURN THE DELETED OBJECT
            //Convert Domain model to DTO
            RegionDto regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}
