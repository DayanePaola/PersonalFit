using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TreinoModel
    {
        [Key]
        public int Id { get; set; }
        public int AlunoFK { get; set; }
    }
}
