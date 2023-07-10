using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleManufacturers.Common.Filters.VehicleModel;
using VehicleManufacturers.Common.Pagination;
using VehicleManufacturers.Common.Sort;
using VehicleManufacturers.Model;
using VehicleManufacturers.Service.Common;
using VehicleManufacturers.WebApi.RestModels.VehicleModel;

namespace VehicleManufacturers.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleModelController : ControllerBase
    {
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IMapper _mapper;

        public VehicleModelController(IVehicleModelService vehicleModelService, IMapper mapper)
        {
            _vehicleModelService = vehicleModelService;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var vehicleModel = await _vehicleModelService.GetByIdAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VehicleModelRest>(vehicleModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(
            [FromQuery] Sort sort,
            [FromQuery] Pagination pagination,
            [FromQuery] VehicleModelFilter filter)
        {
            try
            {
                var result = await _vehicleModelService.GetAsync(sort, pagination, filter);
                return Ok(new { result.Total, Data = _mapper.Map<List<VehicleModelRest>>(result.Data) });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateVehicleModelRest createVehicleModelRest)
        {
            try
            {
                var vehicleModel = await _vehicleModelService.CreateAsync(_mapper.Map<VehicleModel>(createVehicleModelRest));
                return StatusCode(201, _mapper.Map<VehicleModelRest>(vehicleModel));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateVehicleModelRest vehicleModelRest)
        {
            try
            {
                var vehicleModel = await _vehicleModelService.GetByIdAsync(id);
                if (vehicleModel == null)
                {
                    return NotFound();
                }

                vehicleModel = _mapper.Map(vehicleModelRest, vehicleModel);
                vehicleModel = await _vehicleModelService.UpdateAsync(vehicleModel);
                return Ok(_mapper.Map<VehicleModelRest>(vehicleModel));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _vehicleModelService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
