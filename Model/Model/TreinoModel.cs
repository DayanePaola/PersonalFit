﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("TREINO")]
    public class TreinoModel
    {
        public TreinoModel()
        {
            TreinoExercicio = new HashSet<TreinoExercicioModel>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Titulo*")]
        public string Titulo { get; set; }

        [Required]
        [ForeignKey("Aluno")]
        [Display( Name = "Aluno*")]
        public int AlunoFK { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display( Name = "Situação")]
        public string Situacao { get; set; }

        [Required]
        [Display( Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }
        
        public virtual AlunoModel Aluno { get; set; }
        public virtual ICollection<TreinoExercicioModel> TreinoExercicio  { get; set; }
    }
}
