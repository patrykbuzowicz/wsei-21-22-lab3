// Receiving messages.
const connection = new signalR.HubConnectionBuilder()
    .withUrl('/chat/hub')
    .build();

const messagesContainer = document.getElementById('messages');

connection.on('OnMessageSent', (user, message) => {
    const messageElement = document.createElement('div');
    messageElement.className = 'p-2';

    messageElement.innerText = user + ' says: ' + message;

    messagesContainer.appendChild(messageElement);
    messagesContainer.scrollTo(0, messagesContainer.scrollHeight);
});

function startConnection() {
    connection
        .start()
        // Support infinite retry.
        .catch(() => setTimeout(startConnection, 1000));
}

// Support reconnect.
connection.onclose(() => startConnection());

startConnection();

// Sending messages.
const formElement = document.getElementById('message-form');

formElement.addEventListener('submit', event => {
    event.preventDefault();

    const message = formElement.elements.message.value;

    if (message) {
        connection.invoke('SendMessage', message);
    }

    formElement.elements.message.value = '';
});
