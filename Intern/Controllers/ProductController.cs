﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Intern.Services;
using Intern.Entities;
using Intern.ViewModels.Authen;
using Intern.ViewModels.ChangeAccount;
using Intern.ViewModels;
using Azure.Core;
using Intern.ViewModels.Order;

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

        [HttpGet("nextpage")]
        public async Task<IActionResult> GetProductDetails([FromQuery]int pageIndex)
        {
            var products = await _services.NextPage(pageIndex);
            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("getproducthome")]
        public async Task<IActionResult> GetProductHome()
        {
            var productDetails = await _services.GetProductHome();
            if (productDetails == null)
                return NotFound();
            return Ok(productDetails);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchProduct(string search)
        {
            var products = await _services.SearchProduct(search);
            if (products == null)
                return NotFound();

            return Ok(products);
        }
        [HttpGet("getproductid")]
        public async Task<IActionResult> GetProductId(int Id)
        {
            var productdetail = await _services.GetProductId(Id);
            if (productdetail == null)
                return BadRequest();

            return Ok(productdetail);
        }
        [HttpGet("dress")]
        public async Task<IActionResult> DressCategory()
        {
            var products = await _services.DressCategory();
            if (products == null)
                return NotFound();

            return Ok(products);
        }
        [HttpGet("pan")]
        public async Task<IActionResult> PanCategory()
        {
            var products = await _services.PanCategory();
            if (products == null)
                return NotFound();

            return Ok(products);
        }
        [HttpGet("shirt")]
        public async Task<IActionResult> ShirtCategory()
        {
            var products = await _services.ShirtCategory();
            if (products == null)
                return NotFound();

            return Ok(products);
        }
        [HttpPost("addproduct2bag")]
        public async Task<IActionResult> AddProductToBag(AddProToBagRequest request)
        {
            await _services.AddProductToBag(request);
            return Ok();
        }
        [HttpGet("getproductbagbyaccountid")]
        public async Task<IActionResult> GetProductBagByAccountId(int accountId)
        {
            var result = await _services.GetProductBagByAccountId(accountId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpDelete("deleteaccountbag")]
        public async Task<IActionResult> DeleteBag(int accountBagId)
        {
            var result = await _services.DeleteBag(accountBagId);
            if (result == 0)
                return BadRequest();
            return Ok();
        }
        [HttpPut("updateaccountbagbyid")]
        public async Task<IActionResult> UpdateAccountBagById(int[] request)
        {
            var result = await _services.UpdateAccountBagById(request);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }
        [HttpGet("checklogin")]
        public async Task<IActionResult> CheckLogin([FromQuery]SignInRequest request)
        {
            var signInResponse = await _services.CheckLogin(request);
            if (signInResponse == null)
                return StatusCode(200);
            return Ok(signInResponse);
        }
        [HttpPost("createaccount")]
        public async Task<IActionResult> CreateAccount(SignUpRequest request)
        {
            var signUpRequest = await _services.CreateAccount(request);
            return Ok(signUpRequest);
        }
        [HttpPut("remakepassword")]
        public async Task<IActionResult> RePass([FromBody]RepassRequest request)
        {
            var result = await _services.RePass(request);
            return Ok(result);
        }
        [HttpPut("remakeaccount")]
        public async Task<IActionResult> ReInfo([FromBody] ReInfoRequest request)
        {
            var result = await _services.ReInfo(request);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }
        [HttpGet("getcontacts")]
        public async Task<IActionResult> GetContacts(int accountId)
        {
            var accCus = await _services.GetContacts(accountId);
            if (accCus == null)
                return NotFound();
            return Ok(accCus);
        }
        [HttpPost("addnewaccountshipcontact")]
        public async Task<IActionResult> AddNewAccountShipContact(AddAccShipContactRequest request)
        {
            var result = await _services.AddNewAccountShipContact(request);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }
        [HttpPut("deleteshipcontact")]
        public async Task<IActionResult> DeleteShipContact(int idAccountShipContact)
        {
            var result = await _services.DeleteShipContact(idAccountShipContact);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }
        [HttpPost("getcalculbag")]
        public async Task<IActionResult> GetCalculbag(int[] request)
        {
            var result = await _services.GetCalculbag(request);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpPost("createbill")]
        public async Task<IActionResult> CreateBill(CreateBillRequest request)
        {
            var result = await _services.CreateBill(request);
            if (result == false)
                return BadRequest();
            return Ok(result);
        }
        [HttpGet("getbilldetailbyaccountid")]
        public async Task<IActionResult> GetBillDetailByAccountId(int accountId)
        {
            var result = await _services.GetBillDetailByAccountId(accountId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpPut("cancelBill")]
        public async Task<IActionResult> CancelBill(int billId, int type)
        {
            var result = await _services.CancelBill(billId, type);
            if (result == false)
                return BadRequest();
            return Ok(result);
        }
    }
}
