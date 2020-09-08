using FluentValidation;
using TestProject.Domain.Dto.Requests;

namespace TestProject.Domain.Validators
{
    public class UserValidator : AbstractValidator<CreateUserRequest>
    {
        public UserValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email is not valid");

            RuleFor(u => u.Name)
                .NotEmpty()
                .MaximumLength(10)
                .WithMessage("Name is not valid");

            RuleFor(u => u.MonthlySalary)
                .GreaterThan(0)
                .WithMessage("Monthly salary must greater than 0");

            RuleFor(u => u.MonthlyExpense)
                .GreaterThan(0)
                .WithMessage("Monthly expense must greater than 0");
        }
    }
}
