using GoodRoad.Data.Repository.IRepository;
using GoodRoad.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoodRoad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly IAddressRepository _addressRepository;
        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Address>))]
        public IActionResult GetAddresses()
        {
            var address = _addressRepository.GetAddresses();
            if(ModelState.IsValid)
            {
                return Ok(address);
            }
            return BadRequest();
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Address))]
        public IActionResult GetAddress(int id)
        {
            var address = _addressRepository.GetAddress(id);
            if(ModelState.IsValid)
            {
                return Ok(address);
            }
            return BadRequest();
        }
    }
}
