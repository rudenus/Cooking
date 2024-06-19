using Dal.Enums;

namespace Dal.Entities
{
    public class User
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string Phone { get; set; }

        public Role Role { get; set; }

        public ICollection<Publication> Publications { get; set; } = new List<Publication>();

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    }
}
