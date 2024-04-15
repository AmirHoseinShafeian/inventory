using inventory.DataAccess;
using inventory.Entities;
using inventory.ModelDto;
using Microsoft.AspNetCore.Mvc;

namespace inventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductRemittanceController:ControllerBase
    {
        private readonly EFCoreContext _context;

        public ProductRemittanceController(EFCoreContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<ProductRemittance> CreateProductRegistration(ProductRemittanceDto ProductRemittanceDto)
        {

            ProductRemittance ProductRemittance = new ProductRemittance();
            ProductRemittanceDto.DataRegister = DateTime.Now;
            ProductRemittance.DataRegister = ProductRemittanceDto.DataRegister;
            ProductRemittance.ProductId = ProductRemittanceDto.ProductId;
            ProductRemittance.PriceOfProduct = ProductRemittanceDto.PriceOfProduct;
            ProductRemittance.ProductNumber = ProductRemittanceDto.ProductNumber;
            var totalPrice = ProductRemittanceDto.PriceOfProduct * ProductRemittanceDto.ProductNumber;
            ProductRemittance.TotalPrice = totalPrice;

            if (ProductRemittanceDto == null)
            {
                return BadRequest();
            }
            _context.productRemittances.Add(ProductRemittance);
            _context.SaveChanges();

            return Ok(ProductRemittanceDto);
        }

    }
}
