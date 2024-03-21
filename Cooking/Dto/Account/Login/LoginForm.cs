using System.ComponentModel.DataAnnotations;

namespace Cooking.Dto.Account.Login
{
    public class LoginForm
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
