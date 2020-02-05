using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Common;
using Project.Model.Common;
using Project.Repository;
using Project.Service.Common;
using Project.WebAPI.Dtos;
using Project.WebAPI.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class VehicleMakeController : ControllerBase
    {
        private readonly IVehicleMakeService makeService;
        private readonly IMapper mapper;

        public VehicleMakeController(IVehicleMakeService makeService, IMapper mapper)
        {
            this.makeService = makeService;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetMakers([FromQuery]VehicleParams vehicleMakeParams)
        {
            try
            {
                var page = new PagingParams(vehicleMakeParams.PageNumber, vehicleMakeParams.PageSize);
                var filter = new FilteringParams(vehicleMakeParams.Search);
                var sort = new SortingParams(vehicleMakeParams.SortBy);

                var makers = await makeService.FindMakeAsync(filter, sort, page);
                var mapped = mapper.Map<IEnumerable<VehicleMakeDto>>(makers);

                Response.AddPagination(makers.PageNumber, makers.PageSize, makers.TotalItemCount, makers.PageCount);

                return Ok(mapped);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }


        [HttpGet("all")]
        public async Task<IActionResult> GetMakers()
        {
            try
            {
                var makers = await makeService.GetAllMakeAsync();

                var mapped = mapper.Map<IEnumerable<VehicleMakeBaseDto>>(makers);
                return Ok(mapped);
            }
            catch (Exception)
            {
                return NotFound();
            }                
        }


        [HttpGet("{id}", Name = "GetMaker")]
        public async Task<IActionResult> GetMaker(Guid id)
        {
            if(id.Equals(null) || id == Guid.Empty)
                return BadRequest("Maker don't exsist");

            var make = await makeService.GetMakeAsync(id);

            if (make == null)
                return NotFound();

            var mapped = mapper.Map<VehicleMakeDto>(make);
            return Ok(mapped);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, VehicleMakeDto makerUpdate)
        {
            if (id.Equals(null) || id == Guid.Empty)
                return BadRequest("Maker don't exsist");

            var maker = await makeService.GetMakeAsync(id);

            if (maker == null)
                return NotFound();

            var res = await makeService.UpdateMakeAsync(mapper.Map(makerUpdate, maker));

            if (res > 0)
                return NoContent();

            throw new Exception($"Updating maker {id} failed on save");
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id.Equals(null) || id == Guid.Empty)
                return BadRequest("Maker don't exsist");

            var maker = await makeService.GetMakeAsync(id);

            if (maker == null)
                return NotFound();

            if (await makeService.RemoveMakeAsync(id) > 0)
                return Ok();

            return BadRequest("Failed to delete the vehicle maker");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(VehicleMakeDto createVehicleMakeDto)
        {
            var makerToCreate = mapper.Map<IVehicleMake>(createVehicleMakeDto);

            var createdMaker = await makeService.AddMakeAsync(makerToCreate);

            var makerToReturn = mapper.Map<VehicleMakeDto>(await makeService.GetMakeAsync(createdMaker.Id));

            return CreatedAtRoute("GetMaker", new { id = createdMaker.Id }, makerToReturn);
        }
    }
}
