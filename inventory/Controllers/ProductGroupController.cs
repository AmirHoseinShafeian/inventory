using inventory.DataAccess;
using inventory.Entities;
using inventory.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductGroupController : ControllerBase
    {
        private readonly EFCoreContext _context;

        public ProductGroupController(EFCoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductGroupListDto>>> GetProductGroup()
        {
            try
            {
                List<ProductGroup> productGroupList = new List<ProductGroup>();
                productGroupList = await _context.productGroups.ToListAsync();

                List<ProductGroupListDto> productGroupListDto = new List<ProductGroupListDto>();

                foreach (var item in productGroupList)
                {
                    productGroupListDto.Add(new ProductGroupListDto()
                    {

                        Id = item.Id,
                        Name = item.Name,

                        ParentGroupId = item.ParentGroup != null ? item.ParentGroup.Id : null,
                        ParentGroupName = item.ParentGroup != null ? item.ParentGroup.Name : null,

                        Code = item.Code
                    });
                }

                return productGroupListDto;
            }
            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }



        }

        [HttpGet("{id}")]
        public ActionResult<ProductGroupListDto> GetProductGroupById(int id)
        {
            try
            {
                var product = _context.productGroups.Include(m => m.ParentGroup).FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return NotFound();
                }
                ProductGroupListDto productGroupListDto = new ProductGroupListDto();

                productGroupListDto.Id = product.Id;
                productGroupListDto.Name = product.Name;
                productGroupListDto.Code = product.Code;
                if (product.ParentGroup != null)
                {
                    productGroupListDto.ParentGroupId = product.ParentGroup.Id;
                    productGroupListDto.ParentGroupName = product.ParentGroup.Name;
                }

                return productGroupListDto;

            }
            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }

        }

        [HttpPost]
        public ActionResult<ProductGroup> CreateProductGroup(ProductGroupDto productGroupDto)
        {
            try
            {
                var rand = new Random();
                var uid = rand.Next(1, 500);
                ProductGroup productGroup = new ProductGroup();
                productGroup.Name = productGroupDto.Name;
                productGroup.Code = uid;
                productGroup.ParentGroupId = productGroupDto.ParentGroup;
                if (productGroupDto == null)
                {
                    return NotFound();
                }
                _context.productGroups.Add(productGroup);
                _context.SaveChanges();
                return Ok(productGroupDto);

            }
            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductGroup(int id)
        {
            try
            {
                var productGroup = _context.productGroups.Find(id);
                if (productGroup == null)
                {
                    return NotFound();
                }
                _context.productGroups.Remove(productGroup);
                await _context.SaveChangesAsync();
                return Ok(productGroup);
            }
            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }



        }

        [HttpPut, Route("update")]
        public async Task<IActionResult> UpdateProductGroup(ProductGroupUpdateDto productGroupUpdateDto)
        {
            try
            {
                var productGroup = _context.productGroups.FirstOrDefault(x => x.Id == productGroupUpdateDto.Id);
                if (productGroup == null)
                {
                    return NotFound();
                }

                productGroup.Name = productGroupUpdateDto.Name ?? productGroup.Name;
                productGroup.ParentGroupId = productGroupUpdateDto.ParentGroup ?? productGroup.ParentGroupId;
                productGroup.Code = productGroupUpdateDto.ProductGroupId ?? productGroup.Code;

                _context.Entry(productGroup).State = EntityState.Modified;
                _context.productGroups.Update(productGroup);
                _context.SaveChanges();

                return Ok(productGroupUpdateDto);
            }
            catch (Exception)
            {

                return BadRequest("خظایی رخ داده است");
            }


        }
    }
}
