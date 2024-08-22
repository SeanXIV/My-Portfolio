using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public static class Mail
{
    public static async Task SendMailAsync(string firstName, string lastName, string email, string contactReason)
    {
        try
        {
            var fromAddress = new MailAddress("your-email@example.com", "Your Name");
            var toAddress = new MailAddress("andrewseanego14@gmail.com", "Andrew Seanego");
            const string fromPassword = "your-email-password";
            const string subject = "New Contact Form Submission";

            string body = $"New contact form submission:\n\n" +
                          $"Name: {firstName} {lastName}\n" +
                          $"Email: {email}\n" +
                          $"Reason for Contact: {contactReason}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                await smtp.SendMailAsync(message);
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions (log them, display an error message, etc.)
            Console.WriteLine($"Failed to send email: {ex.Message}");
            throw;
        }
    }
}