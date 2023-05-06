using Intern.Entities;
using Intern.Services;
using Intern.ViewModels.RemakeAdmin;
using Intern.ViewModels.SaleAdmin;
using Intern.ViewModels.Upload;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet("getanalysisshop")]
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
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadRequest request)
        {
            //var json = JsonConvert.SerializeObject(request.data);
            var data = JsonConvert.DeserializeObject<Data> (request.data);
            var result = await _services.Upload(request, data);
            if (result == 0)
                return BadRequest();

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
        [HttpGet("addproduct2billdetail")]
        public async Task<IActionResult> AddProduct2BillDetail(int idProduct, int idBill)
        {
            var result = await _services.AddProduct2BillDetail(idProduct, idBill);
            if (result == 0)
                return BadRequest();

            return Ok(result);
        }
        [HttpGet("updatebilldetailquantity")]
        public async Task<IActionResult> UpdateBillDetailQuantity(int idBillDetail, int quantity)
        {
            var result = await _services.UpdateBillDetailQuantity(idBillDetail, quantity);
            if (result == 0)
                return BadRequest();

            return Ok(result);
        }
        [HttpPost("remakeproduct")]
        public async Task<IActionResult> RemakeProduct(RemakeProduct product)
        {
            var result = await _services.RemakeProduct(product);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }
        [HttpPost("createproperty")]
        public async Task<IActionResult> CreateProperty(CreateAndRemakeProperty request)
        {
            var result = await _services.CreateProperty(request);
            if (result == 0)
                return BadRequest();

            return Ok(result);
        }
        [HttpPut("remakeproperty")]
        public async Task<IActionResult> RemakeProperty (CreateAndRemakeProperty request)
        {
            var result = await _services.RemakeProperty(request);
            if (result == 0)
                return BadRequest();

            return Ok(result);
        }
        [HttpGet("getallbilltype")]
        public async Task<IActionResult> GetAllBillType(int opt)
        {
            var result = await _services.GetAllBillType(opt);

            return Ok(result);
        }
        [HttpGet("sales")]
        public async Task<IActionResult> GetSales()
        {
            var result = await _services.GetSales();

            return Ok(result);
        }
        [HttpPost("createsales")]
        public async Task<IActionResult> CreateSales(CreateSaleRequest request)
        {
            var result = await _services.CreateSales(request);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }
    }
}
