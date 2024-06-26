﻿using inventory.DataAccess;
using inventory.Entities;
using inventory.ModelDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace inventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductRegistrationController : ControllerBase
    {
        private readonly EFCoreContext _context;

        public ProductRegistrationController(EFCoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// ثبت رسید محصول
        /// </summary>
        /// <param name="productRegistrationDto"></param>
        /// <returns></returns>
        [HttpPost, Authorize]
        public ActionResult<ProductRegistration> CreateProductRegistration(ProductRegistrationDto productRegistrationDto)
        {
            try
            {
                ProductRegistration productRegistration = new ProductRegistration();
                productRegistration.DataRegister = DateTime.Now;
                productRegistration.ProductId = productRegistrationDto.ProductId;
                productRegistration.PriceOfProduct = productRegistrationDto.PriceOfProduct;
                productRegistration.ProductNumber = productRegistrationDto.ProductNumber;
                var totalPrice = productRegistration.PriceOfProduct * productRegistration.ProductNumber;
                productRegistration.TotalPrice = totalPrice;

                if (productRegistrationDto == null)
                {
                    return NotFound();
                }
                _context.productRegistrations.Add(productRegistration);
                _context.SaveChanges();

                return Ok(productRegistrationDto);
            }
            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }
        }
    }
}
