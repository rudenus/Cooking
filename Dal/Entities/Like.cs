namespace Dal.Entities
{
    public class Like
    {
        public Guid LikeId {  get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
