using System.ComponentModel.DataAnnotations;

namespace Cooking.Dto.Recipe.Create
{
    public class CreateForm
    {
        [Required]
        public string Description { get; set; }

        public byte[]? File {  get; set; }

        [Required]
        public string Name { get; set; }

        public int? ServingsNumber { get; set; }

        public int? Weight { get; set; }

        public List<CreateFormIngridient> Ingridients { get; set; } = new List<CreateFormIngridient>();

        public List<CreateFormOperation> Operations { get; set; } = new List<CreateFormOperation>();
    }

    public class CreateFormProduct
    {
        [Required]
        public int Calories { get; set; }

        [Required]
        public int Carbohydrates { get; set; }

        [Required]
        public int Fats { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Proteins { get; set; }
    }

    public class CreateFormOperation
    {
        [Required]
        public int Step { get; set; }
        
        public byte[]? File { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int TimeInSeconds { get; set; }

        [Required]
        public string Title { get; set; }
    }

    public class CreateFormIngridient
    {
        public CreateFormProduct NewProduct { get; set; }

        public Guid? ExistingProductId { get; set; }

        [Required]
        public int Weight { get; set; }
    }
}
