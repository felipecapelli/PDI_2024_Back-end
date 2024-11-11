using Microsoft.AspNetCore.Mvc;
using SalesSystemPDI2024.Data.DAL;
using SalesSystemPDI2024.Model;

namespace SalesSystemPDI2024.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsListItemStatusController : Controller
    {
        private readonly ILogger<ProductsListItemStatusController> _logger;

        public ProductsListItemStatusController(ILogger<ProductsListItemStatusController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            ProductsListItemStatusDAL objectDAL = new ProductsListItemStatusDAL();
            return Ok(objectDAL.List());
        }

        [HttpGet("{IDToLookFor}")]
        public IActionResult GetByID(long IDToLookFor)
        {
            ProductsListItemStatusDAL objectDAL = new ProductsListItemStatusDAL();
            ProductsListItemStatus objectModel = objectDAL.RetrieveByID(IDToLookFor);

            if (objectModel == null) return NotFound();

            return Ok(objectModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] ProductsListItemStatus messageBody)
        {
            ProductsListItemStatusDAL objectDAL = new ProductsListItemStatusDAL();
            objectDAL.Add(messageBody);
            return CreatedAtAction(nameof(GetByID),
                new { IDToLookFor = messageBody.StatusID }, messageBody);
        }

        [HttpPut("{IDToLookFor}")]
        public IActionResult Update(long IDToLookFor, [FromBody] ProductsListItemStatus messageBody)
        {
            ProductsListItemStatusDAL objectDAL = new ProductsListItemStatusDAL();
            ProductsListItemStatus objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Update(messageBody, IDToLookFor);
            return NoContent();
        }

        [HttpDelete("{IDToLookFor}")]
        public IActionResult Delete(long IDToLookFor)
        {
            ProductsListItemStatusDAL objectDAL = new ProductsListItemStatusDAL();
            ProductsListItemStatus objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Delete(objectModel);
            return NoContent();
        }
    }
}
