using Microsoft.AspNetCore.Mvc;
using SalesSystemPDI2024.Data.DAL;
using SalesSystemPDI2024.Model;

namespace SalesSystemPDI2024.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShipmentStatusChangeController : Controller
    {
        private readonly ILogger<ShipmentStatusChangeController> _logger;

        public ShipmentStatusChangeController(ILogger<ShipmentStatusChangeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            ShipmentStatusChangeDAL objectDAL = new ShipmentStatusChangeDAL();
            return Ok(objectDAL.List());
        }

        [HttpGet("{ShipmentIDToLookFor, StatusIDToLookFor}")]
        public IActionResult GetByID(long ShipmentIDToLookFor, long StatusIDToLookFor)
        {
            ShipmentStatusChangeDAL objectDAL = new ShipmentStatusChangeDAL();
            ShipmentStatusChange objectModel = objectDAL.RetrieveByID(ShipmentIDToLookFor, StatusIDToLookFor);

            if (objectModel == null) return NotFound();

            return Ok(objectModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] ShipmentStatusChange messageBody)
        {
            ShipmentStatusChangeDAL objectDAL = new ShipmentStatusChangeDAL();
            objectDAL.Add(messageBody);

            long LastIDAdded = objectDAL.GetLastID();
            messageBody.ShipmentID = LastIDAdded;

            return CreatedAtAction(nameof(GetByID),
                new { IDToLookFor = messageBody.ShipmentID }, messageBody);
        }

        [HttpPut("{ShipmentIDToLookFor, StatusIDToLookFor}")]
        public IActionResult Update(long ShipmentIDToLookFor, long StatusIDToLookFor, [FromBody] ShipmentStatusChange messageBody)
        {
            ShipmentStatusChangeDAL objectDAL = new ShipmentStatusChangeDAL();
            ShipmentStatusChange objectModel = objectDAL.RetrieveByID(ShipmentIDToLookFor, StatusIDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Update(messageBody, ShipmentIDToLookFor, StatusIDToLookFor);
            return NoContent();
        }

        [HttpDelete("{ShipmentIDToLookFor, StatusIDToLookFor}")]
        public IActionResult Delete(long ShipmentIDToLookFor, long StatusIDToLookFor)
        {
            ShipmentStatusChangeDAL objectDAL = new ShipmentStatusChangeDAL();
            ShipmentStatusChange objectModel = objectDAL.RetrieveByID(ShipmentIDToLookFor, StatusIDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Delete(objectModel);
            return NoContent();
        }
    }
}
