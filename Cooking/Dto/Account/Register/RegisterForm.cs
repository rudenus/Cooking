namespace Cooking.Dto.Account.Register
{
    internal class RegisterForm
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string? Patronymic { get; set; }
    }
}
