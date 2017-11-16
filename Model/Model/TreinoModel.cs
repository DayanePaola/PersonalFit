using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TreinoModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ALUNOID")]
        public int AlunoFK { get; set; }

        public virtual TreinoExercicioModel TreinoExercicio  { get; set; }
    }
}
