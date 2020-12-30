namespace Medlatec.Authentication.Requests
{
    public class GetInfoByTokenRequest
    {
        public string Token { get; set; }
        public string Provider { get; set; }
    }
}
