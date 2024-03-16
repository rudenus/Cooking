using Dal;
using Microsoft.EntityFrameworkCore;

namespace Cooking.Infrastructure.DuplicateFinder
{
    public class DuplicateFinder
    {
        private readonly Context context;
        public DuplicateFinder(Context context)
        {
            this.context = context;
        }

        public async Task FindDuplicates(UserDuplicateFinderModel user)
        {
            var userByLogin = await context.Users
                .AsNoTracking()
                .Select(x => x.Login)
                .FirstOrDefaultAsync(x => x == user.Login);

            if (userByLogin is not null)
            {
                throw new ArgumentException("Пользователь с таким логином уже существует");
            }

            var userByEmail = await context.Users
                .AsNoTracking()
                .Select(x => x.Email)
                .FirstOrDefaultAsync(x => x == user.Email);

            if (userByEmail is not null)
            {
                throw new ArgumentException("Пользователь с такой почтой уже существует");
            }

            var userByPhone = await context.Users
                .AsNoTracking()
                .Select(x => x.Phone)
                .FirstOrDefaultAsync(x => x == user.Phone);

            if (userByPhone is not null)
            {
                throw new ArgumentException("Пользователь с таким телефоном уже существует");
            }
        }
    }
}
