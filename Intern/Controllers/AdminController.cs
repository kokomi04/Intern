using Intern.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Intern.Controllers
{
    [Route("api/admin1.0")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _services;

        public AdminController(IAdminServices services)
        {
            _services = services;
        }

        [HttpGet("getanalysisshopa")]
        public async Task<IActionResult> GetAnalysisData()
        {
            var result = await _services.GetAnalysisData();
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("getallproperty")]
        public async Task<IActionResult> GetAllProperty()
        {
            var result = await _services.GetAllProperty();
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpGet("searchtop5product")]
        public async Task<IActionResult> SearchTop5Product(string? search)
        {
            var result = await _services.SearchTop5Product(search);
            return Ok(result);
        }
        [HttpPost("createbillinshop")]
        public async Task<IActionResult> CreateBillInShop(int idEmployee)
        {
            var result = await _services.CreateBillInShop(idEmployee);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpGet("getallbilldetailofbill")]
        public async Task<IActionResult> GetAllBillDetailOfBill(int idBill)
        {
            var result = await _services.GetAllBillDetailOfBill(idBill);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
