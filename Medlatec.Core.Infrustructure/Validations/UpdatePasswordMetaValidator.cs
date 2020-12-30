using FluentValidation;
using Medlatec.Core.Application.ModelMetas;
using Medlatec.Core.Domain.Resources;
using Medlatec.Infrastructure.IServices;
using Medlatec.Infrastructure.Resources;

namespace Medlatec.Core.Infrastructure.Validations
{
    public class UpdatePasswordMetaValidator : AbstractValidator<UpdatePasswordMeta>
    {
        public UpdatePasswordMetaValidator(IResourceService<CoreResource> resourceService, IResourceService<SharedResource> sharedResourceService)
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty()
                .WithMessage(resourceService.GetString("Please enter old password."))
                .MaximumLength(50).WithMessage(sharedResourceService.GetString("{0} is not allowed over {1} characters.",
                    resourceService.GetString("Old password"), 50));

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage(resourceService.GetString("Please enter new password."))
                .MaximumLength(50).WithMessage(sharedResourceService.GetString("{0} is not allowed over {1} characters.",
                    resourceService.GetString("New password"), 50));

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty()
                .WithMessage(resourceService.GetString("Please enter confirm password."))
                .MaximumLength(50).WithMessage(sharedResourceService.GetString(
                    "{0} is not allowed over {1} characters.",
                    resourceService.GetString("Confirm password"), 50))
                .NotEqual(x => x.NewPassword)
                .WithMessage(resourceService.GetString("Confirm password not match with new password."));
        }
    }
}
