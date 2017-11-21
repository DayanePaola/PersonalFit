using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("EXERCICIO")]
    public class ExercicioModel
    {
        public ExercicioModel()
        {
            TreinoExercicio = new HashSet<TreinoExercicioModel>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [MinLength(3, ErrorMessage = "Deve conter no minímo 3 caracteres!")]
        [MaxLength(30, ErrorMessage = "Deve conter no máximo 30 caracteres!")]
        [Display(Name = "Nome do exercício*")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [MinLength(30, ErrorMessage = "Deve conter no minímo 30 caracteres!")]
        [MaxLength(100, ErrorMessage = "Deve conter no máximo 100 caracteres!")]
        [Display(Name = "Descrição do exercício*")]
        public string Descricao { get; set; }

        [MinLength(3, ErrorMessage = "Deve conter no minímo 3 caracteres!")]
        [MaxLength(30, ErrorMessage = "Deve conter no máximo 30 caracteres!")]
        [Display(Name = "Equipamento")]
        public string Equipamento { get; set; }

        public virtual ICollection<TreinoExercicioModel> TreinoExercicio { get; set; }
    }
}
