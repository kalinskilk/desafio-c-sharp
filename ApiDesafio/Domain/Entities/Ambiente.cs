using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDesafio.Domain.Entities
{
    public class Ambiente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome único é obrigatório.")]
        [MaxLength(20, ErrorMessage = "O nome único deve ter no máximo 20 caracteres.")]
        [RegularExpression("^[A-Z]{2,10}$", ErrorMessage = "O nome único deve conter apenas letras maiúsculas e entre 2 a 10 caracteres.")]
        public string NomeUnico { get; set; }
    }
}
