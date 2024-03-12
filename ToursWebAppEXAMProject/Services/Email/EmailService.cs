using MailKit.Net.Imap;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using ToursWebAppEXAMProject.ConfigFiles;

namespace ToursWebAppEXAMProject.Services.Email
{
    public class EmailService
    {
        /// <summary>
        /// Метод отправки сообщения в email пользователя для подтверждения регистрации
        /// </summary>
        /// <param name="email">эл. почта пользователя</param>
        /// <param name="subject">тема сообщения</param>
        /// <param name="message">текст сообщения</param>
        /// <returns></returns>
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // создание сообщения
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress($"Туристическая фирма: {ConfigEmail.CompanyName}", ConfigEmail.Email));
            emailMessage.To.Add(new MailboxAddress("Пользователь", email));           
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                // открытие соединения по протоколу SMTP
                await client.ConnectAsync(ConfigEmail.SmtpServer, ConfigEmail.PortSmtp, ConfigEmail.UseSslSmtp);
                // аутентификация по электронной почте и коду для приложения для аккаунта
                await client.AuthenticateAsync(ConfigEmail.Email, CodeApp.GetPassword());
                // отправка сообщения
                await client.SendAsync(emailMessage);
                // закрытие соединения
                await client.DisconnectAsync(true);
            }
        }

        /// <summary>
        /// Метод как бы отправки сообщения от пользователя на почту турфирмы
        /// </summary>
        /// <param name="email">эл. почта пользователя</param>
        /// <param name="subject">тема сообщения</param>
        /// <param name="message">текст сообщения</param>
        /// <returns></returns>
        public async Task SendEmailFromClientAsync(string email, string subject, string message)
        {
            // создание сообщения, кот. пересылается турфирмой на свою почту какбы от имени пользователя
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress($"Пользователь: {email}", ConfigEmail.Email));
            emailMessage.To.Add(new MailboxAddress($"Туристическая фирма: {ConfigEmail.CompanyName}", ConfigEmail.Email));
            emailMessage.Subject = $"Вопрос от пользователя {subject}. Сформирован через форму обратной связи на сайте {ConfigData.Domain}. Ответ создан автоматически и отправлен на email пользователя {subject}";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
                        
            using (var client = new SmtpClient())
            {
                // открытие соединения по протоколу SMTP
                await client.ConnectAsync(ConfigEmail.SmtpServer, ConfigEmail.PortSmtp, ConfigEmail.UseSslSmtp);
                // аутентификация по электронной почте и коду для приложения для аккаунта
                await client.AuthenticateAsync(ConfigEmail.Email, CodeApp.GetPassword());
                // отправка сообщения
                await client.SendAsync(emailMessage);
                // закрытие соединения
                await client.DisconnectAsync(true);
            }
        }

        /// <summary>
        /// Метод получения сообщений пользователей
        /// </summary>
        /// <returns></returns>
        public async Task ReceiveEmailAsync()
        {
            using (var client = new ImapClient())
            {
                await client.ConnectAsync(ConfigEmail.ImapServer, ConfigEmail.PortImap, ConfigEmail.UseSslImap);
                await client.AuthenticateAsync(ConfigEmail.Email, CodeApp.GetPassword());
                
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                // вывод в консоль всех сообщений с почтового клиента
                Console.WriteLine("Total messages: {0}", inbox.Count);
                Console.WriteLine("Recent messages: {0}", inbox.Recent);

                for (int i = 0; i < inbox.Count; i++)
                {
                    var message = inbox.GetMessage(i);
                    Console.WriteLine("Subject: {0}", message.Subject);
                }

                await client.DisconnectAsync(true);
            }
           
        }
    }
}

