namespace Dal.Entities
{
    public class Comment
    {
        public Guid CommentId { get; set; }

        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid PublicationId { get; set; }

        public Guid UserId { get; set; }

        public Publication Publication { get; set; }

        public User User { get; set; }
    }
}
