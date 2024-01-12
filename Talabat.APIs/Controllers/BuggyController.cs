using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _context.Products.Find(100);
            if(product == null) return NotFound(new ApiResponse(404));
            return Ok();
        }

        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            var product = _context.Products.Find(100);

            var productToReturn = product.ToString(); // Exception (NullReferenceException)

            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id) // Validation Error
        {
            return Ok();
        }
    }

}
