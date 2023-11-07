// найдем html элементы по их id
let user = document.getElementById("textUser");
let message = document.getElementById("textMessage");
let listMessages = document.getElementById("list-message");
let sendMsg = document.getElementById("btnSend");

// создадим соединение signalR
let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

// подписываемся на событие получения сообщения, 
// функция - колбек создает элемент списка с полученным сообщением
connection.on("ReceiveMessage", function (fromUser, message) {
    // создаем элемент списка
    var li = document.createElement("li");

    // формируем строку с именем пользователя и его сообщением
    let msg = fromUser + ": " + message;
        
    // записываем в элемент списка присланное сообщение
    li.textContent = msg;

    // вставляем в конец списка
    listMessages.appendChild(li);

});

connection.start();

// подписываемся на нажатие кнопки "Отправить сообщение"
sendMsg.addEventListener("click", function () {
    // получаем введенное имя пользователя и его сообщение
    let userConnect = user.value;
    let messageConnect = message.value;
    
    // отправляем сообщение
    connection.invoke("SendMessage", userConnect, messageConnect);
});