using BusinessLogic.ProductLogic;
using Cooking.Dto.Product.List;
using Microsoft.AspNetCore.Mvc;

namespace Cooking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        ProductLogic productLogic;
        public ProductController(ProductLogic productLogic)
        {
            this.productLogic = productLogic;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var list = await productLogic.List(true);

            return Ok(list.Select(x => new ListResult()
            {
                Calories = x.Calories,
                Carbohydrates = x.Carbohydrates,
                Fats = x.Fats,
                Name = x.Name,
                ProductId = x.ProductId,
                Proteins = x.Proteins,
            }));
        }

        [HttpGet("list-moderator")]
        public async Task<IActionResult> ListModerated()
        {
            var list = await productLogic.List(false);

            return Ok(list.Select(x => new ListResult()
            {
                Calories = x.Calories,
                Carbohydrates = x.Carbohydrates,
                Fats = x.Fats,
                Name = x.Name,
                ProductId = x.ProductId,
                Proteins = x.Proteins,
            }));
        }
    }
}
