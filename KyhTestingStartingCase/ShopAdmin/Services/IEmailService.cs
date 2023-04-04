using MimeKit;

namespace ShopAdmin.Services
{
    public interface IEmailService
    {
        public List<MimeMessage> CreatingEmails(List<string> listOfEmailsAdresses, List<string> listOfNames);
        public void SendingEmails(List<MimeMessage> listOfEmails);
    }
}
