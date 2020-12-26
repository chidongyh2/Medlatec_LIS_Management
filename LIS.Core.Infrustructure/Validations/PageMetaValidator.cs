using FluentValidation;
using LIS.Core.Application.ModelMetas;

namespace LIS.Core.Infrastructure.Validations
{
    public class PageMetaValidator : AbstractValidator<PageMeta>
    {
        public PageMetaValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Mã trang không được để trống.");
            //RuleFor(x => x.Name).NotNullAndEmpty"Tên trang không được để trống.");
            //RuleFor(x => x.Name).MaximumLength(50).WithMessage("Tên trang không được phép vượt quá 50 ký tự.");

        }
    }
}
