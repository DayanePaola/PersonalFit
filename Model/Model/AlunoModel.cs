using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("ALUNO")]
    public class AlunoModel
    {
        public AlunoModel()
        {
            Treino = new HashSet<TreinoModel>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [MinLength(4, ErrorMessage = "Deve possuir no mínimo 4 caracteres!")]
        [MaxLength(15, ErrorMessage = "Deve possuir no máximo 15 caracteres!")]
        [Display(Name = "Nome*")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [MinLength(9, ErrorMessage = "Deve conter no minímo 9 caracteres!")]
        [Display(Name = "CPF*")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [Display(Name = "Idade*")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [Display(Name = "Peso*")]
        public double Peso { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [Display(Name = "Altura*")]
        public double Altura { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [MinLength(5, ErrorMessage = "Deve possuir no mínimo 5 caracteres!")]
        [MaxLength(30, ErrorMessage = "Deve possuir no máximo 30 caracteres!")]
        [Display(Name = "Objetivo*")]
        public string Objetivo { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioFK { get; set; }

        [Required]
        [ForeignKey("Professor")]
        [Display(Name = "Professor")]
        public int ProfessorFK { get; set; }

        public virtual UsuarioModel Usuario { get; set; }
        public virtual ProfessorModel Professor { get; set; }
        public virtual ICollection<TreinoModel> Treino { get; set; }
    }
}
