namespace api.Domain.Command
{
    public class EmailVerifyTokenData
    {
        public string UserEmail { get; set; }
        public string Token { get; set; }
    }
}
