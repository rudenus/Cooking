using BusinessLogic.UserLogic.Models.Create;
using BusinessLogic.UserLogic.Models.Get;
using Dal;
using Dal.Entities;

namespace BusinessLogic.UserLogic
{
    public class UserLogic
    {
        //я зная что это плохая практика. Но мое приложение не такое большое, чтобы изолироваться от слоя хранения данных.
        private readonly Context context;
        public UserLogic(Context context)
        {
            this.context = context;
        }

        public async Task<GetUserOutput> Get(Guid userId)
        {
            var user = context.Users
                .Select(x => new
                {
                    x.UserId,
                    x.Name,
                    x.Role,
                })
                .FirstOrDefault(x => x.UserId == userId);

            return new GetUserOutput()
            {
                Name = user.Name,
                Role = user.Role.ToString(),
            };
        }
        public async Task Create(CreateUserInput user)
        {
            string fullname = user.Name + " " + user.Surname;
            if (!string.IsNullOrEmpty(user.Patronymic))
            {
                fullname += " " + user.Patronymic;
            }

            var entity = new User()
            {
                UserId = Guid.NewGuid(),
                Email = user.Email,
                FullName = fullname,
                IsBanned = false,
                Login = user.Login,
                Name = user.Name,
                PasswordHash = MD5Hash(user.Password),
                Patronymic = user.Patronymic,
                Phone = user.Phone,
                Role = Dal.Enums.Role.Default,
                Surname = user.Surname,
            };

            context.Users.Add(entity);

            await context.SaveChangesAsync();
        }


        //не относится к бизнес логике. Но считаю, что метод крайне устойчив -> пока(возможно навсегда) можно оставить здесь
        private string MD5Hash(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                var temp = Convert.ToHexString(hashBytes).ToLower();

                return temp;
            }
        }
    }
}
