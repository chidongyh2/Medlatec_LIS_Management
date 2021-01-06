using FluentValidation;
using Medlatec.Core.Application.ModelMetas;
using Medlatec.Infrastructure.Helpers;

namespace Medlatec.Core.Application.Validations
{
    public class PageMetaValidator : AbstractValidator<PageMeta>
    {
        public PageMetaValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Mã trang không được để trống.");
            RuleFor(x => x.Name).NotNullAndEmpty("Tên trang không được để trống.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Tên trang không được phép vượt quá 50 ký tự.");

        }
    }
}
