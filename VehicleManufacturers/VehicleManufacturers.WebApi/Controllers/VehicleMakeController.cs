using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleManufacturers.Model;
using VehicleManufacturers.Service.Common;
using VehicleManufacturers.WebApi.RestModels;

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
            return Ok(_mapper.Map<VehicleMakeRest>(vehicle));
            //return Ok(vehicle);
        }

    }
}
