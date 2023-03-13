namespace api.Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string link, string email = "vbtestv@mail.ru");
    }
}
