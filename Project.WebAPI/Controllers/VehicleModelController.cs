using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Common;
using Project.Model.Common;
using Project.Service.Common;
using Project.WebAPI.Dtos;
using Project.WebAPI.Helpers;
using X.PagedList;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleModelController : ControllerBase
    {
        private readonly IVehicleModelService modelService;
        private readonly IMapper mapper;

        public VehicleModelController(IVehicleModelService modelService, IMapper mapper)
        {
            this.modelService = modelService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetModels([FromQuery]VehicleParams vehicleModelParams)
        {
            var page = new PagingParams(vehicleModelParams.PageNumber, vehicleModelParams.PageSize);
            var filter = new FilteringParams(vehicleModelParams.Search, vehicleModelParams.MakeId?? Guid.Empty);
            var sort = new SortingParams(vehicleModelParams.SortBy);

            var models = await modelService.FindModelAsync(filter, sort, page);
            var modelsDto = models.ToMappedPagedList<IVehicleModel, VehicleModelDto>(mapper);

            Response.AddPagination(models.PageNumber, models.PageSize, models.TotalItemCount, models.PageCount);

            return Ok(modelsDto);
        }


        [HttpGet("{id}", Name = "GetModel")]
        public async Task<IActionResult> GetModel(Guid id)
        {
            if (id.Equals(null) || id == Guid.Empty)
                return BadRequest("Model don't exsist");

            var model = await modelService.GetModelAsync(id);

            if (model == null)
                return NotFound();

            var mapped = mapper.Map<VehicleModelDto>(model);
            return Ok(mapped);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, VehicleModelDto modelUpdate)
        {
            if (id.Equals(null) || id == Guid.Empty)
                return BadRequest("Model don't exsist");

            var model = await modelService.GetModelAsync(id);

            if (model == null)
                return NotFound();

            var res = await modelService.UpdateModelAsync(mapper.Map(modelUpdate, model));

            if (res > 0)
                return NoContent();

            throw new Exception($"Updating model {id} failed on save");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id.Equals(null) || id == Guid.Empty)
                return BadRequest("Model don't exsist");

            var model = await modelService.GetModelAsync(id);

            if (model == null)
                return NotFound();

            if (await modelService.RemoveModelAsync(id) > 0)
                return Ok();

            return BadRequest("Failed to delete the vehicle model");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(VehicleModelDto createVehicleModelDto)
        {
            var modelToCreate = mapper.Map<Model.VehicleModel>(createVehicleModelDto);

            var createdModel = await modelService.AddModelAsync(modelToCreate);

            var modelToReturn = mapper.Map<VehicleModelDto>(await modelService.GetModelAsync(createdModel.Id));

            return CreatedAtRoute("GetModel", new { id = createdModel.Id }, modelToReturn);
        }
    }
}
