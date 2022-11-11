using FluentValidation;
using Producer.Dtos;
using Producer.Utils;
using RabbitMQService.Dtos;

namespace Producer.Validator
{
    public class UserValidator  : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username cannot be empty");
                
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty")
                .Length(5, 15).WithMessage("Password length must be between 5 and 15")
                .Must(ValidatorUtils.HasValidPassword);
        }
    }
}
