using Microsoft.AspNetCore.Mvc;
using SalesSystemPDI2024.Data.DAL;
using SalesSystemPDI2024.Model;

namespace SalesSystemPDI2024.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsListItemStatusChangeController : Controller
    {
        private readonly ILogger<ProductsListItemStatusChangeController> _logger;

        public ProductsListItemStatusChangeController(ILogger<ProductsListItemStatusChangeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            ProductListItemStatusChangeDAL objectDAL = new ProductListItemStatusChangeDAL();
            return Ok(objectDAL.List());
        }

        [HttpGet("{ProductListIDToLookFor, StatusIDToLookFor}")]
        public IActionResult GetByID(long ProductListIDToLookFor, long StatusIDToLookFor)
        {
            ProductListItemStatusChangeDAL objectDAL = new ProductListItemStatusChangeDAL();
            ProductListItemStatusChange objectModel = objectDAL.RetrieveByID(ProductListIDToLookFor, StatusIDToLookFor);

            if (objectModel == null) return NotFound();

            return Ok(objectModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] ProductListItemStatusChange messageBody)
        {
            ProductListItemStatusChangeDAL objectDAL = new ProductListItemStatusChangeDAL();
            objectDAL.Add(messageBody);

            long LastIDAdded = objectDAL.GetLastID();
            messageBody.ProductListID = LastIDAdded;

            return CreatedAtAction(nameof(GetByID),
                new { IDToLookFor = messageBody.ProductListID }, messageBody);
        }

        [HttpPut("{ProductListIDToLookFor, StatusIDToLookFor}")]
        public IActionResult Update(long ProductListIDToLookFor, long StatusIDToLookFor, [FromBody] ProductListItemStatusChange messageBody)
        {
            ProductListItemStatusChangeDAL objectDAL = new ProductListItemStatusChangeDAL();
            ProductListItemStatusChange objectModel = objectDAL.RetrieveByID(ProductListIDToLookFor, StatusIDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Update(messageBody, ProductListIDToLookFor, StatusIDToLookFor);
            return NoContent();
        }

        [HttpDelete("{ProductListIDToLookFor, StatusIDToLookFor}")]
        public IActionResult Delete(long ProductListIDToLookFor, long StatusIDToLookFor)
        {
            ProductListItemStatusChangeDAL objectDAL = new ProductListItemStatusChangeDAL();
            ProductListItemStatusChange objectModel = objectDAL.RetrieveByID(ProductListIDToLookFor, StatusIDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Delete(objectModel);
            return NoContent();
        }
    }
}
