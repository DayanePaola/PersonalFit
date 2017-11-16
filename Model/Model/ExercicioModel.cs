using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ExercicioModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [MinLength(30, ErrorMessage = "Deve conter no minímo 30 caracteres!")]
        [Display(Name = "Descrição do exercício*")]
        public string Descricao { get; set; }
        public string Equipamento { get; set; }

        public virtual TreinoExercicioModel TreinoExercicio { get; set; }
    }
}
