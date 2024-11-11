using Microsoft.AspNetCore.Mvc;
using SalesSystemPDI2024.Data.DAL;
using SalesSystemPDI2024.Model;

namespace SalesSystemPDI2024.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseUnitOfMeasurementController : Controller
    {
        private readonly ILogger<BaseUnitOfMeasurementController> _logger;

        public BaseUnitOfMeasurementController(ILogger<BaseUnitOfMeasurementController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            BaseUnitOfMeasurementDAL objectDAL = new BaseUnitOfMeasurementDAL();
            return Ok(objectDAL.List());
        }

        [HttpGet("{IDToLookFor}")]
        public IActionResult GetByID(long IDToLookFor)
        {
            BaseUnitOfMeasurementDAL objectDAL = new BaseUnitOfMeasurementDAL();
            BaseUnitOfMeasurement objectModel = objectDAL.RetrieveByID(IDToLookFor);

            if (objectModel == null) return NotFound();

            return Ok(objectModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] BaseUnitOfMeasurement messageBody)
        {
            BaseUnitOfMeasurementDAL objectDAL = new BaseUnitOfMeasurementDAL();
            objectDAL.Add(messageBody);
            return CreatedAtAction(nameof(GetByID),
                new { IDToLookFor = messageBody.BaseUnitOfMeasurementID }, messageBody);
        }

        [HttpPut("{IDToLookFor}")]
        public IActionResult Update(long IDToLookFor, [FromBody] BaseUnitOfMeasurement messageBody)
        {
            BaseUnitOfMeasurementDAL objectDAL = new BaseUnitOfMeasurementDAL();
            BaseUnitOfMeasurement objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Update(messageBody, IDToLookFor);
            return NoContent();
        }

        [HttpDelete("{IDToLookFor}")]
        public IActionResult Delete(long IDToLookFor)
        {
            BaseUnitOfMeasurementDAL objectDAL = new BaseUnitOfMeasurementDAL();
            BaseUnitOfMeasurement objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Delete(objectModel);
            return NoContent();
        }
    }
}
