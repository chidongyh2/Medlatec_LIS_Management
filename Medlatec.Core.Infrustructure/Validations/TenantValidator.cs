using FluentValidation;
using Medlatec.Core.Application.ModelMetas;
using Medlatec.Core.Domain.Resources;
using Medlatec.Infrastructure.Helpers;
using Medlatec.Infrastructure.IServices;
using Medlatec.Infrastructure.Resources;

namespace Medlatec.Core.Infrastructure.Validations
{
    public class TenantValidator : AbstractValidator<TenantMeta>
    {
        public TenantValidator(IResourceService<CoreResource> resourceService, IResourceService<SharedResource> sharedResourceService)
        {
            RuleFor(x => x.Name).NotNullAndEmpty(resourceService.GetString("Tenant name can not be null."))
                .MaximumLength(256).WithMessage(sharedResourceService.GetString("Tenant name cannot exceed 256 characters."));

            RuleFor(x => x.Email).NotNullAndEmpty(sharedResourceService.GetString("Email cannot be null."))
                .MaximumLength(500).WithMessage(sharedResourceService.GetString("Email cannot exceed 500 characters."));

            RuleFor(x => x.PhoneNumber).NotNullAndEmpty(sharedResourceService.GetString("Phonenumber cannot be null."))
                .MaximumLength(50).WithMessage(sharedResourceService.GetString("Phonenumber cannot exceed 50 characters."));

            RuleFor(x => x.Address).NotNullAndEmpty(sharedResourceService.GetString("Address cannot be null."))
                .MaximumLength(500).WithMessage(sharedResourceService.GetString("Address cannot exceed 500 characters."));

            RuleFor(x => x.Note)
                .MaximumLength(500).WithMessage(sharedResourceService.GetString("Note cannot exceed 500 characters."));

            RuleFor(x => x.UserName).NotNullAndEmpty(sharedResourceService.GetString("UserName cannot be null."))
              .MaximumLength(50).WithMessage(sharedResourceService.GetString("Phonenumber cannot exceed 50 characters."));

            RuleFor(x => x.Password).NotNullAndEmpty(sharedResourceService.GetString("Password cannot be null."))
             .MaximumLength(50).WithMessage(sharedResourceService.GetString("Phonenumber cannot exceed 50 characters."));
        }
    }
}
