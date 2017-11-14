using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TreinoExercicioModel
    {
        [Key]
        public int Id { get; set; }
        public int TreinoFK { get; set; }
        public int ExercicioFK { get; set; }
        public int Repeticoes { get; set; }
        public int Peso { get; set; }
    }
}
