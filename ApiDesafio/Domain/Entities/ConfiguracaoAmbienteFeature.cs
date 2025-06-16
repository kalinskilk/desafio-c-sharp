using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDesafio.Domain.Entities
{
    public class ConfiguracaoAmbienteFeature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int FeatureToggleId { get; set; }

        [ForeignKey("FeatureToggleId")]
        public FeatureToggle FeatureToggle { get; set; }

        [Required]
        public int AmbienteId { get; set; }

        [ForeignKey("AmbienteId")]
        public Ambiente Ambiente { get; set; }

        public bool AtivoNesteAmbiente { get; set; }
    }
}
