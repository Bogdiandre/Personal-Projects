// src/api/WebSocketClient.js
import SockJS from 'sockjs-client';
import { Stomp } from '@stomp/stompjs';

let stompClient = null;

export const connect = (recipient, onMessageReceived) => {
  const socket = new SockJS('http://localhost:8090/ws');
  stompClient = Stomp.over(socket);
  stompClient.connect({}, () => {
    console.log(`Connected to WebSocket for recipient: ${recipient}`);
    stompClient.subscribe(`/user/${recipient}/queue/notifications`, (message) => {
      if (message.body) {
        onMessageReceived(JSON.parse(message.body));
      }
    });
  }, (error) => {
    console.error('Error connecting to WebSocket:', error);
  });
};

export const disconnect = () => {
  if (stompClient) {
    stompClient.disconnect();
  }
  console.log('Disconnected from WebSocket');
};

export default {
  connect,
  disconnect,
};
