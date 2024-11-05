using Microsoft.AspNetCore.Mvc;
using SalesSystemPDI2024.Data.DAL;
using SalesSystemPDI2024.Model;

namespace SalesSystemPDI2024.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductListItemController : Controller
    {
        private readonly ILogger<AddressController> _logger;

        public ProductListItemController(ILogger<AddressController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAdresses()
        {
            AddressDAL addressDAL = new AddressDAL();
            return Ok(addressDAL.List());
        }

        [HttpGet("{addressIDToLookFor}")]
        public IActionResult GetAdressesByAddressNickName(long addressIDToLookFor)
        {
            AddressDAL addressDAL = new AddressDAL();
            Address addressFromDB = addressDAL.RetrieveByID(addressIDToLookFor);

            if (addressFromDB == null) return NotFound();

            return Ok(addressFromDB);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddAddress([FromBody] Address address)
        {
            AddressDAL addressDAL = new AddressDAL();
            addressDAL.Add(address);
            return CreatedAtAction(nameof(GetAdressesByAddressNickName),
                new { addressIDToLookFor = address.AddressID }, address);
        }

        [HttpPut("{addressIDToLookFor}")]
        public IActionResult UpdateAddress(long addressIDToLookFor, [FromBody] Address address)
        {
            AddressDAL addressDAL = new AddressDAL();
            Address addressFromDB = addressDAL.RetrieveByID(addressIDToLookFor);
            if (addressFromDB == null) return NotFound();

            addressDAL.Update(address, addressIDToLookFor);
            return NoContent();
        }

        [HttpDelete("{addressIDToLookFor}")]
        public IActionResult DeletaFilme(long addressIDToLookFor)
        {
            AddressDAL addressDAL = new AddressDAL();
            Address addressFromDB = addressDAL.RetrieveByID(addressIDToLookFor);
            if (addressFromDB == null) return NotFound();

            addressDAL.Delete(addressFromDB);
            return NoContent();
        }
    }
}
