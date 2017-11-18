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
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [MinLength(4, ErrorMessage = "Deve possuir no mínimo 4 caracteres!")]
        [MaxLength(15, ErrorMessage = "Deve possuir no mínimo 15 caracteres!")]
        [Display(Name = "Nome*")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [MinLength(3, ErrorMessage = "Deve possuir no mínimo 3 caracteres!")]
        [MaxLength(3, ErrorMessage = "Deve possuir no mínimo 3 caracteres!")]
        [Display(Name = "Idade*")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [MinLength(4, ErrorMessage = "Deve possuir no mínimo 4 caracteres!")]
        [MaxLength(15, ErrorMessage = "Deve possuir no mínimo 15 caracteres!")]
        [Display(Name = "Peso*")]
        public float Peso { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [MinLength(3, ErrorMessage = "Deve possuir no mínimo 3 caracteres!")]
        [MaxLength(3, ErrorMessage = "Deve possuir no mínimo 3 caracteres!")]
        [Display(Name = "Altura*")]
        public float Altura { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [MinLength(5, ErrorMessage = "Deve possuir no mínimo 5 caracteres!")]
        [MaxLength(15, ErrorMessage = "Deve possuir no mínimo 15 caracteres!")]
        [Display(Name = "Objetivo*")]
        public string Objetivo { get; set; }

        [ForeignKey("USUARIOID")]
        public int UsuarioFK { get; set; }

        [ForeignKey("PROFESSORID")]
        public int ProfessorFK { get; set; }

        public virtual TreinoModel Treino { get; set; }
    }
}
