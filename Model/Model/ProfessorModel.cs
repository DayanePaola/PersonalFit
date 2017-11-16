using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProfessorModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("USUARIOID")]
        public int UsuarioFK { get; set; }
        [Required]
        public string Nome { get; set; }

        public virtual AlunoModel Aluno { get; set; }

    }
}
