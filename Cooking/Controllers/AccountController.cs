using BusinessLogic.UserLogic;
using BusinessLogic.UserLogic.Models.Create;
using Cooking.Dto.Account.Get;
using Cooking.Dto.Account.Login;
using Cooking.Dto.Account.Register;
using Cooking.Infrastructure;
using Cooking.Infrastructure.Authenticator;
using Cooking.Infrastructure.DuplicateFinder;
using Cooking.Infrastructure.Validator.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Authentication;

namespace Cooking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly Authenticator authenticator;
        private readonly DuplicateFinder duplicateFinder;
        private readonly UserLogic userLogic;
        private readonly UserValidator validator;
        public AccountController(Authenticator authenticator, DuplicateFinder duplicateFinder,UserLogic userLogic, UserValidator validator)
        {
            this.authenticator = authenticator;
            this.duplicateFinder = duplicateFinder;
            this.userLogic = userLogic;
            this.validator = validator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(
            RegisterForm input)
        {
            var user = new CreateUserInput();

            try
            {
                var validUser = validator.Validate(new UserValidatorModel()
                {
                    Email = input.Email,
                    Login = input.Login,
                    Password = input.Password,
                    Phone = input.Phone,
                });

                user.Email = validUser.Email;
                user.Login = validUser.Login;
                user.Password = validUser.Password;
                user.Phone = validUser.Phone;
            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            try
            {
                await duplicateFinder.FindDuplicates(new DuplicateFinderUserModel()
                {
                    Email = user.Email,
                    Login = user.Login,
                    Phone = user.Phone,
                });
            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            await userLogic.Create(user);

            return Ok();
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token(
            [FromBody, BindRequired] LoginForm input)
        {
            AuthenticateResult result;
            try
            {
                result = await authenticator.Authenticate(input.Login, input.Password);
            }

            catch (AuthenticationException ex)
            {
                return StatusCode(403, ex.Message);
            }

            return Ok(new
            {
                IsModerator = result.Role == Dal.Enums.Role.Moderator,
                Token = result.Token,
            });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
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

            var user = await userLogic.Get(userId);

            if (user == null)
            {
                return StatusCode(403, "Неверный формат токена авторизации");
            }

            var result = new GetResult()
            {
                Name = user.Name,
                Role = user.Role,
            };

            return Ok(result);
        }
    }
}
