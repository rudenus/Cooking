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
        public async Task<IActionResult> Create()
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
                Calories = x.Calories,
                Carbohydrates = x.Carbohydrates,
                Description = x.Description,
                Fats = x.Fats,
                Products = x.Products,
                RecipeId = x.RecipeId,
                Name = x.Name,
                Proteins = x.Proteins,
            }));
        }

        [HttpPut("{recipeId:guid}")]
        public async Task<IActionResult> Update()
        {
            return View();
        }
    }
}
