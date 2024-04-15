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
        public async Task<ActionResult<IEnumerable<ProductGroup>>> GetProductGroup()
        {

            return await _context.productGroups.ToListAsync();

        }

        [HttpGet("{id}")]
        public ActionResult<ProductGroup> GetProductGroupById(int id)
        {
            var product = _context.productGroups.Find(id);
            if (product == null)
            {
                return new EmptyResult();
            }
            return product;
        }

        [HttpPost]
        public ActionResult<ProductGroup> CreateProductGroup(ProductGroupDto productGroupDto)
        {
            ProductGroup productGroup = new ProductGroup();
            productGroup.Name = productGroupDto.Name;
            productGroup.ProductGroupCode = productGroupDto.ProductGroupId;
            productGroup.ParentGroupId = productGroupDto.ParentGroup;
            if (productGroupDto == null)
            {
                return BadRequest();
            }
            _context.productGroups.Add(productGroup);
            _context.SaveChanges();

            return Ok(productGroupDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductGroup(int id)
        {
            var productGroup = _context.productGroups.Find(id);

            if (productGroup == null)
            {
                return new EmptyResult();
            }
            _context.productGroups.Remove(productGroup);
            await _context.SaveChangesAsync();
            return Ok(productGroup);
        }

        [HttpPut, Route("update")]
        public async Task<IActionResult> UpdateProductGroup(ProductGroupUpdateDto productGroupUpdateDto)
        {
            var productGroup = _context.productGroups.FirstOrDefault(x => x.Id == productGroupUpdateDto.Id);
            if (productGroup == null)
            {
                return  BadRequest();
            }

            productGroup.Name = productGroupUpdateDto.Name ?? productGroup.Name;
            productGroup.ParentGroupId= productGroupUpdateDto.ParentGroup ?? productGroup.ParentGroupId;
            productGroup.ProductGroupCode = productGroupUpdateDto.ProductGroupId ?? productGroup.ProductGroupCode;

            _context.Entry(productGroup).State = EntityState.Modified;
            _context.productGroups.Update(productGroup);
            _context.SaveChanges();

            return Ok(productGroupUpdateDto);

        }
    }
}
