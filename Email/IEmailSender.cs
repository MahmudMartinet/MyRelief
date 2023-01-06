namespace Relief.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(EmailRequestModel email);
    }
}
