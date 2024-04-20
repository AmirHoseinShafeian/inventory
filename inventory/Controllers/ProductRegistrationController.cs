using inventory.DataAccess;
using inventory.Entities;
using inventory.ModelDto;
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

        [HttpPost]
        public ActionResult<ProductRegistration> CreateProductRegistration(ProductRegistrationDto productRegistrationDto)
        {
            try
            {
                ProductRegistration productRegistration = new ProductRegistration();
                productRegistrationDto.DataRegister = DateTime.Now;
                productRegistration.DataRegister = productRegistrationDto.DataRegister;
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
