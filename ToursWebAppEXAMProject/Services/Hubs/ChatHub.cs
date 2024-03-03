using Microsoft.AspNetCore.SignalR;
using ToursWebAppEXAMProject.Enums;
using static TourWebAppEXAMProject.Services.LogsMode.LogsMode;

namespace TourWebAppEXAMProject.Services.Hubs
{
    public class ChatHub : Hub
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

            // сохранение логов в NLoog;    id соединения, у каждого пользователя - свой
            /* var connectionId = Context.ConnectionId;
               var fullStringMessage = $"{DateTime.Now.ToString("HH:mm:ss")}: {fromUser}:\n {message}\n";

               WriteLogs(fullStringMessage, NLogsModeEnum.Debug);
               WriteLogs($"Id соединения: {connectionId}", NLogsModeEnum.Debug);*/
        }
    }
}
