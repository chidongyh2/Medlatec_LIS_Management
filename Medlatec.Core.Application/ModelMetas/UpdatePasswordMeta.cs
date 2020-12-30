namespace Medlatec.Core.Application.ModelMetas
{
    public class UpdatePasswordMeta
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public bool IsResetPassword { get; set; }

        public UpdatePasswordMeta()
        {
            IsResetPassword = false;
        }
    }
}
