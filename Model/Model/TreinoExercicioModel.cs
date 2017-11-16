using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TreinoExercicioModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TREINOID")]
        public int TreinoFK { get; set; }

        [ForeignKey("EXERCICIOID")]
        public int ExercicioFK { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public int Repeticoes { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public int Peso { get; set; }
    }
}
