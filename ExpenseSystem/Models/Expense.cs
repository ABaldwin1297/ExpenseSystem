using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ExpenseSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseSystem.Models {
    public class Expense {
        public int Id { get; set; }
        [StringLength(30)]
        public string Desc { get; set; } = null!;
        [StringLength(15)]
        public string Status { get; set; } = null!;
        [Column(TypeName = "decimal(9,2)")]
        public decimal Total { get; set; } = 0;
        [StringLength(30)]
        public string EmployeeId { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
       
    }
}
