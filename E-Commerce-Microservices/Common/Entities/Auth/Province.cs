using System.ComponentModel.DataAnnotations;

namespace Common.Entities.Auth
{
    public class Province
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = default!;

        public List<City> Cities { get; set; } = new();
    }
}
