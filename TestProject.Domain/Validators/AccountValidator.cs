using FluentValidation;
using TestProject.Domain.Dto.Requests;

namespace TestProject.Domain.Validators
{
    public class AccountValidator : AbstractValidator<CreateAccountRequest>
    {
        public AccountValidator()
        {
            RuleFor(a => a.UserId)
                .NotEmpty()
                .WithMessage("Need a user id to created account"); 
        }
    }
}
