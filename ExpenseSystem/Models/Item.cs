using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseSystem.Models {
    public class Item {
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(7,2)")]
        public decimal Price { get; set; }
    }
}
