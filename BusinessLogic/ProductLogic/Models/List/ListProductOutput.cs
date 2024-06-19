namespace BusinessLogic.ProductLogic.Models.List
{
    public class ListProductOutput
    {
        public Guid ProductId { get; set; }

        public int? Calories { get; set; }

        public int? Carbohydrates { get; set; }

        public int? Fats { get; set; }

        public string Name { get; set; }

        public int? Proteins { get; set; }
    }
}
