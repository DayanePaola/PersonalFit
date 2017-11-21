using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("PROFESSOR")]
    public class ProfessorModel
    {
        public ProfessorModel()
        {
            Aluno = new HashSet<AlunoModel>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioFK { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [MinLength(3, ErrorMessage = "Deve conter no minímo 3 caracteres!")]
        [MaxLength(30, ErrorMessage = "Deve conter no máximo 30 caracteres!")]
        [Display(Name = "Nome*")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [MinLength(9, ErrorMessage = "Deve conter no minímo 9 caracteres!")]
        [MaxLength(11, ErrorMessage = "Deve conter no máximo 11 caracteres!")]
        [Display(Name = "CPF*")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [MinLength(9, ErrorMessage = "Deve conter no minímo 9 caracteres!")]
        [MaxLength(11, ErrorMessage = "Deve conter no máximo 11 caracteres!")]
        [Display(Name = "Cédula de Identificação")]
        public string Cref { get; set; }

        public virtual UsuarioModel Usuario { get; set; }
        public virtual ICollection<AlunoModel> Aluno { get; set; }

    }
}
