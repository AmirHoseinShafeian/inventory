using inventory.DataAccess;
using inventory.Entities;
using inventory.ModelDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly EFCoreContext _context;

        public ProductController(EFCoreContext context)
        {
            _context = context;
        }
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            try
            {
                return await _context.products.ToListAsync();

            }
            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }



        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            try
            {
                var product = _context.products.Find(id);
                if (product == null)
                {
                    return new EmptyResult();
                }
                return product;

            }
            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            try
            {

                var product = _context.products.Find(id);

                if (product == null)
                {
                    return new EmptyResult();
                }
                _context.products.Remove(product);
                await _context.SaveChangesAsync();
                return Ok(product);
            }
            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }
        }

        [HttpPost]
        public ActionResult<Product> CreateProduct(ProductDto productDto)
        {
            try
            {

                Product product = new Product();
                product.Name = productDto.Name;
                product.ProductGroupId = productDto.ProductGroupId;
                product.Price = productDto.Price;
                product.XpDate = productDto.XpDate;
                if (productDto == null)
                {
                    return BadRequest();
                }
                _context.products.Add(product);
                _context.SaveChanges();

            }
            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }
            return Ok(productDto);
        }

        [HttpPut, Route("update")]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            try
            {

                var product = _context.products.FirstOrDefault(x => x.Id == productUpdateDto.Id);
                if (product == null)
                {
                    return BadRequest();
                }

                product.Name = productUpdateDto.Name ?? product.Name;
                product.ProductGroupId = productUpdateDto.ProductGroupId ?? product.ProductGroupId;
                product.Price = productUpdateDto.Price ?? product.Price;
                product.XpDate = productUpdateDto.XpDate ?? product.XpDate;


                _context.Entry(product).State = EntityState.Modified;
                _context.products.Update(product);
                _context.SaveChanges();

                return Ok(productUpdateDto);
            }
            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }

        }

        [HttpGet, Route("Report")]
        public async Task<ActionResult<IEnumerable<Product>>> ProductReport(string name)
        {
            try
            {

                var productName = _context.products.Where(x => x.Name == name)
                    .Include(x => x.ProductGroup)
                    .FirstOrDefault();

                if (productName == null)
                    return NotFound();

                var productInput = _context.productRegistrations.Where(x => x.ProductId == productName.Id).ToList();

                int inputItemCount = 0;
                foreach (var item in productInput)
                {
                    inputItemCount += item.ProductNumber;
                }

                var productOutput = _context.productRemittances.Where(x => x.ProductId == productName.Id).ToList();

                int outputtItemCount = 0;
                foreach (var item in productOutput)
                {
                    outputtItemCount += item.ProductNumber;
                }


                ProductReportDto productReport = new ProductReportDto();

                productReport.Count = inputItemCount - outputtItemCount;
                productReport.ProductName = productName.Name;
                productReport.ProductId = productName.Id;
                productReport.ProductGroupName = productName.ProductGroup.Name;

                return Ok(productReport);
            }
            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }



        }
    }
}
