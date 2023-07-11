using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleManufacturers.Common.Filters.VegicleMake;
using VehicleManufacturers.Common.Pagination;
using VehicleManufacturers.Common.Sort;
using VehicleManufacturers.Model;
using VehicleManufacturers.Service.Common;
using VehicleManufacturers.WebApi.RestModels.VehicleMake;

namespace VehicleManufacturers.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakeController : ControllerBase
    {
        private readonly IVehicleMakeService _vehicleMakeService;
        private readonly IMapper _mapper;

        public VehicleMakeController(IVehicleMakeService vehicleMakeService, IMapper mapper)
        {
            _vehicleMakeService = vehicleMakeService;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var vehicle = await _vehicleMakeService.GetByIdAsync(id);
            
            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VehicleMakeRest>(vehicle));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(
            [FromQuery] Sort sort,
            [FromQuery] Pagination pagination,
            [FromQuery] VehicleMakeFilter filter)
        {
            try
            {
                var result = await _vehicleMakeService.GetAsync(sort, pagination, filter);
                return Ok(new { result.Total, Data = _mapper.Map<List<VehicleMakeRest>>(result.Data) });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateVehicleMakeRest createVehicleMakeRest)
        {
            try
            {
                var vehicleMake = await _vehicleMakeService.CreateAsync(_mapper.Map<VehicleMake>(createVehicleMakeRest));
                return StatusCode(201, _mapper.Map<VehicleMakeRest>(vehicleMake));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateVehicleMakeRest vehicleMakeRest)
        {
            try
            {
                var vehicleMake = await _vehicleMakeService.GetByIdAsync(id);
                if (vehicleMake == null)
                {
                    return NotFound();
                }

                vehicleMake = _mapper.Map(vehicleMakeRest, vehicleMake);
                vehicleMake = await _vehicleMakeService.UpdateAsync(vehicleMake);
                return Ok(_mapper.Map<VehicleMakeRest>(vehicleMake));
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
                await _vehicleMakeService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
