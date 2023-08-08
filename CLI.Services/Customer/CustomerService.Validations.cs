using System.Runtime.CompilerServices;
using CLI.Entity.Dto;

namespace CLI.Services.Customer
{
    public partial class CustomerService
    {
        private bool IsCustomerInputValid(CreateCustomerDto input)
        {
            return !string.IsNullOrWhiteSpace(input?.Name) && !string.IsNullOrWhiteSpace(input?.Description);
        }

        private void SetValidationMessage<T>(T obj, [CallerMemberName] string methodName = null) where T : BaseOutputDto
        {
            string ValidatonMessage = "{0} Input Data is invalid";

            obj ??= (T)Activator.CreateInstance(typeof(T));

            obj.ErrorMessage = String.Concat(ValidatonMessage, methodName);
        }

        private bool IsAddToWishListInputValid(AddProducToWishListDto input)
        {
            return !string.IsNullOrWhiteSpace(input?.Name) && !string.IsNullOrWhiteSpace(input?.Description)
                && Guid.Empty != (input?.ProductId ?? Guid.Empty);
        }
    }
}
