using System.ComponentModel.DataAnnotations;

namespace ZavodConservDatabaseImplement.Models
{
    public class ConservComponent
    {
        public int Id { get; set; }

        public int ConservId { get; set; }

        public int ComponentId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Component Component { get; set; }

        public virtual Conserv Conserv { get; set; }
    }
}