using Microsoft.AspNetCore.Mvc;
using SalesSystemPDI2024.Data.DAL;
using SalesSystemPDI2024.Model;

namespace SalesSystemPDI2024.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderStatusChangeController : Controller
    {
        private readonly ILogger<OrderStatusChangeController> _logger;

        public OrderStatusChangeController(ILogger<OrderStatusChangeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            OrderStatusChangeDAL objectDAL = new OrderStatusChangeDAL();
            return Ok(objectDAL.List());
        }

        [HttpGet("{OrderIDToLookFor, StatusIDToLookFor}")]
        public IActionResult GetByID(long OrderIDToLookFor, long StatusIDToLookFor)
        {
            OrderStatusChangeDAL objectDAL = new OrderStatusChangeDAL();
            OrderStatusChange objectModel = objectDAL.RetrieveByID(OrderIDToLookFor, StatusIDToLookFor);

            if (objectModel == null) return NotFound();

            return Ok(objectModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] OrderStatusChange messageBody)
        {
            OrderStatusChangeDAL objectDAL = new OrderStatusChangeDAL();
            objectDAL.Add(messageBody);

            long LastIDAdded = objectDAL.GetLastID();
            messageBody.StatusID = LastIDAdded;

            return CreatedAtAction(nameof(GetByID),
                new { IDToLookFor = messageBody.StatusID }, messageBody);
        }

        [HttpPut("{IDToLOrderIDToLookFor, StatusIDToLookForookFor}")]
        public IActionResult Update(long OrderIDToLookFor, long StatusIDToLookFor, [FromBody] OrderStatusChange messageBody)
        {
            OrderStatusChangeDAL objectDAL = new OrderStatusChangeDAL();
            OrderStatusChange objectModel = objectDAL.RetrieveByID(OrderIDToLookFor, StatusIDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Update(messageBody, OrderIDToLookFor, StatusIDToLookFor);
            return NoContent();
        }

        [HttpDelete("{IDTOrderIDToLookFor, StatusIDToLookForoLookFor}")]
        public IActionResult Delete(long OrderIDToLookFor, long StatusIDToLookFor)
        {
            OrderStatusChangeDAL objectDAL = new OrderStatusChangeDAL();
            OrderStatusChange objectModel = objectDAL.RetrieveByID(OrderIDToLookFor, StatusIDToLookFor);
            if (objectModel == null) return NotFound();

            objectDAL.Delete(objectModel);
            return NoContent();
        }
    }
}
