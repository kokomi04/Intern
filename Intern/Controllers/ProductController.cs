using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Intern.Services;
using Intern.ViewModels;

namespace Intern.Controllers
{
    [Route("api/product1.0")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _services;
        public ProductController(IProductServices services)
        {
            _services = services;
        }

        [HttpGet("nextpagea")]
        public async Task<IActionResult> GetProductDetails([FromQuery]int pageIndex = 1)
        {
            var productDetails = await _services.NextPage(pageIndex);
            if (productDetails == null)
                return BadRequest("Danh sach trong");
            return Ok(productDetails);
        }

        [HttpGet("getproducthome")]
        public async Task<IActionResult> GetProductHome()
        {
            var productDetails = await _services.GetProductHome();
            if (productDetails == null)
                return BadRequest("Danh sach trong");
            return Ok(productDetails);
        }
        [HttpGet("checklogin")]
        public async Task<IActionResult> CheckLogin([FromQuery]SignInRequest request)
        {
            var acc = await _services.CheckLogin(request);
            if (acc == null)
                return StatusCode(200);
            return Ok(acc);
        }
    }
}
