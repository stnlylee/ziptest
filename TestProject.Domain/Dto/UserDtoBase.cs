namespace TestProject.Domain.Dto
{
    public abstract class UserDtoBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal MonthlyExpense { get; set; }
    }
}
