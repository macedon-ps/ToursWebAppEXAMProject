// ������ html �������� �� �� id
let user = document.getElementById("textUser");
let message = document.getElementById("textMessage");
let listMessages = document.getElementById("list-message");
let sendMsg = document.getElementById("btnSend");

// �������� ���������� signalR
let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

// ������������� �� ������� ��������� ���������, 
// ������� - ������ ������� ������� ������ � ���������� ����������
connection.on("ReceiveMessage", function (fromUser, message) {
    // ������� ������� ������
    var li = document.createElement("li");

    // ��������� ������ � ������ ������������ � ��� ����������
    let msg = fromUser + ": " + message;
        
    // ���������� � ������� ������ ���������� ���������
    li.textContent = msg;

    // ��������� � ����� ������
    listMessages.appendChild(li);

});

connection.start();

// ������������� �� ������� ������ "��������� ���������"
sendMsg.addEventListener("click", function () {
    // �������� ��������� ��� ������������ � ��� ���������
    let userConnect = user.value;
    let messageConnect = message.value;
    
    // ���������� ���������
    connection.invoke("SendMessage", userConnect, messageConnect);
});