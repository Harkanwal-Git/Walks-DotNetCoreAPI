using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Walks.API.Data;
using Walks.API.Models.Domain;
using Walks.API.Models.DTO;
using Walks.API.Repositories;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]             //This by default does model state validation check. If not present then use ModelState.IsValid
    
    public class RegionsController : ControllerBase
    {
        private readonly WalksDBContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(WalksDBContext dbContext,IRegionRepository regionRepository,IMapper mapper,
            ILogger<RegionsController> logger)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
            _mapper = mapper;
            this._logger = logger;
        }

        //Get All Regions
        [HttpGet]
        //[Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetRegions()
        {
            try
            {
                throw new Exception("This is a custom exception");
                _logger.LogInformation("GetAllRegions Action Method was invoked");
                _logger.LogError("THis is a test Error log");
                //var regions=_dbContext.Regions;  //returns IQueryable
                //var regions = await _dbContext.Regions.ToListAsync();  //loads into memory

                var regions = await _regionRepository.GetAllAsync();


                var regionsDto = _mapper.Map<List<RegionDto>>(regions);
                _logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regionsDto)}");

                //foreach (var region in regions)
                //{
                //    regionsDto.Add(new RegionDto
                //    {
                //        Id = region.Id,
                //        Code = region.Code,
                //        Name = region.Name,
                //        RegionImageUrl = region.RegionImageUrl
                //    });
                //}


                return Ok(regionsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                throw;
            }
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            // var region = await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id.Value);
            var region = await _regionRepository.GetRegionByIdAsync(id);

            //return region == null ? NotFound() : Ok(region);

            if (region == null) {return NotFound(); }


            //RegionDto regionDto = new RegionDto
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            return Ok(_mapper.Map<RegionDto>(region));
        }

        [HttpPost]
  
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            if (ModelState.IsValid)
            {
                // map DTO to domain model
                //Region regionDomainModel = new Region
                //{
                //    //Id = Guid.NewGuid(),
                //    Code = addRegionRequestDto.Code,
                //    Name = addRegionRequestDto.Name,
                //    RegionImageUrl = addRegionRequestDto.RegionImageUrl
                //};
                Region regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);
                //// use domain model to create region
                //await _dbContext.Regions.AddAsync(regionDomainModel);
                //await _dbContext.SaveChangesAsync();

                regionDomainModel = await _regionRepository.CreateRegionAsync(regionDomainModel);

                // Map domain model back to DTO
                //var regionDTO = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl
                //};
                var regionDTO = _mapper.Map<RegionDto>(regionDomainModel);
                return CreatedAtAction(nameof(GetRegionById), new { id = regionDTO.Id }, regionDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
        //Update region
        [HttpPut("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // //Check if region exists
            // var regionDomainModel = await _dbContext.Regions.FindAsync(id);

            // if (regionDomainModel == null)
            // {
            //     return NotFound();
            // }
            // //Map DTO to domain model
            var regionDomainModel =_mapper.Map<Region>(updateRegionRequestDto);
            //regionDomainModel.Code = updateRegionRequestDto.Code;
            //regionDomainModel.Name = updateRegionRequestDto.Name;
            //regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            //await _dbContext.SaveChangesAsync();

            regionDomainModel=await _regionRepository.UpdateRegionAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //Convert Domain model to DTO
            //RegionDto regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            

            return Ok(_mapper.Map<RegionDto>(regionDomainModel));
        }

        //Delete a region

        [HttpDelete("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var regionDomainModel= await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            // if (regionDomainModel == null)
            // {
            //     return NotFound();
            // }

            // //Delete region
            // _dbContext.Regions.Remove(regionDomainModel);
            // _dbContext.SaveChanges();
            var regionDomainModel = await _regionRepository.DeleteRegionAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // //return Ok();

            // //IF YOU WANT TO RETURN THE DELETED OBJECT
            // //Convert Domain model to DTO
            //RegionDto regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            return Ok(_mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
