using FluentValidation;
using RabbitMQService.Dtos;

namespace Producer.Validator
{
    public class OrderValidator  : AbstractValidator<OrderDto>
    {
        public OrderValidator()
        {
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price cannot be empty");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product Name cannot be empty")
                .Matches(@"^[a-zA-Z-']*$").WithMessage(" Product Name cannot contain special character");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity cannot be empty");
        }
    }
}
