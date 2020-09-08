namespace TestProject.Domain.Dto.Requests
{
    public class CreateUserRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal MonthlyExpense { get; set; }
    }
}
