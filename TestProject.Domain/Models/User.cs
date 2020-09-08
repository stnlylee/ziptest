using System.Collections.Generic;

namespace TestProject.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal MonthlyExpense { get; set; }
        public virtual IEnumerable<Account> Accounts { get; set; }
    }
}
