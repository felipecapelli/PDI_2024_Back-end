using Microsoft.AspNetCore.Mvc;
using SalesSystemPDI2024.Data.DAL;
using SalesSystemPDI2024.Model;

namespace SalesSystemPDI2024.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseCurrencyController : Controller
    {
        private readonly ILogger<BaseCurrencyController> _logger;

        public BaseCurrencyController(ILogger<BaseCurrencyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            BaseCurrencyDAL objectDAL = new BaseCurrencyDAL();
            return Ok(objectDAL.List());
        }

        [HttpGet("{IDToLookFor}")]
        public IActionResult GetByID(long IDToLookFor)
        {
            BaseCurrencyDAL objectDAL = new BaseCurrencyDAL();
            BaseCurrency objectModel = objectDAL.RetrieveByID(IDToLookFor);

            if (objectModel == null) return NotFound();

            return Ok(objectModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] BaseCurrency messageBody)
        {
            BaseCurrencyDAL objectDAL = new BaseCurrencyDAL();
            objectDAL.Add(messageBody);
            return CreatedAtAction(nameof(GetByID),
                new { IDToLookFor = messageBody.BaseCurrencyID }, messageBody);
        }

        [HttpPut("{IDToLookFor}")]
        public IActionResult Update(long IDToLookFor, [FromBody] BaseCurrency messageBody)
        {
            BaseCurrencyDAL objectDAL = new BaseCurrencyDAL();
            BaseCurrency objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Update(messageBody, IDToLookFor);
            return NoContent();
        }

        [HttpDelete("{IDToLookFor}")]
        public IActionResult Delete(long IDToLookFor)
        {
            BaseCurrencyDAL objectDAL = new BaseCurrencyDAL();
            BaseCurrency objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Delete(objectModel);
            return NoContent();
        }
    }
}
