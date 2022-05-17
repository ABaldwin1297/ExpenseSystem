namespace ExpenseSystem.Models {
    public class ExpenseLine {
        public int Id { get; set; }
        public int Quantity { get; set; } = 0;
        public int ExpenseId { get; set; } = 0;
        public virtual Expense Expenses { get; set; } = null!;
        public int ItemId { get; set; } = 0;
        public virtual Item Item { get; set; } = null!;
    }
}
