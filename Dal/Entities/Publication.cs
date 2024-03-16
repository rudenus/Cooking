namespace Dal.Entities
{
    public class Publication
    {
        public Guid PublicationId { get; set; }

        public string Body { get; set; }

        public int CommentsNumber { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid? FileId { get; set; }

        public int LikesNumber { get; set; }

        public Guid RecipeId { get; set; }

        public string Title { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public File? File { get; set; }

        public ICollection<Like> Likes { get; set; } = new List<Like>();

        public Recipe? Recipe {  get; set; } 
    }
}
