using Microsoft.AspNetCore.Mvc;
using SalesSystemPDI2024.Data.DAL;
using SalesSystemPDI2024.Model;

namespace SalesSystemPDI2024.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NaturalPearsonController : Controller
    {
        private readonly ILogger<NaturalPearsonController> _logger;

        public NaturalPearsonController(ILogger<NaturalPearsonController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            NaturalPearsonDAL objectDAL = new NaturalPearsonDAL();
            return Ok(objectDAL.List());
        }

        [HttpGet("{IDToLookFor}")]
        public IActionResult GetByID(long IDToLookFor)
        {
            NaturalPearsonDAL objectDAL = new NaturalPearsonDAL();
            NaturalPearson objectModel = objectDAL.RetrieveByID(IDToLookFor);

            if (objectModel == null) return NotFound();

            return Ok(objectModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] NaturalPearson messageBody)
        {
            NaturalPearsonDAL objectDAL = new NaturalPearsonDAL();
            objectDAL.Add(messageBody);
            return CreatedAtAction(nameof(GetByID),
                new { IDToLookFor = messageBody.CustomersSuppliersID }, messageBody);
        }

        [HttpPut("{IDToLookFor}")]
        public IActionResult Update(long IDToLookFor, [FromBody] NaturalPearson messageBody)
        {
            NaturalPearsonDAL objectDAL = new NaturalPearsonDAL();
            NaturalPearson objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Update(messageBody, IDToLookFor);
            return NoContent();
        }

        [HttpDelete("{IDToLookFor}")]
        public IActionResult Delete(long IDToLookFor)
        {
            NaturalPearsonDAL objectDAL = new NaturalPearsonDAL();
            NaturalPearson objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Delete(objectModel);
            return NoContent();
        }
    }
}
