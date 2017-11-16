using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("USUARIO")]
    public class UsuarioModel
    {
        [Key]
        public int Id { get; set; }
        
        [Required (ErrorMessage ="Campo obrigatório!")]
        [MinLength(5, ErrorMessage = "Deve possuir no mínimo 5 caracteres!")]
        [MaxLength(10, ErrorMessage = "Deve possuir no mínimo 10 caracteres!")]
        [Display(Name = "Login*")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(5, ErrorMessage = "Deve possuir no mínimo 5 caracteres!")]
        [MaxLength(30, ErrorMessage = "Deve possuir no mínimo 30 caracteres!")]
        [Display(Name = "Senha*")]
        public string Senha { get; set; }

        public virtual ProfessorModel Professor { get; set; }
        public virtual AlunoModel Aluno { get; set; }
    }
}
