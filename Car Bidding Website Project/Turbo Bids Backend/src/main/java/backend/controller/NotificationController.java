package backend.controller;

import backend.service.NotificationService;
import backend.service.domain.Notification;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.messaging.handler.annotation.MessageMapping;
import org.springframework.messaging.handler.annotation.SendTo;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
public class NotificationController {

    @Autowired
    private NotificationService notificationService;

    @MessageMapping("/notify")
    @SendTo("/topic/notifications")
    public Notification sendNotification(Notification notification) {
        return notificationService.saveNotification(notification);
    }

    @GetMapping("/notifications/{recipient}")
    public List<Notification> getUnseenNotificationsForUser(@PathVariable String recipient) {
        return notificationService.getUnseenNotificationsForUser(recipient);
    }

    @PutMapping("/notifications/{notificationId}")
    public void markNotificationAsSeen(@PathVariable Long notificationId) {
        notificationService.markNotificationAsSeen(notificationId);
    }
}
