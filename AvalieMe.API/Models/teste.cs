using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvalieMe.API.Models
{
    [Table("avalieme.teste")]
    public partial class teste
    {
        public long id { get; set; }

        public long usuarioId { get; set; }

        [Required]
        [StringLength(50)]
        public string nomeImagem { get; set; }

        [Required]
        [StringLength(50)]
        public string categoria { get; set; }

        [Column(TypeName = "bit")]
        public bool? multiplasPessoas { get; set; }

        [StringLength(50)]
        public string posicaoPessoa { get; set; }

        public DateTime inclusao { get; set; }

        public DateTime? alteracao { get; set; }

        [Column(TypeName = "bit")]
        public bool ativo { get; set; }
    }
}
