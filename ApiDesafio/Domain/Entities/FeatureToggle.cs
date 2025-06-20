using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDesafio.Domain.Entities
{
    public class FeatureToggle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string NomeUnico { get; set; }

        [MaxLength(255)]
        public required string Descricao { get; set; }

        public bool AtivoGlobalmente { get; set; }
    }
}
