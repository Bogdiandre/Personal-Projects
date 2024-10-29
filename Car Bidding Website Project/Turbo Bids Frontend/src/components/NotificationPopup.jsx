import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom'; // Import Link from react-router-dom
import './NotificationPopup.css';
import NotificationAPI from '../api/NotificationAPI';
import WebSocketClient from '../api/WebSocketClient';
import TokenManager from '../api/TokenManager';

const NotificationPopup = () => {
  const [isOpen, setIsOpen] = useState(false);
  const [notifications, setNotifications] = useState([]);

  useEffect(() => {
    const recipient = TokenManager.getUserId();
    if (!recipient) {
      console.error('No recipient found');
      return;
    }

    console.log('Fetching notifications for recipient:', recipient);

    // Fetch notifications from the API
    const fetchNotifications = async () => {
      try {
        const data = await NotificationAPI.getNotificationsForUser(recipient);
        setNotifications(data);
      } catch (error) {
        console.error('Error fetching notifications:', error);
      }
    };

    fetchNotifications();

    // Connect to WebSocket and subscribe to notifications
    WebSocketClient.connect(recipient, (newNotification) => {
      setNotifications((prevNotifications) => [newNotification, ...prevNotifications]);
    });

    return () => {
      WebSocketClient.disconnect();
    };
  }, []);

  const togglePopup = () => {
    setIsOpen(!isOpen);
  };

  return (
    <div className="notification-container">
      <button className="inbox-btn" onClick={togglePopup}>
        <svg viewBox="0 0 512 512" height="16" xmlns="http://www.w3.org/2000/svg">
          <path
            d="M48 64C21.5 64 0 85.5 0 112c0 15.1 7.1 29.3 19.2 38.4L236.8 313.6c11.4 8.5 27 8.5 38.4 0L492.8 150.4c12.1-9.1 19.2-23.3 19.2-38.4c0-26.5-21.5-48-48-48H48zM0 176V384c0 35.3 28.7 64 64 64H448c35.3 0 64-28.7 64-64V176L294.4 339.2c-22.8 17.1-54 17.1-76.8 0L0 176z"
          ></path>
        </svg>
        <span className="msg-count">{notifications.length}</span>
      </button>
      {isOpen && (
        <div className="notification-popup">
          <div className="notification-list">
            {notifications.map((notification) => (
              <div className="notificationForm" key={notification.id}>
                <div className="container">
                  <div className="left">
                    <div className="status-ind"></div>
                  </div>
                  <div className="right">
                    <div className="text-wrap">
                      <p className="text-content">
                        {notification.message}
                      </p>
                    </div>
                    <div className="button-wrap">
                      <Link to={`/listings/${notification.listingId}`} className="primary-cta">View Listing</Link>
                      <button className="secondary-cta">Mark as read</button>
                    </div>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      )}
    </div>
  );
};

export default NotificationPopup;
