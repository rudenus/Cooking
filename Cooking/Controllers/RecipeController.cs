using BusinessLogic.RecipeLogic;
using BusinessLogic.RecipeLogic.Models.Create;
using BusinessLogic.RecipeLogic.Models.List;
using Cooking.Dto.Recipe.Create;
using Cooking.Dto.Recipe.List;
using Cooking.Infrastructure;
using Cooking.Infrastructure.Validator.Recipe;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Authentication;

namespace Cooking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : Controller
    {
        private readonly RecipeLogic recipeLogic;
        private readonly RecipeValidator recipeValidator;
        public RecipeController(RecipeLogic recipeLogic, RecipeValidator recipeValidator)
        {
            this.recipeLogic = recipeLogic;
            this.recipeValidator = recipeValidator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody, BindRequired] CreateForm input)
        {
            Guid userId;
            try
            {
                userId = this.GetUserId();//нужно как то уйти от этой хрени
            }

            catch (AuthenticationException ex)
            {
                return StatusCode(403, ex.Message);
            }

            var recipe = new CreateRecipeInput()
            {
                Description = input.Description,
                Name = input.Name,
                File = input.File,
                ServingsNumber = input.ServingsNumber,
                UserId = userId,
                Weight = input.Weight,
            };

            

            try
            {
                var validRecipe = recipeValidator.Validate(new ValidatorRecipeInput()
                {
                    Ingridients = input.Ingridients.Select(x => new ValidatorRecipeInputIngridient()
                    {
                        ExistingProductId = x.ExistingProductId,
                        NewProduct = x.NewProduct != null ? new ValidatorRecipeInputProduct()
                        {
                            Calories = x.NewProduct.Calories,
                            Carbohydrates = x.NewProduct.Calories,
                            Fats = x.NewProduct.Fats,
                            Proteins = x.NewProduct.Proteins
                        } : null,
                        Weight = x.Weight
                    }),

                    Operations = input.Operations.Select(x => new ValidatorRecipeInputOperation()
                    {
                        Step = x.Step,
                        TimeInSeconds = x.TimeInSeconds,
                    })
                });

                recipe.Operations = validRecipe.Operations.Zip(input.Operations).Select(x => new CreateRecipeInputOperation()
                {
                    Description = x.Second.Description,
                    File = x.Second.File,
                    Step = x.First.Step,
                    TimeInSeconds = x.First.TimeInSeconds,
                    Title = x.Second.Title,
                });

                recipe.Ingridients = validRecipe.Ingridients.Zip(input.Ingridients).Select(x => new CreateRecipeInputIngridient()
                {
                    ExistingProductId = x.First.ExistingProductId,
                    NewProduct = new CreateRecipeInputProduct()
                    {
                        Calories = x.First.NewProduct.Calories,
                        Carbohydrates = x.First.NewProduct.Carbohydrates,
                        Fats = x.First.NewProduct.Fats,
                        Name = x.Second.NewProduct.Name,
                        Proteins = x.First.NewProduct.Proteins,
                    }
                });
            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            await recipeLogic.Create(recipe);

            return Ok();
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
