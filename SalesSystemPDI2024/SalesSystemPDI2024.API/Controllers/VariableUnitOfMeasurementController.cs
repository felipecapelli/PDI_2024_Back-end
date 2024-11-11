using Microsoft.AspNetCore.Mvc;
using SalesSystemPDI2024.Data.DAL;
using SalesSystemPDI2024.Model;

namespace SalesSystemPDI2024.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VariableUnitOfMeasurementController : Controller
    {
        private readonly ILogger<VariableUnitOfMeasurementController> _logger;

        public VariableUnitOfMeasurementController(ILogger<VariableUnitOfMeasurementController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            VariableUnitOfMeasurementDAL objectDAL = new VariableUnitOfMeasurementDAL();
            return Ok(objectDAL.List());
        }

        [HttpGet("{IDToLookFor}")]
        public IActionResult GetByID(long IDToLookFor)
        {
            VariableUnitOfMeasurementDAL objectDAL = new VariableUnitOfMeasurementDAL();
            VariableUnitOfMeasurement objectModel = objectDAL.RetrieveByID(IDToLookFor);

            if (objectModel == null) return NotFound();

            return Ok(objectModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] VariableUnitOfMeasurement messageBody)
        {
            VariableUnitOfMeasurementDAL objectDAL = new VariableUnitOfMeasurementDAL();
            objectDAL.Add(messageBody);
            return CreatedAtAction(nameof(GetByID),
                new { IDToLookFor = messageBody.VariableUnitOfMeasurementID }, messageBody);
        }

        [HttpPut("{IDToLookFor}")]
        public IActionResult Update(long IDToLookFor, [FromBody] VariableUnitOfMeasurement messageBody)
        {
            VariableUnitOfMeasurementDAL objectDAL = new VariableUnitOfMeasurementDAL();
            VariableUnitOfMeasurement objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Update(messageBody, IDToLookFor);
            return NoContent();
        }

        [HttpDelete("{IDToLookFor}")]
        public IActionResult Delete(long IDToLookFor)
        {
            VariableUnitOfMeasurementDAL objectDAL = new VariableUnitOfMeasurementDAL();
            VariableUnitOfMeasurement objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Delete(objectModel);
            return NoContent();
        }
    }
}
