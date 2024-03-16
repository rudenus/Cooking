namespace Dal.Entities
{
    public class Product
    {
            public Guid ProductId { get; set; }

            public string Name { get; set; }

            public int Calories { get; set; }

            public bool IsModerated { get; set; }

            public int Proteins { get; set; }

            public int Fats { get; set; }

            public int Carbohydrates { get; set; }
    }
}
