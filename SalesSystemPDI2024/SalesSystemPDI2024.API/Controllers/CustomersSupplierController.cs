using Microsoft.AspNetCore.Mvc;
using SalesSystemPDI2024.Data.DAL;
using SalesSystemPDI2024.Model;

namespace SalesSystemPDI2024.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersSupplierController : Controller
    {
        private readonly ILogger<CustomersSupplierController> _logger;

        public CustomersSupplierController(ILogger<CustomersSupplierController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            CustomersSupplierDAL objectDAL = new CustomersSupplierDAL();
            return Ok(objectDAL.List());
        }

        [HttpGet("{IDToLookFor}")]
        public IActionResult GetByID(long IDToLookFor)
        {
            CustomersSupplierDAL objectDAL = new CustomersSupplierDAL();
            CustomersSupplier objectModel = objectDAL.RetrieveByID(IDToLookFor);

            if (objectModel == null) return NotFound();

            return Ok(objectModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] CustomersSupplier messageBody)
        {
            CustomersSupplierDAL objectDAL = new CustomersSupplierDAL();
            objectDAL.Add(messageBody);
            return CreatedAtAction(nameof(GetByID),
                new { IDToLookFor = messageBody.CustomersSuppliersID }, messageBody);
        }

        [HttpPut("{IDToLookFor}")]
        public IActionResult Update(long IDToLookFor, [FromBody] CustomersSupplier messageBody)
        {
            CustomersSupplierDAL objectDAL = new CustomersSupplierDAL();
            CustomersSupplier objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Update(messageBody, IDToLookFor);
            return NoContent();
        }

        [HttpDelete("{IDToLookFor}")]
        public IActionResult Delete(long IDToLookFor)
        {
            CustomersSupplierDAL objectDAL = new CustomersSupplierDAL();
            CustomersSupplier objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Delete(objectModel);
            return NoContent();
        }
    }
}
