

using Microsoft.AspNetCore.Mvc;
using SalesSystemPDI2024.Data.DAL;
using SalesSystemPDI2024.Model;

namespace SalesSystemPDI2024.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressTypeController : Controller
    {
        private readonly ILogger<AddressTypeController> _logger;

        public AddressTypeController(ILogger<AddressTypeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            AddressTypeDAL objectDAL = new AddressTypeDAL();
            return Ok(objectDAL.List());
        }

        [HttpGet("{IDToLookFor}")]
        public IActionResult GetByID(long IDToLookFor)
        {
            AddressTypeDAL objectDAL = new AddressTypeDAL();
            AddressType objectModel = objectDAL.RetrieveByID(IDToLookFor);

            if (objectModel == null) return NotFound();

            return Ok(objectModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] AddressType messageBody)
        {
            AddressTypeDAL objectDAL = new AddressTypeDAL();
            objectDAL.Add(messageBody);

            long LastIDAdded = objectDAL.GetLastID();
            messageBody.AddressTypeID = LastIDAdded;

            return CreatedAtAction(nameof(GetByID),
                new { IDToLookFor = messageBody.AddressTypeID }, messageBody);
        }

        [HttpPut("{IDToLookFor}")]
        public IActionResult Update(long IDToLookFor, [FromBody] AddressType messageBody)
        {
            AddressTypeDAL objectDAL = new AddressTypeDAL();
            AddressType objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Update(messageBody, IDToLookFor);
            return NoContent();
        }

        [HttpDelete("{IDToLookFor}")]
        public IActionResult Delete(long IDToLookFor)
        {
            AddressTypeDAL objectDAL = new AddressTypeDAL();
            AddressType objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Delete(objectModel);
            return NoContent();
        }
    }
}
