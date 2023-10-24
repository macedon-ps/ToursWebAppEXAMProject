using Microsoft.AspNetCore.SignalR;

namespace ToursWebAppEXAMProject.Hubs
{
    public class ChatHub: Hub
    {
        /// <summary>
        /// Метод рассылки сообщений в SignalR Core
        /// </summary>
        /// <param name="fromUser">Пользователь сети</param>
        /// <param name="message">Сообщение пользователя сети</param>
        /// <returns></returns>
        public async Task SendMessage(string fromUser, string message)
        {
            // рассылаем сообщение всем пользователям сети;
            // формат: имя пользователя и сообщение
            await Clients.All.SendAsync("ReceiveMessage", fromUser, message);
        }
    }
}
