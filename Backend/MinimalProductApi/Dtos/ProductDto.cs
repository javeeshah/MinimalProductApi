using System.ComponentModel.DataAnnotations;

namespace MinimalProductApi.Dtos
{
    public class ProductDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(minimum: 0.01, maximum: (double)decimal.MaxValue, ErrorMessage = "The Price field must be greater than zero")]
        public decimal Price { get; set; }
    }
}
