using Microsoft.AspNetCore.Mvc;
using SalesSystemPDI2024.Data.DAL;
using SalesSystemPDI2024.Model;

namespace SalesSystemPDI2024.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyQuotationController : Controller
    {
        private readonly ILogger<CurrencyQuotationController> _logger;

        public CurrencyQuotationController(ILogger<CurrencyQuotationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            CurrencyQuotationDAL objectDAL = new CurrencyQuotationDAL();
            return Ok(objectDAL.List());
        }

        [HttpGet("{IDToLookFor}")]
        public IActionResult GetByID(long IDToLookFor)
        {
            CurrencyQuotationDAL objectDAL = new CurrencyQuotationDAL();
            CurrencyQuotation objectModel = objectDAL.RetrieveByID(IDToLookFor);

            if (objectModel == null) return NotFound();

            return Ok(objectModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] CurrencyQuotation messageBody)
        {
            CurrencyQuotationDAL objectDAL = new CurrencyQuotationDAL();
            objectDAL.Add(messageBody);

            long LastIDAdded = objectDAL.GetLastID();
            messageBody.CurrencyQuotationID = LastIDAdded;

            return CreatedAtAction(nameof(GetByID),
                new { IDToLookFor = messageBody.CurrencyQuotationID }, messageBody);
        }

        [HttpPut("{IDToLookFor}")]
        public IActionResult Update(long IDToLookFor, [FromBody] CurrencyQuotation messageBody)
        {
            CurrencyQuotationDAL objectDAL = new CurrencyQuotationDAL();
            CurrencyQuotation objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Update(messageBody, IDToLookFor);
            return NoContent();
        }

        [HttpDelete("{IDToLookFor}")]
        public IActionResult Delete(long IDToLookFor)
        {
            CurrencyQuotationDAL objectDAL = new CurrencyQuotationDAL();
            CurrencyQuotation objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Delete(objectModel);
            return NoContent();
        }
    }
}
