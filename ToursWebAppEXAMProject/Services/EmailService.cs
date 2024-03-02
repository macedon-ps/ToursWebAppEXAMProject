﻿using MailKit.Net.Smtp;
using MimeKit;
using ToursWebAppEXAMProject.ConfigFiles;

namespace ToursWebAppEXAMProject.Services
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

            emailMessage.From.Add(new MailboxAddress(ConfigEmail.CompanyName, ConfigEmail.Email));
            emailMessage.To.Add(new MailboxAddress("Получатель", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                // открытие соединения по протоколу SMTP
                await client.ConnectAsync(ConfigEmail.SmtpServer, ConfigEmail.Port, ConfigEmail.UseSsl);
                // аутентификация по электронной почте и коду для приложения для аккаунта
                await client.AuthenticateAsync(ConfigEmail.Email, CodeApp.GetPassword());
                // отправка сообщения
                await client.SendAsync(emailMessage);
                // закрытие соединения
                await client.DisconnectAsync(true);
            }
        }
    }
}

