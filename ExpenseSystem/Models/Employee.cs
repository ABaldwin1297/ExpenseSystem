using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseSystem.Models {

    [Index(nameof(Email), IsUnique = true)]
    public class Employee {
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; } = string.Empty;
        [StringLength(30)]
        public string Email { get; set; } = null!;
        [StringLength(30)]
        public string Password { get; set; } = string.Empty;
        public bool Admin { get; set; } = false;
        [Column(TypeName = "decimal (9,2)")]
        public decimal ExpensesDue { get; set; } = 0;
        [Column(TypeName = "decimal (9,2)")]
        public decimal ExpensesPaid { get; set; } = 0;
    }
}
