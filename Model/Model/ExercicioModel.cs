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
        public String Nome { get; set; }
        public String Equipamento { get; set; }
    }
}
