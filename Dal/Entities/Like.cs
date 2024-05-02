namespace Dal.Entities
{
    public class Like
    {
        public Guid PublicationId { get; set; }

        public Publication Publication { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
