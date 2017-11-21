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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("TREINOID")]
        public int TreinoFK { get; set; }

        [Required]
        [ForeignKey("EXERCICIOID")]
        public int ExercicioFK { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Repetições*")]
        public int Repeticoes { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Peso*")]
        public double Peso { get; set; }

        public virtual TreinoModel Treino { get; set; }
        public virtual ExercicioModel Exercicio { get; set; }

    }
}
