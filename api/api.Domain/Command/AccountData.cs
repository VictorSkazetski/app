namespace api.Domain.Command
{
    public class AccountData
    {
        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public string? RepeatPassword { get; set; }
    }
}
