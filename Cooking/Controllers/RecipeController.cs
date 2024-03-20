using BusinessLogic.RecipeLogic;
using BusinessLogic.RecipeLogic.List;
using Cooking.Dto.Recipe.List;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cooking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : Controller
    {
        private readonly RecipeLogic recipeLogic;
        public RecipeController(RecipeLogic recipeLogic)
        {
            this.recipeLogic = recipeLogic;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody, BindRequired] input)
        {
            return View();
        }

        [HttpGet("{recipeId:guid}")]
        public async Task<IActionResult> Get()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery, BindRequired]ListForm input)
        {
            var listRecipe = await recipeLogic.List(new ListRecipeInput()
            {
                CaloriesMax = input.CaloriesMax,
                CaloriesMin = input.CaloriesMin,
                CarbohydratesMax = input.CarbohydratesMax,
                CarbohydratesMin = input.CarbohydratesMin,
                FatsMax = input.FatsMax,
                FatsMin = input.FatsMin,
                ProteinsMax = input.ProteinsMax,
                ProteinsMin = input.ProteinsMin,
                Products = input.Products,
                PageNumber = input.PageNumber ?? 0,
                PageSize = input.PageSize ?? 20,
            });

            return Ok(listRecipe.Select(x => new ListResult()
            {
                CaloriesPer100 = x.CaloriesPer100,
                CarbohydratesPer100 = x.CarbohydratesPer100,
                Description = x.Description,
                FatsPer100 = x.FatsPer100,
                Products = x.Products,
                RecipeId = x.RecipeId,
                Name = x.Name,
                ProteinsPer100 = x.ProteinsPer100,
            }));
        }

        [HttpPut("{recipeId:guid}")]
        public async Task<IActionResult> Update()
        {
            return View();
        }
    }
}
