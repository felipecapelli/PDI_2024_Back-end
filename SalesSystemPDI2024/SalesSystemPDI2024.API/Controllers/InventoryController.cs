using Microsoft.AspNetCore.Mvc;
using SalesSystemPDI2024.Data.DAL;
using SalesSystemPDI2024.Model;

namespace SalesSystemPDI2024.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : Controller
    {
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            InventoryDAL objectDAL = new InventoryDAL();
            return Ok(objectDAL.List());
        }

        [HttpGet("{IDToLookFor}")]
        public IActionResult GetByID(long IDToLookFor)
        {
            InventoryDAL objectDAL = new InventoryDAL();
            Inventory objectModel = objectDAL.RetrieveByID(IDToLookFor);

            if (objectModel == null) return NotFound();

            return Ok(objectModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] Inventory messageBody)
        {
            InventoryDAL objectDAL = new InventoryDAL();
            objectDAL.Add(messageBody);

            long LastIDAdded = objectDAL.GetLastID();
            messageBody.InventoryID = LastIDAdded;

            return CreatedAtAction(nameof(GetByID),
                new { IDToLookFor = messageBody.InventoryID }, messageBody);
        }

        [HttpPut("{IDToLookFor}")]
        public IActionResult Update(long IDToLookFor, [FromBody] Inventory messageBody)
        {
            InventoryDAL objectDAL = new InventoryDAL();
            Inventory objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Update(messageBody, IDToLookFor);
            return NoContent();
        }

        [HttpDelete("{IDToLookFor}")]
        public IActionResult Delete(long IDToLookFor)
        {
            InventoryDAL objectDAL = new InventoryDAL();
            Inventory objectModel = objectDAL.RetrieveByID(IDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Delete(objectModel);
            return NoContent();
        }
    }
}
