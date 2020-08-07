namespace AvalieMe.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("avalieme.usuario")]
    public partial class usuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Required]
        [StringLength(150)]
        public string nome { get; set; }

        [Required]
        [StringLength(150)]
        public string senha { get; set; }

        [Required]
        [StringLength(150)]
        public string email { get; set; }

        public DateTime inclusao { get; set; }

        public DateTime? alteracao { get; set; }

        [Column(TypeName = "bit")]
        public bool ativo { get; set; }

        [StringLength(50)]
        public string token { get; set; }
    }
}
