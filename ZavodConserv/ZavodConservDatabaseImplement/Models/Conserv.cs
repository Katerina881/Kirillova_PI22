
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavodConservDatabaseImplement.Models
{
    public class Conserv
    {
        public int Id { get; set; }

        [Required]
        public string ConservName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("ConservId")]
        public virtual List<ConservComponent> ConservComponents { get; set; }

        [ForeignKey("ConservId")]
        public virtual List<Order> Orders { get; set; }
    }
}
