using Microsoft.AspNetCore.Mvc;
using SalesSystemPDI2024.Data.DAL;
using SalesSystemPDI2024.Model;

namespace SalesSystemPDI2024.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressesListController : Controller
    {
        private readonly ILogger<AddressesListController> _logger;

        public AddressesListController(ILogger<AddressesListController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            AddressesListDAL objectDAL = new AddressesListDAL();
            return Ok(objectDAL.List());
        }

        [HttpGet("{IDToLookFor}")]
        public IActionResult GetByID(long IDToLookFor)
        {
            AddressesListDAL objectDAL = new AddressesListDAL();
            AddressesList objectModel = objectDAL.RetrieveByID(IDToLookFor);

            if (objectModel == null) return NotFound();

            return Ok(objectModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] AddressesList messageBody)
        {
            AddressesListDAL objectDAL = new AddressesListDAL();
            objectDAL.Add(messageBody);

            long LastIDAdded = objectDAL.GetLastID();
            messageBody.AddressesListID = LastIDAdded;

            return CreatedAtAction(nameof(GetByID),
                new { IDToLookFor = messageBody.AddressesListID }, messageBody);
        }

        [HttpPut("{IDToLookFor}")]
        public IActionResult Update(long IDToLookFor, [FromBody] AddressesList messageBody)
        {
            AddressesListDAL objectDAL = new AddressesListDAL();
            AddressesList objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Update(messageBody, IDToLookFor);
            return NoContent();
        }

        [HttpDelete("{IDToLookFor}")]
        public IActionResult Delete(long IDToLookFor)
        {
            AddressesListDAL objectDAL = new AddressesListDAL();
            AddressesList objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Delete(objectModel);
            return NoContent();
        }
    }
}
