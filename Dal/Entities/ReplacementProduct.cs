using Dal.Enums;

namespace Dal.Entities
{
    public class ReplacementProduct
    {
        public Guid ReplacementId { get; set; }

        public Product Replacement { get; set; }

        public Guid ReplacingId { get; set; }

        public Product Replacing { get; set; }

        public ReplacementLevel ReplacementLevel { get; set; }
    }
}
