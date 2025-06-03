using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities.Auth
{
    public class UserAddress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = default!;


        [Required]
        [MaxLength(200)]
        public string RecipientName { get; set; } = default!;

        [Required]
        [MaxLength(500)]
        public string Address { get; set; } = default!;

        [Required]
        public int CityId { get; set; }

        [Required]
        public int ProvinceId { get; set; }


        [Required]
        [MaxLength(20)]
        public string PostalCode { get; set; } = default!;

        [ForeignKey(nameof(CityId))]
        public City City { get; set; } = default!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = default!;
        [ForeignKey(nameof(ProvinceId))]
        public Province Province { get; set; } = default!;
    }
}
