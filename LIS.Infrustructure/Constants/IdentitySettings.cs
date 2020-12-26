namespace LIS.Infrastructure.Constants
{
    public class IdentitySettings
    {
        public string PublicOrigin { get; set; }
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ApiName { get; set; }
        public string ApiSecret { get; set; }
        public bool RequireHttpsMetadata { get; set; }
    }
}
