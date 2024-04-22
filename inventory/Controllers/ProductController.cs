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
                    return NotFound();
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
                product.Exp = productDto.Exp;
                if (productDto == null)
                {
                    return NotFound();
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
                    return NotFound();
                }

                product.Name = productUpdateDto.Name ?? product.Name;
                product.ProductGroupId = productUpdateDto.ProductGroupId ?? product.ProductGroupId;
                product.Price = productUpdateDto.Price ?? product.Price;
                product.Exp = productUpdateDto.Exp ?? product.Exp;


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

        [HttpGet, Route("ReportByName")]
        public async Task<ActionResult<IEnumerable<Product>>> ProductReportByname(string name)
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


                ProductReportByNameDto productReport = new ProductReportByNameDto();

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

        [HttpGet, Route("Report")]
        public ActionResult<List<ProductReportDto>> ProductReport()
        {
            try
            {

                var productReport = _context.products.Include(p => p.ProductGroup).Include(s => s.ProductRegistrations).Include(d => d.ProductRemittances).Select(m =>
                   new ProductReportDto()
                   {
                       Price = m.Price,
                       ProductGroupName = m.ProductGroup.Name,
                       ProductName = m.Name,
                       ProductId = m.Id,
                       Count = m.ProductRegistrations.Sum(q => q.ProductNumber) - m.ProductRemittances.Sum(a => a.ProductNumber)
                   }
                    ).OrderBy(y => y.Price).ToList();

                if (productReport == null)
                {
                    return Ok(new List<ProductReportDto>());
                }


                return Ok(productReport);
            }
            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }

        }

        [HttpGet, Route("ProductReportExp")]
        public async Task<ActionResult<IEnumerable<Product>>> ProductReportExp()
        {
            try
            {
                var dateTime = DateTime.Now;
                dateTime = dateTime.Date.AddDays(3);

                var productReportExp = await _context.products.Where(x => x.Exp == dateTime).ToListAsync();


                if (productReportExp == null || productReportExp.Count == 0)
                    return NotFound();

                return Ok(productReportExp);
            }

            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }



        }

        [HttpGet, Route("ReportByCount")]
        public ActionResult<List<ProductReportDto>> ProductReportByCount()
        {
            try
            {

                var productReport = _context.products.Include(p => p.ProductGroup).Include(s => s.ProductRegistrations).Include(d => d.ProductRemittances).Select(m =>
                   new ProductReportDto()
                   {
                       Price = m.Price,
                       ProductGroupName = m.ProductGroup.Name,
                       ProductName = m.Name,
                       ProductId = m.Id,
                       Count = m.ProductRegistrations.Sum(q => q.ProductNumber) - m.ProductRemittances.Sum(a => a.ProductNumber)
                   }
                    ).Where(z => z.Count < 5).OrderBy(y => y.Price).ToList();

                if (productReport == null)
                {
                    return Ok(new List<ProductReportDto>());
                }

                return Ok(productReport);
            }
            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }

        }

        [HttpGet, Route("ProductTotalPriceReport")]
        public ActionResult<List<ProductReportDto>> ProductTotalPriceReport()
        {
            try
            {

                var productReport = _context.products.Include(p => p.ProductGroup).Include(s => s.ProductRegistrations).Include(d => d.ProductRemittances).Select(m =>
                   new ProductReportDto()
                   {
                       Price = m.Price,
                       Count = m.ProductRegistrations.Sum(q => q.ProductNumber) - m.ProductRemittances.Sum(a => a.ProductNumber)
                   }
                    ).ToList();

                if (productReport == null)
                {
                    return Ok(new List<ProductTotalPrice>());
                }

                ProductTotalPrice productTotalPrice = new ProductTotalPrice();

                int totalPrice = 0;

                foreach (var item in productReport)
                {
                    totalPrice += item.Price * item.Count;
                }
                productTotalPrice.TotalPrice = totalPrice;

                return Ok(productTotalPrice);
            }
            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }

        }
    }
}
