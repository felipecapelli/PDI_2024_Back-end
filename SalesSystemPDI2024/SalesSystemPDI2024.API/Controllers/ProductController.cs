using Microsoft.AspNetCore.Mvc;
using SalesSystemPDI2024.Data.DAL;
using SalesSystemPDI2024.Model;

namespace SalesSystemPDI2024.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            ProductDAL objectDAL = new ProductDAL();
            return Ok(objectDAL.List());
        }

        [HttpGet("{IDToLookFor}")]
        public IActionResult GetByID(long IDToLookFor)
        {
            ProductDAL objectDAL = new ProductDAL();
            Product objectModel = objectDAL.RetrieveByID(IDToLookFor);

            if (objectModel == null) return NotFound();

            return Ok(objectModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] Product messageBody)
        {
            ProductDAL objectDAL = new ProductDAL();
            objectDAL.Add(messageBody);

            long LastIDAdded = objectDAL.GetLastID();
            messageBody.ProductID = LastIDAdded;

            return CreatedAtAction(nameof(GetByID),
                new { IDToLookFor = messageBody.ProductID }, messageBody);
        }

        [HttpPut("{IDToLookFor}")]
        public IActionResult Update(long IDToLookFor, [FromBody] Product messageBody)
        {
            ProductDAL objectDAL = new ProductDAL();
            Product objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Update(messageBody, IDToLookFor);
            return NoContent();
        }

        [HttpDelete("{IDToLookFor}")]
        public IActionResult Delete(long IDToLookFor)
        {
            ProductDAL objectDAL = new ProductDAL();
            Product objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Delete(objectModel);
            return NoContent();
        }
    }
}
