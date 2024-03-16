namespace Cooking.Infrastructure.Validator
{
    public class Validator
    {
        public UserValidatorModel Validate(UserValidatorModel user)
        {
            if (string.IsNullOrEmpty(user.Login) || user.Login.Length < 5 || user.Login.Length > 50 || user.Login.Any(x => !char.IsLetterOrDigit(x)))
            {
                throw new ArgumentException("Логин должен быть длиной от 5 до 50 символов и состоять из букв или цифр");
            }

            if (string.IsNullOrEmpty(user.Name))
            {
                throw new ArgumentException("Имя пользователя не может быть пустым");
            }

            if (string.IsNullOrEmpty(user.Surname))
            {
                throw new ArgumentException("Фамилия пользователя не может быть пустым");
            }

            if (!PasswordValidator.TryParse(user.Password, out var password))
            {
                throw new ArgumentException("Неправильный формат пароля");
            }

            if (!EmailValidator.TryParse(user.Email, out var email))
            {
                throw new ArgumentException("Неправильный формат почты");
            }

            if (!PhoneValidator.TryParse(user.Phone, out var phone))
            {
                throw new ArgumentException("Неправильный формат телефона");
            }

            return new UserValidatorModel()
            {
                Email = email,
                Login = user.Login,
                Name = user.Name,
                Password = password,
                Phone = phone,
                Surname = user.Surname,
            };
        }
    }
}
