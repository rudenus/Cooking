using BusinessLogic.ModeratorLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cooking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ModeratorController : Controller
    {
        private readonly ModeratorLogic moderatorLogic;
        public ModeratorController(ModeratorLogic moderatorLogic) 
        {
            this.moderatorLogic = moderatorLogic;
        }

        [HttpPost(@"approve-recipe/{recipeId:guid}")]
        public async Task<IActionResult> ApproveRecipe(Guid recipeId)
        {
            await moderatorLogic.Approve(recipeId);
            return Ok();
        }
    }
}
