using inventory.DataAccess;
using inventory.Entities;
using inventory.ModelDto;
using Microsoft.AspNetCore.Mvc;

namespace inventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductRemittanceController : ControllerBase
    {
        private readonly EFCoreContext _context;

        public ProductRemittanceController(EFCoreContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// ثبت حواله محصول
        /// </summary>
        /// <param name="ProductRemittanceDto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ProductRemittance> CreateProductRegistration(ProductRemittanceDto ProductRemittanceDto)
        {
            try
            {
                ProductRemittance ProductRemittance = new ProductRemittance();
                ProductRemittance.DataRegister = DateTime.Now;
                ProductRemittance.ProductId = ProductRemittanceDto.ProductId;
                ProductRemittance.PriceOfProduct = ProductRemittanceDto.PriceOfProduct;
                ProductRemittance.ProductNumber = ProductRemittanceDto.ProductNumber;
                var totalPrice = ProductRemittance.PriceOfProduct * ProductRemittance.ProductNumber;
                ProductRemittance.TotalPrice = totalPrice;

                if (ProductRemittanceDto == null)
                {
                    return NotFound();
                }
                _context.productRemittances.Add(ProductRemittance);
                _context.SaveChanges();

                return Ok(ProductRemittanceDto);

            }
            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }
        }

    }
}
