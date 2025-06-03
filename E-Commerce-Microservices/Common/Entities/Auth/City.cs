using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities.Auth
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = default!;

        [Required]
        public int ProvinceId { get; set; }

        [ForeignKey(nameof(ProvinceId))]
        public Province Province { get; set; } = default!;
    }
}
